using System.Windows.Forms;

namespace MyGALibrary
{
    partial class GAControl<T>
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.rtb_Iteration = new System.Windows.Forms.RichTextBox();
            this.rtb_SoFarTheBest = new System.Windows.Forms.RichTextBox();
            this.cht_Progress = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btn_RunOneIteration = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.cbx_ShowAnimation = new System.Windows.Forms.CheckBox();
            this.btn_RunToEnd = new System.Windows.Forms.Button();
            this.prg_GA = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cht_Progress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1043, 666);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.cht_Progress);
            this.splitContainer2.Size = new System.Drawing.Size(645, 666);
            this.splitContainer2.SplitterDistance = 218;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.rtb_Iteration);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.rtb_SoFarTheBest);
            this.splitContainer4.Size = new System.Drawing.Size(645, 218);
            this.splitContainer4.SplitterDistance = 152;
            this.splitContainer4.TabIndex = 4;
            // 
            // rtb_Iteration
            // 
            this.rtb_Iteration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Iteration.Location = new System.Drawing.Point(0, 0);
            this.rtb_Iteration.Margin = new System.Windows.Forms.Padding(4);
            this.rtb_Iteration.Name = "rtb_Iteration";
            this.rtb_Iteration.Size = new System.Drawing.Size(152, 218);
            this.rtb_Iteration.TabIndex = 2;
            this.rtb_Iteration.Text = "";
            // 
            // rtb_SoFarTheBest
            // 
            this.rtb_SoFarTheBest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_SoFarTheBest.Location = new System.Drawing.Point(0, 0);
            this.rtb_SoFarTheBest.Margin = new System.Windows.Forms.Padding(4);
            this.rtb_SoFarTheBest.Name = "rtb_SoFarTheBest";
            this.rtb_SoFarTheBest.Size = new System.Drawing.Size(489, 218);
            this.rtb_SoFarTheBest.TabIndex = 3;
            this.rtb_SoFarTheBest.Text = "";
            // 
            // cht_Progress
            // 
            chartArea3.Name = "ChartArea1";
            this.cht_Progress.ChartAreas.Add(chartArea3);
            this.cht_Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.cht_Progress.Legends.Add(legend3);
            this.cht_Progress.Location = new System.Drawing.Point(0, 0);
            this.cht_Progress.Name = "cht_Progress";
            series7.BorderWidth = 2;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.LimeGreen;
            series7.Legend = "Legend1";
            series7.Name = "Iteration-Average";
            series8.BorderWidth = 2;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = System.Drawing.Color.DodgerBlue;
            series8.Legend = "Legend1";
            series8.Name = "Iteration-Best";
            series9.BorderWidth = 2;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series9.Color = System.Drawing.Color.Firebrick;
            series9.Legend = "Legend1";
            series9.Name = "So-Far-The-Best";
            this.cht_Progress.Series.Add(series7);
            this.cht_Progress.Series.Add(series8);
            this.cht_Progress.Series.Add(series9);
            this.cht_Progress.Size = new System.Drawing.Size(645, 444);
            this.cht_Progress.TabIndex = 0;
            this.cht_Progress.Text = "chart1";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.prg_GA);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.splitContainer3.Panel2.Controls.Add(this.btn_RunToEnd);
            this.splitContainer3.Panel2.Controls.Add(this.cbx_ShowAnimation);
            this.splitContainer3.Panel2.Controls.Add(this.btn_Reset);
            this.splitContainer3.Panel2.Controls.Add(this.btn_RunOneIteration);
            this.splitContainer3.Size = new System.Drawing.Size(394, 666);
            this.splitContainer3.SplitterDistance = 273;
            this.splitContainer3.TabIndex = 9;
            // 
            // btn_RunOneIteration
            // 
            this.btn_RunOneIteration.Location = new System.Drawing.Point(48, 113);
            this.btn_RunOneIteration.Name = "btn_RunOneIteration";
            this.btn_RunOneIteration.Size = new System.Drawing.Size(135, 36);
            this.btn_RunOneIteration.TabIndex = 6;
            this.btn_RunOneIteration.Text = "One Iteration";
            this.btn_RunOneIteration.UseVisualStyleBackColor = true;
            this.btn_RunOneIteration.Click += new System.EventHandler(this.btn_RunOneIteration_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(48, 71);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(135, 36);
            this.btn_Reset.TabIndex = 4;
            this.btn_Reset.Text = "Reset";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // cbx_ShowAnimation
            // 
            this.cbx_ShowAnimation.AutoSize = true;
            this.cbx_ShowAnimation.Checked = true;
            this.cbx_ShowAnimation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_ShowAnimation.Location = new System.Drawing.Point(48, 28);
            this.cbx_ShowAnimation.Name = "cbx_ShowAnimation";
            this.cbx_ShowAnimation.Size = new System.Drawing.Size(142, 22);
            this.cbx_ShowAnimation.TabIndex = 8;
            this.cbx_ShowAnimation.Text = "show animation";
            this.cbx_ShowAnimation.UseVisualStyleBackColor = true;
            // 
            // btn_RunToEnd
            // 
            this.btn_RunToEnd.Location = new System.Drawing.Point(48, 155);
            this.btn_RunToEnd.Name = "btn_RunToEnd";
            this.btn_RunToEnd.Size = new System.Drawing.Size(135, 36);
            this.btn_RunToEnd.TabIndex = 7;
            this.btn_RunToEnd.Text = "Run to End";
            this.btn_RunToEnd.UseVisualStyleBackColor = true;
            this.btn_RunToEnd.Click += new System.EventHandler(this.btn_RunToEnd_Click);
            // 
            // prg_GA
            // 
            this.prg_GA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prg_GA.Location = new System.Drawing.Point(0, 0);
            this.prg_GA.Margin = new System.Windows.Forms.Padding(4);
            this.prg_GA.Name = "prg_GA";
            this.prg_GA.Size = new System.Drawing.Size(394, 273);
            this.prg_GA.TabIndex = 5;
            // 
            // GAControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "GAControl";
            this.Size = new System.Drawing.Size(1043, 666);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cht_Progress)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        public RichTextBox rtb_SoFarTheBest;
        public RichTextBox rtb_Iteration;
        public System.Windows.Forms.DataVisualization.Charting.Chart cht_Progress;
        private SplitContainer splitContainer3;
        public PropertyGrid prg_GA;
        public Button btn_RunToEnd;
        public CheckBox cbx_ShowAnimation;
        public Button btn_Reset;
        public Button btn_RunOneIteration;
    }
}
