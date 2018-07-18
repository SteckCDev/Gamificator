using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Gamification
{
	public partial class MainForm : Form
	{
		XP xp;

		Thread mon;

		const int SIZE = 2;
		string dir;
		string[] src;
		int[] lines;

		public MainForm()
		{
			InitializeComponent();

			//$"{File.ReadAllLines("").Length}";
			//ui_level.Location = new Point(this.Size.Width / 2 - ui_level.Size.Width / 2, ui_level.Location.Y);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			xp = new XP(50, new float[] { 1.5f, 2f }, 0, 0);
			mon = new Thread(Monitoring);

			dir = "C:/Users/grief/Documents/Visual Studio 2017/Projects/Learning/Learning/";
			src = new string[] { "main.cpp", "Source.cpp" };
			lines = new int[SIZE];

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

		private void Monitoring()
		{
			while (true)
			{
				for (int i = 0; i < SIZE; i++)
				{
					int currLines = File.ReadAllLines(dir + src[i]).Length;
					if (currLines > lines[i])
					{
						xp.IncreaseXp(currLines - lines[i]);
						lines[i] = currLines;
						UpdateInfo();
					}
					else if (currLines < lines[i])
					{
						//lines[i] = currLines;
					}
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
		}
	}
}
