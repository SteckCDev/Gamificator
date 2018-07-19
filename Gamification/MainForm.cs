using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Gamification
{
	public partial class MainForm : Form
	{
		XP xp;
		Thread mon;
		IniFile cfg;

		int srcQuantity;
		string dir;
		string projectName;
		string[] srcList;
		int[] lines;
		int[] lastRelease;
		int[] lastDebug;

		public MainForm()
		{
			InitializeComponent();

			TopMost = true;
			Screen sc = Screen.FromHandle(Handle);
			Location = new Point(SystemInformation.PrimaryMonitorSize.Width - Size.Width, sc.WorkingArea.Height - Size.Height);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (!File.Exists("cfg.ini"))
			{
				using (var sw = File.AppendText(@"cfg.ini"))
				{
					sw.WriteLine("[constructor]");
					sw.WriteLine("dir=");
					sw.WriteLine("project=");
					sw.WriteLine("target=");
					sw.WriteLine("xp=");
					sw.WriteLine("level=");
				}
			}

			cfg = new IniFile("cfg.ini");

			try
			{
				xp = new XP(new float[] { 1.5f, 2f }, Int32.Parse(cfg.Read("target", "constructor")), Int32.Parse(cfg.Read("xp", "constructor")), Int32.Parse(cfg.Read("level", "constructor"))); // TAKE FROM CONFIG
			}
			catch
			{
				MessageBox.Show("Fill config first", "Error in config");
				Environment.Exit(0);
			}

			dir = cfg.Read("dir", "constructor");
			projectName = cfg.Read("project", "constructor");

			List<string> sourceList = new List<string>();
			try
			{
				FileHelper.GetAllFiles(dir, "*.cpp", sourceList);
			}
			catch
			{
				MessageBox.Show("Directory is empty or not exist", "Error in config");
				Environment.Exit(0);
			}
			var temp = sourceList.ToArray();
			sourceList = null;
			sourceList = new List<string>();
			FileHelper.GetAllFiles(dir, "*.h", sourceList);

			srcList = new string[temp.Length + sourceList.ToArray().Length];
			temp.CopyTo(srcList, 0);
			sourceList.CopyTo(srcList, temp.Length);

			srcQuantity = srcList.Length;
			lines = new int[srcQuantity];

			DateTime dateTime;
			dateTime = File.GetLastWriteTime($"{dir}../Release/{projectName}.exe");
			lastRelease = new int[] { dateTime.Year, dateTime.DayOfYear, dateTime.Hour, dateTime.Minute };
			dateTime = File.GetLastWriteTime($"{dir}../Debug/{projectName}.exe");
			lastDebug = new int[] { dateTime.Year, dateTime.DayOfYear, dateTime.Hour, dateTime.Minute };

			for (int i = 0; i < srcQuantity; i++)
			{
				lines[i] = File.ReadAllLines(srcList[i]).Length;
			}

			UpdateInfo();
			Start();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) { Stop(); ConfigWrite(); }

		private void Monitoring()
		{
			while (true)
			{
				int startXp = xp.GetXP();
				int tarXp = xp.GetTarget();

				/* SRC CHECK START */

				for (int i = 0; i < srcQuantity; i++)
				{
					if (File.Exists(srcList[i])) {
						int currLines = File.ReadAllLines(srcList[i]).Length;
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
				}

				/* SRC CHECK END */

				/* RELEASE CHECK START */

				DateTime dateTime = File.GetLastWriteTime($"{dir}../Release/{projectName}.exe");
				if (dateTime.Year > lastRelease[0] || dateTime.DayOfYear > lastRelease[1] || dateTime.Hour > lastRelease[2] || dateTime.Minute > lastRelease[3])
				{
					lastRelease[0] = dateTime.Year; lastRelease[1] = dateTime.DayOfYear; lastRelease[2] = dateTime.Hour; lastRelease[3] = dateTime.Minute;
					xp.IncreaseXp(20);
				}

				/* RELEASE CHECK END */

				/* DEBUG CHECK START */

				dateTime = File.GetLastWriteTime($"{dir}../Debug/{projectName}.exe");
				if (dateTime.Year > lastDebug[0] || dateTime.DayOfYear > lastDebug[1] || dateTime.Hour > lastDebug[2] || dateTime.Minute > lastDebug[3])
				{
					lastDebug[0] = dateTime.Year; lastDebug[1] = dateTime.DayOfYear; lastDebug[2] = dateTime.Hour; lastDebug[3] = dateTime.Minute;
					xp.IncreaseXp(5);
				}

				/* DEBUG CHECK END */

				if (startXp != xp.GetXP() || tarXp != xp.GetTarget())
				{
					UpdateInfo();
				}
				
				Thread.Sleep(1000);
			}
		}

		private void UpdateInfo()
		{
			SetValue(xp.GetXP(), xp.GetTarget(), ui_xpPreview);
			SetText($"{xp.GetXP()} XP", ui_xp);
			SetText($"{xp.GetTarget()} XP", ui_xpTarget);
			SetText($"Level {xp.GetLevel()}", ui_level);
			SetLocation(new Point(Size.Width / 2 - ui_level.Size.Width / 2, ui_level.Location.Y), ui_level);
			SetLocation(new Point(12, ui_xp.Location.Y), ui_xp);
			SetLocation(new Point(Size.Width - 12 - ui_xpTarget.Size.Width, ui_xpTarget.Location.Y), ui_xpTarget);

			ConfigWrite();
		}

		private void ConfigWrite()
		{
			cfg.Write("target", xp.GetTarget().ToString(), "constructor");
			cfg.Write("xp", xp.GetXP().ToString(), "constructor");
			cfg.Write("level", xp.GetLevel().ToString(), "constructor");
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
		delegate void LocationArgReturningVoidDelegate(Point point, Control obj);
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

		private void SetLocation(Point point, Control obj)
		{
			if (obj.InvokeRequired)
			{
				LocationArgReturningVoidDelegate d = new LocationArgReturningVoidDelegate(SetLocation);
				Invoke(d, new object[] { point, obj });
			}
			else
			{
				obj.Location = point;
			}
		}

		/* TOOLS */
		/* TOOLS */
		/* TOOLS */

		public static class FileHelper
		{
			public static void GetAllFiles(string rootDirectory, string fileExtension, List<string> files)
			{
				string[] directories = Directory.GetDirectories(rootDirectory);
				files.AddRange(Directory.GetFiles(rootDirectory, fileExtension));

				foreach (string path in directories)
					GetAllFiles(path, fileExtension, files);
			}
		}
	}
}
