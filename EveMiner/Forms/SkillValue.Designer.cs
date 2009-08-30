namespace EveMiner.Forms
{
	partial class SkillValue
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
			this.numericUpDownValue = new System.Windows.Forms.NumericUpDown();
			this.pictureBoxValue = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxValue)).BeginInit();
			this.SuspendLayout();
			// 
			// numericUpDownValue
			// 
			this.numericUpDownValue.Location = new System.Drawing.Point(54, 0);
			this.numericUpDownValue.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numericUpDownValue.Name = "numericUpDownValue";
			this.numericUpDownValue.Size = new System.Drawing.Size(30, 20);
			this.numericUpDownValue.TabIndex = 3;
			this.numericUpDownValue.ValueChanged += new System.EventHandler(this.numericUpDownValue_ValueChanged);
			// 
			// pictureBoxValue
			// 
			this.pictureBoxValue.Image = global::EveMiner.Properties.Resources.level0;
			this.pictureBoxValue.Location = new System.Drawing.Point(0, 5);
			this.pictureBoxValue.Name = "pictureBoxValue";
			this.pictureBoxValue.Size = new System.Drawing.Size(48, 8);
			this.pictureBoxValue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxValue.TabIndex = 2;
			this.pictureBoxValue.TabStop = false;
			// 
			// SkillValue
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.numericUpDownValue);
			this.Controls.Add(this.pictureBoxValue);
			this.Name = "SkillValue";
			this.Size = new System.Drawing.Size(84, 20);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxValue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDownValue;
		private System.Windows.Forms.PictureBox pictureBoxValue;
	}
}
