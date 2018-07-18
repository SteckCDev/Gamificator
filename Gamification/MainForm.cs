using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Gamification
{
	public partial class MainForm : Form
	{
		XP xp;

		Thread mon;

		const int SIZE = 2;
		string dir;
		string projectName;
		string[] src;
		int[] lines;
		string lastReleseHash;
		int[] array;

		public MainForm()
		{
			InitializeComponent();

			//$"{File.ReadAllLines("").Length}";
			//ui_level.Location = new Point(this.Size.Width / 2 - ui_level.Size.Width / 2, ui_level.Location.Y);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			xp = new XP(new float[] { 1.5f, 2f }, 50, 0, 0);
			mon = new Thread(Monitoring);

			dir = "C:/Users/grief/Documents/Visual Studio 2017/Projects/Learning/Learning/";
			projectName = "Learning";
			src = new string[] { "main.cpp", "Source.cpp" };
			lines = new int[SIZE];
			lastReleseHash = Checksum($"{dir}../Release/{projectName}.exe"); // TAKE FROM CONFIG

			DateTime dateTime = File.GetLastWriteTime($"{dir}../Release/{projectName}.exe");
			array = new int[] { dateTime.Year, dateTime.DayOfYear, dateTime.Hour, dateTime.Minute };

			for (int i = 0; i < SIZE; i++)
			{
				lines[i] = File.ReadAllLines(dir + src[i]).Length;
			}

			UpdateInfo();
			Start();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// CFG HERE

			Stop();
		}

		private void Monitoring()
		{
			while (true)
			{
				/* SRC CHECK START */

				for (int i = 0; i < SIZE; i++)
				{
					int currLines = File.ReadAllLines(dir + src[i]).Length;
					if (currLines > lines[i])
					{
						xp.IncreaseXp(currLines - lines[i]);
						lines[i] = currLines;
					}
					else if (currLines < lines[i])
					{
						//lines[i] = currLines;
					}
				}

				/* SRC CHECK END */

				/* RELEASE CHECK START */
				/*
				string hash = Checksum($"{dir}../Release/{projectName}.exe");

				if (lastReleseHash != hash)
				{
					lastReleseHash = hash;
					xp.IncreaseXp(20);
				}
				*/

				DateTime dateTime = File.GetLastWriteTime($"{dir}../Release/{projectName}.exe");
				if (dateTime.Year > array[0] || dateTime.DayOfYear > array[1] || dateTime.Hour > array[2] || dateTime.Minute > array[3])
				{
					array[0] = dateTime.Year; array[1] = dateTime.DayOfYear; array[2] = dateTime.Hour; array[3] = dateTime.Minute;
					xp.IncreaseXp(20);
				}

				/* RELEASE CHECK END */

				UpdateInfo();
				Thread.Sleep(1000);
			}
		}

		private void UpdateInfo()
		{
			SetValue(xp.GetXP(), xp.GetTarget(), ui_xpPreview);
			SetText($"{xp.GetXP()} XP", ui_xp);
			SetText($"{xp.GetTarget()} XP", ui_xpTarget);
			SetText($"Level {xp.GetLevel()}", ui_level);
		}

		/* THREADS */
		/* THREADS */
		/* THREADS */

		private void Start()
		{
			mon = new Thread(new ThreadStart(Monitoring));
			mon.Start();
		}

		private void Stop()
		{
			mon.Abort();
			mon = null;
		}

		delegate void StringArgReturningVoidDelegate(string text, Control obj);
		delegate void IntegerArgReturningVoidDelegate(int value, int max, ProgressBar obj);

		private void SetText(string text, Control obj)
		{
			if (obj.InvokeRequired)
			{
				StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetText);
				Invoke(d, new object[] { text, obj });
			}
			else
			{
				obj.Text = text;
			}
		}

		private void SetValue(int value, int max, ProgressBar obj)
		{
			if (obj.InvokeRequired)
			{
				IntegerArgReturningVoidDelegate d = new IntegerArgReturningVoidDelegate(SetValue);
				Invoke(d, new object[] { value, max, obj });
			}
			else
			{
				obj.Maximum = max;
				obj.Value = value;
			}
		}

		/* TOOLS */
		/* TOOLS */
		/* TOOLS */

		private string Checksum(string path)
		{	if (File.Exists(path))
			{
				using (FileStream fs = File.OpenRead(path))
				{
					MD5 md5 = new MD5CryptoServiceProvider();
					byte[] fileData = new byte[fs.Length];
					fs.Read(fileData, 0, (int)fs.Length);
					byte[] checkSum = md5.ComputeHash(fileData);
					string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
					return result;
				}
			}
			else
			{
				return "0";
			}
		}
	}
}
