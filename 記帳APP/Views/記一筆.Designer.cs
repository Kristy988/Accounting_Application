namespace 記帳APP.Views
{
    partial class 記一筆
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
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.price_TextBox = new System.Windows.Forms.TextBox();
            this.type_ComboBox = new System.Windows.Forms.ComboBox();
            this.subType_ComboBox = new System.Windows.Forms.ComboBox();
            this.targets_ComboBox = new System.Windows.Forms.ComboBox();
            this.payment_ComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.navbar1 = new 記帳APP.Components.Navbar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(16, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "日期";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(18, 13);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(188, 25);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Tag = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(16, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "金額";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(16, 178);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 31);
            this.label3.TabIndex = 1;
            this.label3.Text = "消費類型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(16, 235);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 31);
            this.label4.TabIndex = 1;
            this.label4.Text = "消費目的";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(16, 286);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 31);
            this.label5.TabIndex = 1;
            this.label5.Text = "消費對象";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(16, 342);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 31);
            this.label6.TabIndex = 1;
            this.label6.Text = "付款方式";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(385, 18);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 31);
            this.label7.TabIndex = 1;
            this.label7.Text = "發票收據1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(727, 18);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 31);
            this.label8.TabIndex = 1;
            this.label8.Text = "發票收據2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 391);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 44);
            this.button1.TabIndex = 5;
            this.button1.Text = "儲存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // price_TextBox
            // 
            this.price_TextBox.Location = new System.Drawing.Point(18, 63);
            this.price_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.price_TextBox.Name = "price_TextBox";
            this.price_TextBox.Size = new System.Drawing.Size(188, 25);
            this.price_TextBox.TabIndex = 3;
            this.price_TextBox.Tag = "Price";
            // 
            // type_ComboBox
            // 
            this.type_ComboBox.FormattingEnabled = true;
            this.type_ComboBox.Location = new System.Drawing.Point(18, 126);
            this.type_ComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.type_ComboBox.Name = "type_ComboBox";
            this.type_ComboBox.Size = new System.Drawing.Size(185, 23);
            this.type_ComboBox.TabIndex = 4;
            this.type_ComboBox.Tag = "Category";
            this.type_ComboBox.SelectedIndexChanged += new System.EventHandler(this.type_ComboBox_SelectedIndexChanged);
            // 
            // subType_ComboBox
            // 
            this.subType_ComboBox.FormattingEnabled = true;
            this.subType_ComboBox.Location = new System.Drawing.Point(18, 183);
            this.subType_ComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.subType_ComboBox.Name = "subType_ComboBox";
            this.subType_ComboBox.Size = new System.Drawing.Size(185, 23);
            this.subType_ComboBox.TabIndex = 4;
            this.subType_ComboBox.Tag = "Subcategory";
            // 
            // targets_ComboBox
            // 
            this.targets_ComboBox.FormattingEnabled = true;
            this.targets_ComboBox.Location = new System.Drawing.Point(18, 235);
            this.targets_ComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.targets_ComboBox.Name = "targets_ComboBox";
            this.targets_ComboBox.Size = new System.Drawing.Size(185, 23);
            this.targets_ComboBox.TabIndex = 4;
            this.targets_ComboBox.Tag = "Target";
            // 
            // payment_ComboBox
            // 
            this.payment_ComboBox.FormattingEnabled = true;
            this.payment_ComboBox.Location = new System.Drawing.Point(18, 290);
            this.payment_ComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.payment_ComboBox.Name = "payment_ComboBox";
            this.payment_ComboBox.Size = new System.Drawing.Size(185, 23);
            this.payment_ComboBox.TabIndex = 4;
            this.payment_ComboBox.Tag = "Payment";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(264, 26);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(276, 278);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "Picture1";
            this.pictureBox1.Click += new System.EventHandler(this.ImageUpload_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(606, 26);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(276, 278);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "Picture2";
            this.pictureBox2.Click += new System.EventHandler(this.ImageUpload_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.payment_ComboBox);
            this.panel1.Controls.Add(this.targets_ComboBox);
            this.panel1.Controls.Add(this.subType_ComboBox);
            this.panel1.Controls.Add(this.type_ComboBox);
            this.panel1.Controls.Add(this.price_TextBox);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Location = new System.Drawing.Point(127, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(903, 320);
            this.panel1.TabIndex = 8;
            // 
            // navbar1
            // 
            this.navbar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.navbar1.Location = new System.Drawing.Point(0, 440);
            this.navbar1.Margin = new System.Windows.Forms.Padding(1);
            this.navbar1.Name = "navbar1";
            this.navbar1.Size = new System.Drawing.Size(1057, 98);
            this.navbar1.TabIndex = 0;
            // 
            // 記一筆
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 538);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.navbar1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "記一筆";
            this.Text = "記一筆";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.Navbar navbar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox price_TextBox;
        private System.Windows.Forms.ComboBox type_ComboBox;
        private System.Windows.Forms.ComboBox subType_ComboBox;
        private System.Windows.Forms.ComboBox targets_ComboBox;
        private System.Windows.Forms.ComboBox payment_ComboBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
    }
}

