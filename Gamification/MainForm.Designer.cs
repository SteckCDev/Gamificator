namespace Gamification
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.ui_xpPreview = new System.Windows.Forms.ProgressBar();
			this.ui_xp = new System.Windows.Forms.Label();
			this.ui_xpTarget = new System.Windows.Forms.Label();
			this.ui_level = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ui_xpPreview
			// 
			this.ui_xpPreview.BackColor = System.Drawing.Color.White;
			this.ui_xpPreview.Enabled = false;
			this.ui_xpPreview.Location = new System.Drawing.Point(12, 12);
			this.ui_xpPreview.Maximum = 50;
			this.ui_xpPreview.Name = "ui_xpPreview";
			this.ui_xpPreview.Size = new System.Drawing.Size(276, 10);
			this.ui_xpPreview.Step = 100;
			this.ui_xpPreview.TabIndex = 0;
			this.ui_xpPreview.Value = 25;
			// 
			// ui_xp
			// 
			this.ui_xp.AutoSize = true;
			this.ui_xp.BackColor = System.Drawing.Color.Transparent;
			this.ui_xp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ui_xp.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ui_xp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.ui_xp.Location = new System.Drawing.Point(12, 25);
			this.ui_xp.Name = "ui_xp";
			this.ui_xp.Size = new System.Drawing.Size(34, 17);
			this.ui_xp.TabIndex = 1;
			this.ui_xp.Text = "0 XP";
			// 
			// ui_xpTarget
			// 
			this.ui_xpTarget.AutoSize = true;
			this.ui_xpTarget.BackColor = System.Drawing.Color.Transparent;
			this.ui_xpTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ui_xpTarget.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ui_xpTarget.ForeColor = System.Drawing.Color.Lavender;
			this.ui_xpTarget.Location = new System.Drawing.Point(247, 25);
			this.ui_xpTarget.Name = "ui_xpTarget";
			this.ui_xpTarget.Size = new System.Drawing.Size(41, 17);
			this.ui_xpTarget.TabIndex = 2;
			this.ui_xpTarget.Text = "50 XP";
			// 
			// ui_level
			// 
			this.ui_level.AutoSize = true;
			this.ui_level.BackColor = System.Drawing.Color.Transparent;
			this.ui_level.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ui_level.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ui_level.ForeColor = System.Drawing.Color.White;
			this.ui_level.Location = new System.Drawing.Point(103, 33);
			this.ui_level.Name = "ui_level";
			this.ui_level.Size = new System.Drawing.Size(95, 34);
			this.ui_level.TabIndex = 3;
			this.ui_level.Text = "Level 0";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.GrayText;
			this.ClientSize = new System.Drawing.Size(300, 75);
			this.Controls.Add(this.ui_level);
			this.Controls.Add(this.ui_xpTarget);
			this.Controls.Add(this.ui_xp);
			this.Controls.Add(this.ui_xpPreview);
			this.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar ui_xpPreview;
		private System.Windows.Forms.Label ui_xp;
		private System.Windows.Forms.Label ui_xpTarget;
		private System.Windows.Forms.Label ui_level;
	}
}

