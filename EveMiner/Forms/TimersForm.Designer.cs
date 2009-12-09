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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimersForm));
			this.dataGridViewTimers = new System.Windows.Forms.DataGridView();
			this.textBoxCycle = new System.Windows.Forms.TextBox();
			this.textBoxMiningYield = new System.Windows.Forms.TextBox();
			this.textBoxStartValue = new System.Windows.Forms.TextBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.comboBoxOre = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.ColumnOre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnStartQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnCurrentQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnCycle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnTimeToEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnLaser1Start = new System.Windows.Forms.DataGridViewImageColumn();
			this.ColumnLaser2Start = new System.Windows.Forms.DataGridViewImageColumn();
			this.ColumnLaser3Start = new System.Windows.Forms.DataGridViewImageColumn();
			this.ColumnButtonDelete = new System.Windows.Forms.DataGridViewImageColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTimers)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewTimers
			// 
			this.dataGridViewTimers.AllowUserToAddRows = false;
			this.dataGridViewTimers.AllowUserToDeleteRows = false;
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
            this.ColumnLaser1Start,
            this.ColumnLaser2Start,
            this.ColumnLaser3Start,
            this.ColumnButtonDelete});
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTimers.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewTimers.Location = new System.Drawing.Point(12, 60);
			this.dataGridViewTimers.Name = "dataGridViewTimers";
			this.dataGridViewTimers.ReadOnly = true;
			this.dataGridViewTimers.RowHeadersVisible = false;
			this.dataGridViewTimers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewTimers.Size = new System.Drawing.Size(504, 240);
			this.dataGridViewTimers.TabIndex = 25;
			this.dataGridViewTimers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewTimersCellClick);
			// 
			// textBoxCycle
			// 
			this.textBoxCycle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxCycle.Location = new System.Drawing.Point(344, 2);
			this.textBoxCycle.Name = "textBoxCycle";
			this.textBoxCycle.ReadOnly = true;
			this.textBoxCycle.Size = new System.Drawing.Size(104, 22);
			this.textBoxCycle.TabIndex = 24;
			this.textBoxCycle.Text = "180";
			this.textBoxCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxCycle.MouseLeave += new System.EventHandler(this.OnMouseLeaveCtrl);
			this.textBoxCycle.MouseEnter += new System.EventHandler(this.OnMouseEnterCtrl);
			// 
			// textBoxMiningYield
			// 
			this.textBoxMiningYield.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxMiningYield.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxMiningYield.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBoxMiningYield.Location = new System.Drawing.Point(211, 2);
			this.textBoxMiningYield.Name = "textBoxMiningYield";
			this.textBoxMiningYield.ReadOnly = true;
			this.textBoxMiningYield.Size = new System.Drawing.Size(127, 22);
			this.textBoxMiningYield.TabIndex = 23;
			this.textBoxMiningYield.Text = "1081.5525";
			this.textBoxMiningYield.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxMiningYield.MouseLeave += new System.EventHandler(this.OnMouseLeaveCtrl);
			this.textBoxMiningYield.MouseEnter += new System.EventHandler(this.OnMouseEnterCtrl);
			// 
			// textBoxStartValue
			// 
			this.textBoxStartValue.Location = new System.Drawing.Point(104, 32);
			this.textBoxStartValue.Name = "textBoxStartValue";
			this.textBoxStartValue.Size = new System.Drawing.Size(42, 20);
			this.textBoxStartValue.TabIndex = 19;
			this.textBoxStartValue.Text = "1000";
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(152, 30);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(53, 23);
			this.buttonAdd.TabIndex = 20;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// comboBoxOre
			// 
			this.comboBoxOre.DropDownHeight = 250;
			this.comboBoxOre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxOre.FormattingEnabled = true;
			this.comboBoxOre.IntegralHeight = false;
			this.comboBoxOre.Location = new System.Drawing.Point(68, 3);
			this.comboBoxOre.Name = "comboBoxOre";
			this.comboBoxOre.Size = new System.Drawing.Size(137, 21);
			this.comboBoxOre.TabIndex = 17;
			this.comboBoxOre.SelectedIndexChanged += new System.EventHandler(this.comboBoxOre_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 35);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 13);
			this.label3.TabIndex = 18;
			this.label3.Text = "Asteroid Volume:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Ore type:";
			// 
			// toolTipInfo
			// 
			this.toolTipInfo.IsBalloon = true;
			this.toolTipInfo.ShowAlways = true;
			this.toolTipInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUpdate.Location = new System.Drawing.Point(463, 31);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(53, 23);
			this.buttonUpdate.TabIndex = 21;
			this.buttonUpdate.Text = "Update";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			this.buttonUpdate.MouseLeave += new System.EventHandler(this.OnMouseLeaveCtrl);
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			this.buttonUpdate.MouseEnter += new System.EventHandler(this.OnMouseEnterCtrl);
			// 
			// ColumnOre
			// 
			this.ColumnOre.Frozen = true;
			this.ColumnOre.HeaderText = "Ore";
			this.ColumnOre.Name = "ColumnOre";
			this.ColumnOre.ReadOnly = true;
			this.ColumnOre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnOre.ToolTipText = "Ore Type";
			this.ColumnOre.Width = 147;
			// 
			// ColumnStartQty
			// 
			this.ColumnStartQty.Frozen = true;
			this.ColumnStartQty.HeaderText = "Start";
			this.ColumnStartQty.Name = "ColumnStartQty";
			this.ColumnStartQty.ReadOnly = true;
			this.ColumnStartQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnStartQty.ToolTipText = "Starting Quantity";
			this.ColumnStartQty.Width = 60;
			// 
			// ColumnCurrentQty
			// 
			this.ColumnCurrentQty.FillWeight = 90F;
			this.ColumnCurrentQty.Frozen = true;
			this.ColumnCurrentQty.HeaderText = "Current";
			this.ColumnCurrentQty.Name = "ColumnCurrentQty";
			this.ColumnCurrentQty.ReadOnly = true;
			this.ColumnCurrentQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnCurrentQty.ToolTipText = "Current Quantity";
			this.ColumnCurrentQty.Width = 60;
			// 
			// ColumnCycle
			// 
			this.ColumnCycle.Frozen = true;
			this.ColumnCycle.HeaderText = "Cycle";
			this.ColumnCycle.Name = "ColumnCycle";
			this.ColumnCycle.ReadOnly = true;
			this.ColumnCycle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnCycle.Width = 60;
			// 
			// ColumnTimeToEnd
			// 
			this.ColumnTimeToEnd.Frozen = true;
			this.ColumnTimeToEnd.HeaderText = "Time";
			this.ColumnTimeToEnd.Name = "ColumnTimeToEnd";
			this.ColumnTimeToEnd.ReadOnly = true;
			this.ColumnTimeToEnd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColumnTimeToEnd.Width = 60;
			// 
			// ColumnLaser1Start
			// 
			this.ColumnLaser1Start.Frozen = true;
			this.ColumnLaser1Start.HeaderText = "1";
			this.ColumnLaser1Start.Name = "ColumnLaser1Start";
			this.ColumnLaser1Start.ReadOnly = true;
			this.ColumnLaser1Start.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ColumnLaser1Start.ToolTipText = "Turret 1";
			this.ColumnLaser1Start.Width = 24;
			// 
			// ColumnLaser2Start
			// 
			this.ColumnLaser2Start.Frozen = true;
			this.ColumnLaser2Start.HeaderText = "2";
			this.ColumnLaser2Start.Name = "ColumnLaser2Start";
			this.ColumnLaser2Start.ReadOnly = true;
			this.ColumnLaser2Start.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ColumnLaser2Start.ToolTipText = "Turret 2";
			this.ColumnLaser2Start.Width = 24;
			// 
			// ColumnLaser3Start
			// 
			this.ColumnLaser3Start.Frozen = true;
			this.ColumnLaser3Start.HeaderText = "3";
			this.ColumnLaser3Start.Name = "ColumnLaser3Start";
			this.ColumnLaser3Start.ReadOnly = true;
			this.ColumnLaser3Start.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ColumnLaser3Start.ToolTipText = "Turret 3";
			this.ColumnLaser3Start.Width = 24;
			// 
			// ColumnButtonDelete
			// 
			this.ColumnButtonDelete.Frozen = true;
			this.ColumnButtonDelete.HeaderText = "x";
			this.ColumnButtonDelete.Name = "ColumnButtonDelete";
			this.ColumnButtonDelete.ReadOnly = true;
			this.ColumnButtonDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ColumnButtonDelete.ToolTipText = "Delete row";
			this.ColumnButtonDelete.Width = 24;
			// 
			// TimersForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(529, 312);
			this.Controls.Add(this.dataGridViewTimers);
			this.Controls.Add(this.textBoxCycle);
			this.Controls.Add(this.textBoxMiningYield);
			this.Controls.Add(this.textBoxStartValue);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.comboBoxOre);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TimersForm";
			this.Text = "Timers";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimersForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTimers)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewTimers;
		private System.Windows.Forms.TextBox textBoxCycle;
		private System.Windows.Forms.TextBox textBoxMiningYield;
		private System.Windows.Forms.TextBox textBoxStartValue;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.ComboBox comboBoxOre;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolTip toolTipInfo;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOre;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStartQty;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCurrentQty;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCycle;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTimeToEnd;
		private System.Windows.Forms.DataGridViewImageColumn ColumnLaser1Start;
		private System.Windows.Forms.DataGridViewImageColumn ColumnLaser2Start;
		private System.Windows.Forms.DataGridViewImageColumn ColumnLaser3Start;
		private System.Windows.Forms.DataGridViewImageColumn ColumnButtonDelete;
	}
}