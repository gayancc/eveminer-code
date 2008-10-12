using OreType=EveMiner.Ores.OreType;

namespace EveMiner.Forms
{
	partial class OreForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.oreCtrl1 = new EveMiner.Forms.OreCtrl();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(22, 273);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OreForm_MouseMove);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OreForm_MouseDown);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OreForm_MouseUp);
			// 
			// oreCtrl1
			// 
			this.oreCtrl1.BackColor = System.Drawing.Color.Black;
			this.oreCtrl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.oreCtrl1.Location = new System.Drawing.Point(24, 12);
			this.oreCtrl1.Name = "oreCtrl1";
			this.oreCtrl1.OreType = OreType.Gneiss;
			this.oreCtrl1.Size = new System.Drawing.Size(124, 32);
			this.oreCtrl1.TabIndex = 2;
			this.oreCtrl1.VolumePerMinute = 0;
			// 
			// OreForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(160, 274);
			this.Controls.Add(this.oreCtrl1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "OreForm";
			this.Text = "OreForm";
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OreForm_MouseUp);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OreForm_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OreForm_MouseMove);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private OreCtrl oreCtrl1;



	}
}