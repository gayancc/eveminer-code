namespace EveMiner.Forms
{
	partial class OreCtrl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelTime = new System.Windows.Forms.Label();
			this.labelUnit = new System.Windows.Forms.Label();
			this.pictureBoxOmber = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxOmber)).BeginInit();
			this.SuspendLayout();
			// 
			// labelTime
			// 
			this.labelTime.AutoSize = true;
			this.labelTime.Location = new System.Drawing.Point(38, 19);
			this.labelTime.Name = "labelTime";
			this.labelTime.Size = new System.Drawing.Size(34, 13);
			this.labelTime.TabIndex = 39;
			this.labelTime.Text = "20:32";
			// 
			// labelUnit
			// 
			this.labelUnit.AutoSize = true;
			this.labelUnit.Location = new System.Drawing.Point(38, 0);
			this.labelUnit.Name = "labelUnit";
			this.labelUnit.Size = new System.Drawing.Size(66, 13);
			this.labelUnit.TabIndex = 40;
			this.labelUnit.Text = "9999/11000";
			// 
			// pictureBoxOmber
			// 
			this.pictureBoxOmber.Image = global::EveMiner.Properties.Resources.Nocxium;
			this.pictureBoxOmber.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxOmber.Name = "pictureBoxOmber";
			this.pictureBoxOmber.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxOmber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxOmber.TabIndex = 38;
			this.pictureBoxOmber.TabStop = false;
			// 
			// OreCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.labelTime);
			this.Controls.Add(this.labelUnit);
			this.Controls.Add(this.pictureBoxOmber);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.Name = "OreCtrl";
			this.Size = new System.Drawing.Size(124, 32);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxOmber)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelTime;
		private System.Windows.Forms.Label labelUnit;
		private System.Windows.Forms.PictureBox pictureBoxOmber;
	}
}
