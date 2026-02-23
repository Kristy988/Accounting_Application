namespace 記帳APP.Views
{
    partial class 記帳本
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
            this.navbar1 = new 記帳APP.Components.Navbar();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.toDatePicker = new System.Windows.Forms.DateTimePicker();
            this.TimeFromLabel = new System.Windows.Forms.Label();
            this.TimeToLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // navbar1
            // 
            this.navbar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.navbar1.Location = new System.Drawing.Point(0, 370);
            this.navbar1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.navbar1.Name = "navbar1";
            this.navbar1.Size = new System.Drawing.Size(800, 80);
            this.navbar1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(633, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "查詢";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(32, 48);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 31;
            this.dataGridView1.Size = new System.Drawing.Size(693, 305);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // fromDatePicker
            // 
            this.fromDatePicker.Location = new System.Drawing.Point(71, 14);
            this.fromDatePicker.Name = "fromDatePicker";
            this.fromDatePicker.Size = new System.Drawing.Size(200, 22);
            this.fromDatePicker.TabIndex = 3;
            // 
            // toDatePicker
            // 
            this.toDatePicker.Location = new System.Drawing.Point(301, 14);
            this.toDatePicker.Name = "toDatePicker";
            this.toDatePicker.Size = new System.Drawing.Size(200, 22);
            this.toDatePicker.TabIndex = 4;
            // 
            // TimeFromLabel
            // 
            this.TimeFromLabel.AutoSize = true;
            this.TimeFromLabel.Location = new System.Drawing.Point(35, 21);
            this.TimeFromLabel.Name = "TimeFromLabel";
            this.TimeFromLabel.Size = new System.Drawing.Size(30, 12);
            this.TimeFromLabel.TabIndex = 5;
            this.TimeFromLabel.Text = "From";
            // 
            // TimeToLabel
            // 
            this.TimeToLabel.AutoSize = true;
            this.TimeToLabel.Location = new System.Drawing.Point(277, 21);
            this.TimeToLabel.Name = "TimeToLabel";
            this.TimeToLabel.Size = new System.Drawing.Size(18, 12);
            this.TimeToLabel.TabIndex = 6;
            this.TimeToLabel.Text = "To";
            // 
            // 記帳本
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TimeToLabel);
            this.Controls.Add(this.TimeFromLabel);
            this.Controls.Add(this.toDatePicker);
            this.Controls.Add(this.fromDatePicker);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.navbar1);
            this.Name = "記帳本";
            this.Text = "記帳本";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.Navbar navbar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker fromDatePicker;
        private System.Windows.Forms.DateTimePicker toDatePicker;
        private System.Windows.Forms.Label TimeFromLabel;
        private System.Windows.Forms.Label TimeToLabel;
    }
}