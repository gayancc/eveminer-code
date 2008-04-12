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
		/// ��������� ����� �������
		/// </summary>
		private const int _configEncoding = 1251;

		/// <summary>
		/// ����������� ������
		/// </summary>
		private static readonly object _lockFlag = new object();

// 		private static string _configFileName = Path.Combine(
// 			Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Config.xml");


		/// <summary>
		/// ������������ ��� ����� �������
		/// </summary>
		/// <remarks>
		/// �������� XmlSerializer ����� ���� ���, ��� ��� ��� ��� ��������
		/// ���������� ������������ ���������� ����, ��� ����� �� 
		/// ������� �������!
		/// ����� ������� ������� ��� ��� ������������� ����������� ����������.
		///</remarks>
		private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(T));

		/// <summary>
		/// ������ ��� ����� �������
		/// </summary>
		private static string _configFileName = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" +
			Application.ProductName, typeof(T).Name + ".xml");

		/// <summary>
		/// ���������� �������� �������� ���������.
		/// </summary>
		private static T _instance;

		/// <summary>
		/// ������������ ��� ����� �������
		/// </summary>
		private static XmlSerializer Serializer
		{
			get { return _serializer; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// ������������ �������
		/// </summary>
		/// <returns></returns>
		public static T Clone()
		{
			// �������� ������������ ����������
			// ����� ���������, �� � ������ ������ ��� �������
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
		/// ������������� ������
		/// </summary>
		public static void Reload()
		{
			_instance = default(T);
		}
		/// <summary>
		/// ������ ������� � ���� �� ����
		/// </summary>
		public static void Save()
		{
			if(!Directory.Exists(Path.GetDirectoryName(_configFileName)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(_configFileName));
			}

			//������ ��������� ����� �������
			if(File.Exists(_configFileName))
			{
				if(File.Exists(_configFileName + ".old"))
					File.Delete(_configFileName + ".old");
				File.Move(_configFileName, _configFileName + ".old");
			}

			// ���������� ������ � ����. ����� �� ������ ��� � 
			// ��c���� ���������.
			if(Instance != null)
			{
				using(StreamWriter writer = new StreamWriter(
					_configFileName, false,
					Encoding.GetEncoding(_configEncoding)))
				{
					// ��� �������������� ���������� ������ ��������������.
					Serializer.Serialize(writer, _instance);
				}
			}
		}

		/// <summary>
		/// ���������� ��� ���������� �������.
		/// </summary>
		public static event EventHandler ConfigChanged;

		/// <summary>
		/// ��������� ����� ������ ��� �������� ���������� �������
		/// </summary>
		/// <param name="cfg">����������� ������</param>
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
				// ��� ��������... ������������ �� ����������.
				if(_instance != null)
					return _instance;

				// ���������� ����� ������ ���� �������� ��� �� ������.
				// � ��� ������ ������ �����.
				lock (_lockFlag)
				{
					// ��� �������� ����� ��� ��� ���-�� ��� ������ ������� 
					// ������ �� ����������.
					if(_instance != null)
						return _instance;

					try
					{
						// �������� ��������� ���� � ����� � ��������������� ���
						// ������ ��� ����� � ���������� ���������.
						using(StreamReader reader =
							new StreamReader(_configFileName,
							                 Encoding.GetEncoding(_configEncoding)))
						{
							// ��� �������������� ���������� ������ ��������������.
							_instance = (T) Serializer.Deserialize(reader);
							//���� �� ����� ���� �������� ������ ����, �� � ��� ��� ���������� ���������� ���������������� ������� 
							if(_instance == null)
								_instance = new T();
						}
					}
					catch(FileNotFoundException ex)
					{
						// ���� �� ������� ��������������� �� ������ ������� ����� ���������
						// � ���� ���, �� ����� ��� ���������! ����� ���� �� ������ ���������
						// �� ������!
						Trace.Write(
							string.Format("���������� ������� ������������ '{0}'. ������: '{1}'. ������ ������ �� �������", _configFileName,
							              ex.Message));
						_instance = new T();
					}
					catch(DirectoryNotFoundException ex)
					{
						// ���� �� ������� ��������������� �� ������ ������� ����� ���������
						// � ���� ���, �� ����� ��� ���������! ����� ���� �� ������ ���������
						// �� ������!
						Trace.Write(
							string.Format("���������� ������� ������������ '{0}'. ������: '{1}'. ������ ������ �� �������", _configFileName,
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
		/// ������ ��� ����� �������
		/// </summary>
		public static string ConfigFileName
		{
			get { return _configFileName; }
			set { _configFileName = value; }
		}
		#endregion
	}
}