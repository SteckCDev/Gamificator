using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gamification
{
	public partial class MainForm : Form
	{
		XP xp;

		public MainForm()
		{
			InitializeComponent();
			ui_level.Location = new Point(this.Size.Width / 2 - ui_level.Size.Width / 2, ui_level.Location.Y);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			xp = new XP(50, new float[]{ 1.5f, 2f });
		}
	}
}
