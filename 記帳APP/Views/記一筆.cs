using CSV;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Presenter;
using static 記帳APP.Contract.AddRecordContract;

namespace 記帳APP.Views
{
    public partial class 記一筆 : Form, IAddRecordView
    {
        private string imagePath1 = ConfigurationManager.AppSettings["UploadPath"];
        private string imagePath2 = ConfigurationManager.AppSettings["UploadPath"];
        IAddRecordPresenter addRecordPresenter;
        public 記一筆()
        {
            InitializeComponent();
            addRecordPresenter = new AddRecordPresenter(this);
            addRecordPresenter.GetComboBoxData();
        }
        void IAddRecordView.GetComboBoxDataResponse(DataDTO dataDTO)
        {
            type_ComboBox.DataSource = dataDTO.Category;
            subType_ComboBox.DataSource = dataDTO.Subcategory[dataDTO.Category[0]];
            targets_ComboBox.DataSource = dataDTO.Target;
            payment_ComboBox.DataSource = dataDTO.Payment;
            pictureBox1.Image = Image.FromFile(imagePath1);
            pictureBox2.Image = Image.FromFile(imagePath2);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void type_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            addRecordPresenter.ChangeSubcatComboBox(type_ComboBox.Text);

        }
        public void GetSubcatComboBoxResponse(List<string> Subcates)
        {
            subType_ComboBox.DataSource = Subcates;
        }


        private void ImageUpload_Click(object sender, EventArgs e)
        {

            PictureBox pictureBox = (PictureBox)sender;
            pictureBox.Image.Dispose();
            GC.Collect();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\krist\Downloads";
            openFileDialog.Filter = "圖片檔|*.png;*.jpg;*.gif";
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(openFileDialog.FileName);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecordDTO data = new RecordDTO();
            //data.Date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            panel1.Controls.OfType<Control>()
                           .ToList()
                           .ForEach(x =>
                           {
                               PropertyInfo propertyInfo = typeof(RecordDTO).GetProperty(x.Tag.ToString());
                               if (x is DateTimePicker)
                               {
                                   propertyInfo.SetValue(data, ((DateTimePicker)x).Value.ToString("yyyy-MM-dd"));
                               }
                               if (x is TextBox)
                               {
                                   propertyInfo.SetValue(data, x.Text);
                               }
                               if (x is ComboBox)
                               {
                                   propertyInfo.SetValue(data, x.Text);
                               }
                               if (x is PictureBox)
                               {
                                   propertyInfo.SetValue(data, ((PictureBox)x).Image);
                               }
                           });

            addRecordPresenter.CreateRecord(data);
            MessageBox.Show("新增成功");
            pictureBox1.Image.Dispose();
            pictureBox2.Image.Dispose();
            GC.Collect();

            pictureBox1.Image = Image.FromFile(imagePath1);
            pictureBox2.Image = Image.FromFile(imagePath2);

        }

    }

}
