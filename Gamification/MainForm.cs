using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography;

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
		string pathRelease;
		string pathDebug;
		string[] srcList;
		int[] lines;

		string lastRelease;
		string lastDebug;

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
			pathRelease = $"{dir}../Release/{projectName}.exe";
			pathDebug = $"{dir}../Debug/{projectName}.exe";

			List<string> sourceList = new List<string>();
			try
			{
				FileHelper.GetAllFiles(dir, "*.cpp", sourceList);
			}
			catch
			{
				MessageBox.Show("Directory is empty or doesn't exist", "Error in config");
				Environment.Exit(0);
			}
			var temp = sourceList.ToArray();
			sourceList = null;
			sourceList = new List<string>();
			try
			{
				FileHelper.GetAllFiles(dir, "*.h", sourceList);
			}
			catch
			{
				MessageBox.Show("Directory is empty or doesn't exist", "Error in config");
				Environment.Exit(0);
			}


			srcList = new string[temp.Length + sourceList.ToArray().Length];
			temp.CopyTo(srcList, 0);
			sourceList.CopyTo(srcList, temp.Length);

			srcQuantity = srcList.Length;
			lines = new int[srcQuantity];

			lastRelease = File.Exists(pathRelease) ? CalculateMD5(pathRelease) : "0";
			lastDebug   = File.Exists(pathDebug)   ? CalculateMD5(pathDebug)   : "0";

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
							lines[i] = currLines;
						}
					}
				}

				/* SRC CHECK END */

				/* RELEASE CHECK START */

				string currRelease = File.Exists(pathRelease) ? CalculateMD5(pathRelease) : "0";
				if (currRelease != lastRelease && currRelease != "0")
				{
					lastRelease = currRelease;
					xp.IncreaseXp(20);
				}

				/* RELEASE CHECK END */

				/* DEBUG CHECK START */

				string currDebug = File.Exists(pathDebug) ? CalculateMD5(pathDebug) : "0";
				if (currDebug != lastDebug && currDebug != "0")
				{
					lastDebug = currDebug;
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

		string CalculateMD5(string filename)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Read))
				{
					var hash = md5.ComputeHash(stream);
					return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
				}
			}
		}
	}
}
