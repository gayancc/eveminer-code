using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EveMiner
{
	public static class Sound
	{
		/// <summary>
		/// Plays the sound.
		/// </summary>
		/// <param name="filename">The filename.</param>
		public static void PlaySound(string filename)
		{
			try
			{
				string file = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar +
							  filename;
				System.Media.SoundPlayer sp = new System.Media.SoundPlayer(file);
				sp.Play();
			}
			catch (FileNotFoundException)
			{
			}
		}

	}
}
