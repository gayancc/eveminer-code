using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EveMiner
{
	public static class Config<T> where T : class, new()
	{
		#region Fields
		/// <summary>
		/// Кодировка файла конфига
		/// </summary>
		private const int _configEncoding = 1251;

		/// <summary>
		/// Критическая секция
		/// </summary>
		private static readonly object _lockFlag = new object();

// 		private static string _configFileName = Path.Combine(
// 			Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Config.xml");


		/// <summary>
		/// Сериализатор для файла конфига
		/// </summary>
		/// <remarks>
		/// Сздавать XmlSerializer нужно один раз, так как при его создании
		/// происходит динамическая компиляция кода, что очень не 
		/// быстрое занятие!
		/// Самое простое сделать это при инициализации статической переменной.
		///</remarks>
		private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(T));

		/// <summary>
		/// Полное имя файла конфига
		/// </summary>
		private static string _configFileName = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" +
			Application.ProductName, typeof(T).Name + ".xml");

		/// <summary>
		/// Переменная хранящая значение синглтона.
		/// </summary>
		private static T _instance;

		/// <summary>
		/// Сериализатор для файла конфига
		/// </summary>
		private static XmlSerializer Serializer
		{
			get { return _serializer; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Клонирование конфига
		/// </summary>
		/// <returns></returns>
		public static T Clone()
		{
			// глубокое клонирование экземпляра
			// метод медленный, но в данном случае это неважно
			T newInstance;
			using(MemoryStream ms = new MemoryStream())
			{
				Serializer.Serialize(ms, Instance);
				ms.Flush();
				ms.Position = 0;
				newInstance = (T) Serializer.Deserialize(ms);
				//newInstance.ConfigChanged = _instance.ConfigChanged;
			}
			return newInstance;
			//return (Config)MemberwiseClone();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Перезагрузить конфиг
		/// </summary>
		public static void Reload()
		{
			_instance = default(T);
		}
		/// <summary>
		/// Запись конфига в файл на диск
		/// </summary>
		public static void Save()
		{
			if(!Directory.Exists(Path.GetDirectoryName(_configFileName)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(_configFileName));
			}

			//делаем резервную копию конфига
			if(File.Exists(_configFileName))
			{
				if(File.Exists(_configFileName + ".old"))
					File.Delete(_configFileName + ".old");
				File.Move(_configFileName, _configFileName + ".old");
			}

			// Записываем конфиг в файл. Опять же делаем это в 
			// руcской кодировке.
			if(Instance != null)
			{
				using(StreamWriter writer = new StreamWriter(
					_configFileName, false,
					Encoding.GetEncoding(_configEncoding)))
				{
					// Для десериализации пользуемся единым сериализатором.
					Serializer.Serialize(writer, _instance);
				}
			}
		}

		/// <summary>
		/// Вызывается при обновлении конфига.
		/// </summary>
		public static event EventHandler ConfigChanged;

		/// <summary>
		/// Назначить новый конфиг для текущего экземпляра объекта
		/// </summary>
		/// <param name="cfg">назначаемый конфиг</param>
		public static void NewConfig(T cfg)
		{
			_instance = cfg;
			if(ConfigChanged != null)
				ConfigChanged(_instance, EventArgs.Empty);
		}
		#endregion

		#region Properties
		[XmlIgnore]
		public static T Instance
		{
			get
			{
				// Для скорости... исзбавляемся от блокировки.
				if(_instance != null)
					return _instance;

				// Блокировка нужна только если экземляр еще не создан.
				// А это бывает крайне редко.
				lock (_lockFlag)
				{
					// Эта проверка нужна так как кто-то мог успеть создать 
					// объект до блокировки.
					if(_instance != null)
						return _instance;

					try
					{
						// Пытаемся загрузить файл с диска и десериализовать его
						// Делать это лучше в конкретной кодировке.
						using(StreamReader reader =
							new StreamReader(_configFileName,
							                 Encoding.GetEncoding(_configEncoding)))
						{
							// Для десериализации пользуемся единым сериализатором.
							_instance = (T) Serializer.Deserialize(reader);
							//Если по каким либо причинам конфиг есть, но в нем нет экземпляра ожидаемого десериализуемого объекта 
							if(_instance == null)
								_instance = new T();
						}
					}
					catch(FileNotFoundException ex)
					{
						// Если не удалось десериализовать то просто создаем новый экземпляр
						// И если что, не знаем что случилось! Нужно хотя бы выдать сообщение
						// об ошибке!
						Trace.Write(
							string.Format("Невозможно считать конфигурацию '{0}'. Ошибка: '{1}'. Создан конфиг по дефолту", _configFileName,
							              ex.Message));
						_instance = new T();
					}
					catch(DirectoryNotFoundException ex)
					{
						// Если не удалось десериализовать то просто создаем новый экземпляр
						// И если что, не знаем что случилось! Нужно хотя бы выдать сообщение
						// об ошибке!
						Trace.Write(
							string.Format("Невозможно считать конфигурацию '{0}'. Ошибка: '{1}'. Создан конфиг по дефолту", _configFileName,
							              ex.Message));
						_instance = new T();
					}
					catch(Exception ex)
					{
						Trace.Write(ex);
						throw;
					}

					return _instance;
				}
			}
		}

		/// <summary>
		/// Полное имя файла конфига
		/// </summary>
		public static string ConfigFileName
		{
			get { return _configFileName; }
			set { _configFileName = value; }
		}
		#endregion
	}
}