namespace Gamification
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.ui_xp = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// ui_xp
			// 
			this.ui_xp.Enabled = false;
			this.ui_xp.Location = new System.Drawing.Point(12, 12);
			this.ui_xp.Name = "ui_xp";
			this.ui_xp.Size = new System.Drawing.Size(276, 10);
			this.ui_xp.Step = 100;
			this.ui_xp.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.ui_xp.TabIndex = 0;
			this.ui_xp.Value = 50;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(300, 100);
			this.Controls.Add(this.ui_xp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.ShowIcon = false;
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar ui_xp;
	}
}

