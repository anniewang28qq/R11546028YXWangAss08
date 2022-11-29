namespace R11546028YXWangAss07
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip_NewGA = new System.Windows.Forms.ToolStrip();
            this.tsb_CreateBinaryGA = new System.Windows.Forms.ToolStripButton();
            this.tsb_CreatePermutation = new System.Windows.Forms.ToolStripButton();
            this.tsb_Open = new System.Windows.Forms.ToolStripButton();
            this.status_GAType = new System.Windows.Forms.StatusStrip();
            this.tssl_type = new System.Windows.Forms.ToolStripStatusLabel();
            this.dlg_Open = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer_Form = new System.Windows.Forms.SplitContainer();
            this.splitContainer_User = new System.Windows.Forms.SplitContainer();
            this.dgv_SetUpTime = new System.Windows.Forms.DataGridView();
            this.txb_Penalty = new System.Windows.Forms.TextBox();
            this.l_Penalty = new System.Windows.Forms.Label();
            this.l_description = new System.Windows.Forms.Label();
            this.l_numOfSoFarUpdated = new System.Windows.Forms.Label();
            this.l_numOfIteration = new System.Windows.Forms.Label();
            this.toolStrip_NewGA.SuspendLayout();
            this.status_GAType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Form)).BeginInit();
            this.splitContainer_Form.Panel2.SuspendLayout();
            this.splitContainer_Form.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_User)).BeginInit();
            this.splitContainer_User.Panel1.SuspendLayout();
            this.splitContainer_User.Panel2.SuspendLayout();
            this.splitContainer_User.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SetUpTime)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip_NewGA
            // 
            this.toolStrip_NewGA.AutoSize = false;
            this.toolStrip_NewGA.CanOverflow = false;
            this.toolStrip_NewGA.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip_NewGA.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_CreateBinaryGA,
            this.tsb_CreatePermutation,
            this.tsb_Open});
            this.toolStrip_NewGA.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_NewGA.Name = "toolStrip_NewGA";
            this.toolStrip_NewGA.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip_NewGA.Size = new System.Drawing.Size(1086, 33);
            this.toolStrip_NewGA.TabIndex = 0;
            this.toolStrip_NewGA.Text = "toolStrip1";
            // 
            // tsb_CreateBinaryGA
            // 
            this.tsb_CreateBinaryGA.BackColor = System.Drawing.SystemColors.Control;
            this.tsb_CreateBinaryGA.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CreateBinaryGA.Image")));
            this.tsb_CreateBinaryGA.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CreateBinaryGA.Name = "tsb_CreateBinaryGA";
            this.tsb_CreateBinaryGA.Size = new System.Drawing.Size(165, 28);
            this.tsb_CreateBinaryGA.Text = "New Binary GA";
            this.tsb_CreateBinaryGA.Click += new System.EventHandler(this.tsb_CreateBinaryGA_Click);
            // 
            // tsb_CreatePermutation
            // 
            this.tsb_CreatePermutation.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CreatePermutation.Image")));
            this.tsb_CreatePermutation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CreatePermutation.Name = "tsb_CreatePermutation";
            this.tsb_CreatePermutation.Size = new System.Drawing.Size(219, 28);
            this.tsb_CreatePermutation.Text = "New Permutation GA";
            this.tsb_CreatePermutation.Click += new System.EventHandler(this.tsb_CreatePermutation_Click);
            // 
            // tsb_Open
            // 
            this.tsb_Open.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Open.Image")));
            this.tsb_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Open.Name = "tsb_Open";
            this.tsb_Open.Size = new System.Drawing.Size(119, 28);
            this.tsb_Open.Text = "Open File";
            this.tsb_Open.Click += new System.EventHandler(this.tsb_Open_Click);
            // 
            // status_GAType
            // 
            this.status_GAType.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.status_GAType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_type});
            this.status_GAType.Location = new System.Drawing.Point(0, 616);
            this.status_GAType.Name = "status_GAType";
            this.status_GAType.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.status_GAType.Size = new System.Drawing.Size(1086, 22);
            this.status_GAType.TabIndex = 1;
            // 
            // tssl_type
            // 
            this.tssl_type.Name = "tssl_type";
            this.tssl_type.Size = new System.Drawing.Size(0, 15);
            // 
            // dlg_Open
            // 
            this.dlg_Open.FileName = "openFileDialog1";
            // 
            // splitContainer_Form
            // 
            this.splitContainer_Form.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer_Form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Form.Location = new System.Drawing.Point(0, 33);
            this.splitContainer_Form.Name = "splitContainer_Form";
            // 
            // splitContainer_Form.Panel2
            // 
            this.splitContainer_Form.Panel2.Controls.Add(this.splitContainer_User);
            this.splitContainer_Form.Size = new System.Drawing.Size(1086, 583);
            this.splitContainer_Form.SplitterDistance = 735;
            this.splitContainer_Form.TabIndex = 4;
            // 
            // splitContainer_User
            // 
            this.splitContainer_User.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer_User.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_User.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_User.Name = "splitContainer_User";
            this.splitContainer_User.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_User.Panel1
            // 
            this.splitContainer_User.Panel1.Controls.Add(this.dgv_SetUpTime);
            this.splitContainer_User.Panel1.Controls.Add(this.txb_Penalty);
            this.splitContainer_User.Panel1.Controls.Add(this.l_Penalty);
            this.splitContainer_User.Panel1.Controls.Add(this.l_description);
            // 
            // splitContainer_User.Panel2
            // 
            this.splitContainer_User.Panel2.Controls.Add(this.l_numOfSoFarUpdated);
            this.splitContainer_User.Panel2.Controls.Add(this.l_numOfIteration);
            this.splitContainer_User.Size = new System.Drawing.Size(345, 581);
            this.splitContainer_User.SplitterDistance = 267;
            this.splitContainer_User.TabIndex = 12;
            // 
            // dgv_SetUpTime
            // 
            this.dgv_SetUpTime.AccessibleName = "";
            this.dgv_SetUpTime.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_SetUpTime.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            this.dgv_SetUpTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SetUpTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_SetUpTime.Location = new System.Drawing.Point(0, 2);
            this.dgv_SetUpTime.Name = "dgv_SetUpTime";
            this.dgv_SetUpTime.RowHeadersWidth = 62;
            this.dgv_SetUpTime.RowTemplate.Height = 31;
            this.dgv_SetUpTime.Size = new System.Drawing.Size(345, 265);
            this.dgv_SetUpTime.TabIndex = 11;
            // 
            // txb_Penalty
            // 
            this.txb_Penalty.Location = new System.Drawing.Point(227, 19);
            this.txb_Penalty.Name = "txb_Penalty";
            this.txb_Penalty.Size = new System.Drawing.Size(86, 29);
            this.txb_Penalty.TabIndex = 10;
            this.txb_Penalty.Text = "300";
            // 
            // l_Penalty
            // 
            this.l_Penalty.AutoSize = true;
            this.l_Penalty.Location = new System.Drawing.Point(22, 24);
            this.l_Penalty.Name = "l_Penalty";
            this.l_Penalty.Size = new System.Drawing.Size(199, 18);
            this.l_Penalty.TabIndex = 9;
            this.l_Penalty.Text = "Positive Penalty Multiplier:";
            // 
            // l_description
            // 
            this.l_description.AutoSize = true;
            this.l_description.Location = new System.Drawing.Point(22, 52);
            this.l_description.Name = "l_description";
            this.l_description.Size = new System.Drawing.Size(301, 36);
            this.l_description.TabIndex = 8;
            this.l_description.Text = "The time required to set up each machine \r\nfor processing each job:\r\n";
            // 
            // l_numOfSoFarUpdated
            // 
            this.l_numOfSoFarUpdated.AutoSize = true;
            this.l_numOfSoFarUpdated.Location = new System.Drawing.Point(28, 100);
            this.l_numOfSoFarUpdated.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.l_numOfSoFarUpdated.Name = "l_numOfSoFarUpdated";
            this.l_numOfSoFarUpdated.Size = new System.Drawing.Size(0, 18);
            this.l_numOfSoFarUpdated.TabIndex = 3;
            // 
            // l_numOfIteration
            // 
            this.l_numOfIteration.AutoSize = true;
            this.l_numOfIteration.Location = new System.Drawing.Point(28, 68);
            this.l_numOfIteration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.l_numOfIteration.Name = "l_numOfIteration";
            this.l_numOfIteration.Size = new System.Drawing.Size(0, 18);
            this.l_numOfIteration.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 638);
            this.Controls.Add(this.splitContainer_Form);
            this.Controls.Add(this.toolStrip_NewGA);
            this.Controls.Add(this.status_GAType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "R11546028 Ass08";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip_NewGA.ResumeLayout(false);
            this.toolStrip_NewGA.PerformLayout();
            this.status_GAType.ResumeLayout(false);
            this.status_GAType.PerformLayout();
            this.splitContainer_Form.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Form)).EndInit();
            this.splitContainer_Form.ResumeLayout(false);
            this.splitContainer_User.Panel1.ResumeLayout(false);
            this.splitContainer_User.Panel1.PerformLayout();
            this.splitContainer_User.Panel2.ResumeLayout(false);
            this.splitContainer_User.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_User)).EndInit();
            this.splitContainer_User.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SetUpTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip_NewGA;
        private System.Windows.Forms.ToolStripButton tsb_CreateBinaryGA;
        private System.Windows.Forms.StatusStrip status_GAType;
        private System.Windows.Forms.ToolStripStatusLabel tssl_type;
        private System.Windows.Forms.ToolStripButton tsb_Open;
        private System.Windows.Forms.OpenFileDialog dlg_Open;
        private System.Windows.Forms.ToolStripButton tsb_CreatePermutation;
        private System.Windows.Forms.SplitContainer splitContainer_Form;
        private System.Windows.Forms.SplitContainer splitContainer_User;
        public System.Windows.Forms.DataGridView dgv_SetUpTime;
        public System.Windows.Forms.TextBox txb_Penalty;
        private System.Windows.Forms.Label l_Penalty;
        private System.Windows.Forms.Label l_description;
        private System.Windows.Forms.Label l_numOfIteration;
        public System.Windows.Forms.Label l_numOfSoFarUpdated;
    }
}

