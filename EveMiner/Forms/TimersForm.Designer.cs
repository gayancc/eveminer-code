namespace EveMiner.Forms
{
	partial class TimersForm
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
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.dataGridViewTimers = new System.Windows.Forms.DataGridView();
			this.ColumnOre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnStartQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnCurrentQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnCycle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnTimeToEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnButtonStart = new System.Windows.Forms.DataGridViewButtonColumn();
			this.ColumnButtonDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.checkBoxUseGangBonuses = new System.Windows.Forms.CheckBox();
			this.textBoxCycle = new System.Windows.Forms.TextBox();
			this.textBoxMiningYield = new System.Windows.Forms.TextBox();
			this.textBoxStartValue = new System.Windows.Forms.TextBox();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.comboBoxOre = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTimers)).BeginInit();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(3, 290);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(449, 17);
			this.progressBar1.TabIndex = 26;
			// 
			// dataGridViewTimers
			// 
			this.dataGridViewTimers.AllowUserToAddRows = false;
			this.dataGridViewTimers.AllowUserToDeleteRows = false;
			this.dataGridViewTimers.AllowUserToOrderColumns = true;
			this.dataGridViewTimers.AllowUserToResizeRows = false;
			this.dataGridViewTimers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                        | System.Windows.Forms.AnchorStyles.Left)
			                                                                       | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewTimers.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridViewTimers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridViewTimers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridViewTimers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			                                                                                       	this.ColumnOre,
			                                                                                       	this.ColumnStartQty,
			                                                                                       	this.ColumnCurrentQty,
			                                                                                       	this.ColumnCycle,
			                                                                                       	this.ColumnTimeToEnd,
			                                                                                       	this.ColumnButtonStart,
			                                                                                       	this.ColumnButtonDelete});
			this.dataGridViewTimers.Location = new System.Drawing.Point(3, 60);
			this.dataGridViewTimers.Name = "dataGridViewTimers";
			this.dataGridViewTimers.ReadOnly = true;
			this.dataGridViewTimers.RowHeadersVisible = false;
			this.dataGridViewTimers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewTimers.Size = new System.Drawing.Size(449, 224);
			this.dataGridViewTimers.TabIndex = 25;
			// 
			// ColumnOre
			// 
			this.ColumnOre.HeaderText = "Ore";
			this.ColumnOre.Name = "ColumnOre";
			this.ColumnOre.ReadOnly = true;
			this.ColumnOre.ToolTipText = "Ore Type";
			this.ColumnOre.Width = 147;
			// 
			// ColumnStartQty
			// 
			this.ColumnStartQty.HeaderText = "Start";
			this.ColumnStartQty.Name = "ColumnStartQty";
			this.ColumnStartQty.ReadOnly = true;
			this.ColumnStartQty.ToolTipText = "Starting Quantity";
			this.ColumnStartQty.Width = 60;
			// 
			// ColumnCurrentQty
			// 
			this.ColumnCurrentQty.FillWeight = 90F;
			this.ColumnCurrentQty.HeaderText = "Current";
			this.ColumnCurrentQty.Name = "ColumnCurrentQty";
			this.ColumnCurrentQty.ReadOnly = true;
			this.ColumnCurrentQty.ToolTipText = "Current Quantity";
			this.ColumnCurrentQty.Width = 60;
			// 
			// ColumnCycle
			// 
			this.ColumnCycle.HeaderText = "Cycle";
			this.ColumnCycle.Name = "ColumnCycle";
			this.ColumnCycle.ReadOnly = true;
			this.ColumnCycle.Width = 60;
			// 
			// ColumnTimeToEnd
			// 
			this.ColumnTimeToEnd.HeaderText = "Time";
			this.ColumnTimeToEnd.Name = "ColumnTimeToEnd";
			this.ColumnTimeToEnd.ReadOnly = true;
			this.ColumnTimeToEnd.Width = 60;
			// 
			// ColumnButtonStart
			// 
			this.ColumnButtonStart.HeaderText = ">";
			this.ColumnButtonStart.Name = "ColumnButtonStart";
			this.ColumnButtonStart.ReadOnly = true;
			this.ColumnButtonStart.Text = ">";
			this.ColumnButtonStart.ToolTipText = "Start timer";
			this.ColumnButtonStart.Width = 20;
			// 
			// ColumnButtonDelete
			// 
			this.ColumnButtonDelete.HeaderText = "x";
			this.ColumnButtonDelete.Name = "ColumnButtonDelete";
			this.ColumnButtonDelete.ReadOnly = true;
			this.ColumnButtonDelete.Text = "x";
			this.ColumnButtonDelete.ToolTipText = "Delete row";
			this.ColumnButtonDelete.Width = 20;
			// 
			// checkBoxUseGangBonuses
			// 
			this.checkBoxUseGangBonuses.AutoSize = true;
			this.checkBoxUseGangBonuses.Location = new System.Drawing.Point(279, 37);
			this.checkBoxUseGangBonuses.Name = "checkBoxUseGangBonuses";
			this.checkBoxUseGangBonuses.Size = new System.Drawing.Size(104, 17);
			this.checkBoxUseGangBonuses.TabIndex = 22;
			this.checkBoxUseGangBonuses.Text = "Use Gang Boost";
			this.checkBoxUseGangBonuses.UseVisualStyleBackColor = true;
			// 
			// textBoxCycle
			// 
			this.textBoxCycle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxCycle.Location = new System.Drawing.Point(338, 5);
			this.textBoxCycle.Name = "textBoxCycle";
			this.textBoxCycle.ReadOnly = true;
			this.textBoxCycle.Size = new System.Drawing.Size(104, 22);
			this.textBoxCycle.TabIndex = 24;
			this.textBoxCycle.Text = "180";
			this.textBoxCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBoxMiningYield
			// 
			this.textBoxMiningYield.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxMiningYield.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxMiningYield.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxMiningYield.Location = new System.Drawing.Point(205, 5);
			this.textBoxMiningYield.Name = "textBoxMiningYield";
			this.textBoxMiningYield.ReadOnly = true;
			this.textBoxMiningYield.Size = new System.Drawing.Size(127, 22);
			this.textBoxMiningYield.TabIndex = 23;
			this.textBoxMiningYield.Text = "1081.5525";
			this.textBoxMiningYield.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBoxStartValue
			// 
			this.textBoxStartValue.Location = new System.Drawing.Point(98, 35);
			this.textBoxStartValue.Name = "textBoxStartValue";
			this.textBoxStartValue.Size = new System.Drawing.Size(42, 20);
			this.textBoxStartValue.TabIndex = 19;
			this.textBoxStartValue.Text = "1000";
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Location = new System.Drawing.Point(389, 33);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(53, 23);
			this.buttonUpdate.TabIndex = 21;
			this.buttonUpdate.Text = "Update";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(146, 33);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(53, 23);
			this.buttonAdd.TabIndex = 20;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			// 
			// comboBoxOre
			// 
			this.comboBoxOre.DropDownHeight = 250;
			this.comboBoxOre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxOre.FormattingEnabled = true;
			this.comboBoxOre.IntegralHeight = false;
			this.comboBoxOre.Location = new System.Drawing.Point(62, 6);
			this.comboBoxOre.Name = "comboBoxOre";
			this.comboBoxOre.Size = new System.Drawing.Size(137, 21);
			this.comboBoxOre.TabIndex = 17;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 38);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 13);
			this.label3.TabIndex = 18;
			this.label3.Text = "Asteroid Volume:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Ore type:";
			// 
			// TimersForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(465, 312);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.dataGridViewTimers);
			this.Controls.Add(this.checkBoxUseGangBonuses);
			this.Controls.Add(this.textBoxCycle);
			this.Controls.Add(this.textBoxMiningYield);
			this.Controls.Add(this.textBoxStartValue);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.comboBoxOre);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Name = "TimersForm";
			this.Text = "Timers";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimersForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTimers)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.DataGridView dataGridViewTimers;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOre;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStartQty;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCurrentQty;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCycle;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTimeToEnd;
		private System.Windows.Forms.DataGridViewButtonColumn ColumnButtonStart;
		private System.Windows.Forms.DataGridViewButtonColumn ColumnButtonDelete;
		private System.Windows.Forms.CheckBox checkBoxUseGangBonuses;
		private System.Windows.Forms.TextBox textBoxCycle;
		private System.Windows.Forms.TextBox textBoxMiningYield;
		private System.Windows.Forms.TextBox textBoxStartValue;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.ComboBox comboBoxOre;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
	}
}