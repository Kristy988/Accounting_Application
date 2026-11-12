namespace 記帳APP.Views
{
    partial class 圖表分析
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TimeFromLabel = new System.Windows.Forms.Label();
            this.AnalyzeBTN = new System.Windows.Forms.Button();
            this.toDatePicker = new System.Windows.Forms.DateTimePicker();
            this.fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.ChartType = new System.Windows.Forms.ComboBox();
            this.Compairision = new System.Windows.Forms.ComboBox();
            this.navbar1 = new 記帳APP.Components.Navbar();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 169);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(502, 437);
            this.flowLayoutPanel2.TabIndex = 18;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 73);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(502, 58);
            this.flowLayoutPanel1.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(311, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 151);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "篩選條件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "群組分類";
            // 
            // TimeFromLabel
            // 
            this.TimeFromLabel.AutoSize = true;
            this.TimeFromLabel.Location = new System.Drawing.Point(9, 2);
            this.TimeFromLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimeFromLabel.Name = "TimeFromLabel";
            this.TimeFromLabel.Size = new System.Drawing.Size(38, 15);
            this.TimeFromLabel.TabIndex = 16;
            this.TimeFromLabel.Text = "From";
            // 
            // AnalyzeBTN
            // 
            this.AnalyzeBTN.Location = new System.Drawing.Point(970, 12);
            this.AnalyzeBTN.Name = "AnalyzeBTN";
            this.AnalyzeBTN.Size = new System.Drawing.Size(75, 36);
            this.AnalyzeBTN.TabIndex = 13;
            this.AnalyzeBTN.Text = "分析";
            this.AnalyzeBTN.UseVisualStyleBackColor = true;
            this.AnalyzeBTN.Click += new System.EventHandler(this.AnalyzeBTN_Click);
            // 
            // toDatePicker
            // 
            this.toDatePicker.Location = new System.Drawing.Point(314, 20);
            this.toDatePicker.Name = "toDatePicker";
            this.toDatePicker.Size = new System.Drawing.Size(200, 25);
            this.toDatePicker.TabIndex = 12;
            // 
            // fromDatePicker
            // 
            this.fromDatePicker.Location = new System.Drawing.Point(12, 20);
            this.fromDatePicker.Name = "fromDatePicker";
            this.fromDatePicker.Size = new System.Drawing.Size(200, 25);
            this.fromDatePicker.TabIndex = 11;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Location = new System.Drawing.Point(544, 73);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(511, 533);
            this.flowLayoutPanel3.TabIndex = 20;
            // 
            // ChartType
            // 
            this.ChartType.FormattingEnabled = true;
            this.ChartType.Items.AddRange(new object[] {
            "圓餅圖",
            "折線圖",
            "堆疊圖"});
            this.ChartType.Location = new System.Drawing.Point(544, 20);
            this.ChartType.Name = "ChartType";
            this.ChartType.Size = new System.Drawing.Size(272, 23);
            this.ChartType.TabIndex = 23;
            this.ChartType.Text = "選擇圖表";
            this.ChartType.SelectedIndexChanged += new System.EventHandler(this.ChartType_SelectedIndexChanged);
            // 
            // Compairision
            // 
            this.Compairision.FormattingEnabled = true;
            this.Compairision.Items.AddRange(new object[] {
            "上週同期",
            "上月同期"});
            this.Compairision.Location = new System.Drawing.Point(833, 20);
            this.Compairision.Name = "Compairision";
            this.Compairision.Size = new System.Drawing.Size(122, 23);
            this.Compairision.TabIndex = 24;
            this.Compairision.Text = "時間區間";
            // 
            // navbar1
            // 
            this.navbar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.navbar1.Location = new System.Drawing.Point(0, 638);
            this.navbar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.navbar1.Name = "navbar1";
            this.navbar1.Size = new System.Drawing.Size(1067, 109);
            this.navbar1.TabIndex = 0;
            // 
            // 圖表分析
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 747);
            this.Controls.Add(this.Compairision);
            this.Controls.Add(this.ChartType);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimeFromLabel);
            this.Controls.Add(this.AnalyzeBTN);
            this.Controls.Add(this.toDatePicker);
            this.Controls.Add(this.fromDatePicker);
            this.Controls.Add(this.navbar1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "圖表分析";
            this.Text = "圖表分析";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.Navbar navbar1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TimeFromLabel;
        private System.Windows.Forms.Button AnalyzeBTN;
        private System.Windows.Forms.DateTimePicker toDatePicker;
        private System.Windows.Forms.DateTimePicker fromDatePicker;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.ComboBox ChartType;
        private System.Windows.Forms.ComboBox Compairision;
    }
}