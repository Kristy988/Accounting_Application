using CSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using 記帳APP.Models;

namespace 記帳APP.Views
{
    public partial class 記一筆 : Form
    {
        string directoryPath = ConfigurationManager.AppSettings["DirectoryPath"];
        string upPicPath = ConfigurationManager.AppSettings["UploadPath"];

        public 記一筆()
        {
            InitializeComponent();
        }

        private void 記一筆_Load(object sender, EventArgs e)
        {
            type_ComboBox.DataSource = DataModel.Category;
            subType_ComboBox.DataSource = DataModel.Subcategory[DataModel.Category[0]];
            targets_ComboBox.DataSource = DataModel.Target;
            payment_ComboBox.DataSource = DataModel.Payment;
            pictureBox1.Image = Image.FromFile(upPicPath);
            pictureBox2.Image = Image.FromFile(upPicPath);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void type_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            subType_ComboBox.DataSource = DataModel.Subcategory[type_ComboBox.Text];

        }
        private string imagePath1 = ConfigurationManager.AppSettings["UploadPath"];
        private string imagePath2 = ConfigurationManager.AppSettings["UploadPath"];
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
                if (pictureBox.Name == "pictureBox1")
                    imagePath1 = openFileDialog.FileName;
                if (pictureBox.Name == "pictureBox2")
                    imagePath2 = openFileDialog.FileName;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecordModel data = new RecordModel();
            data.Date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            data.Price = price_TextBox.Text;
            data.Category = type_ComboBox.Text;
            data.Subcategory = subType_ComboBox.Text;
            data.Target = targets_ComboBox.Text;
            data.Payment = payment_ComboBox.Text;
            data.Picture1 = imagePath1;
            data.Picture2 = imagePath2;

            string folderPath = Path.Combine(directoryPath, data.Date);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            string pic1Guid = Guid.NewGuid().ToString();
            string pic1 = Path.Combine(directoryPath, data.Date, data.Date + pic1Guid + ".png");
            using (Bitmap bmp1 = new Bitmap(pictureBox1.Image))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID  
                // for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.  
                // An EncoderParameters object has an array of EncoderParameter  
                // objects. In this case, there is only one  
                // EncoderParameter object in the array.  
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 15L);//品質0到100分的中間值50分
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(pic1, jpgEncoder, myEncoderParameters);

            }

            Bitmap originalImage = new Bitmap(pictureBox1.Image);

            int newWidth = 40;
            int newHeight = 40;

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            resizedImage.Save(Path.Combine(directoryPath, data.Date, "small_" + data.Date + pic1Guid + ".png"), ImageFormat.Jpeg);
            data.Picture1 = Path.Combine(directoryPath, data.Date, "small_" + data.Date + pic1Guid + ".png");


            string pic2Guid = Guid.NewGuid().ToString();
            string pic2 = Path.Combine(directoryPath, data.Date, data.Date + pic2Guid + ".png");
            using (Bitmap bmp1 = new Bitmap(pictureBox2.Image))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID  
                // for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.  
                // An EncoderParameters object has an array of EncoderParameter  
                // objects. In this case, there is only one  
                // EncoderParameter object in the array.  
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 15L);//品質0到100分的中間值50分
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(pic2, jpgEncoder, myEncoderParameters);

            }
            Bitmap originalImage2 = new Bitmap(pictureBox2.Image);

            int newWidth2 = 40;
            int newHeight2 = 40;

            Bitmap resizedImage2 = new Bitmap(newWidth2, newHeight2);

            using (Graphics g = Graphics.FromImage(resizedImage2))
            {
                g.DrawImage(originalImage2, 0, 0, newWidth2, newHeight2);
            }
            resizedImage2.Save(Path.Combine(directoryPath, data.Date, "small_" + data.Date + pic2Guid + ".png"), ImageFormat.Jpeg);
            data.Picture2 = Path.Combine(directoryPath, data.Date, "small_" + data.Date + pic2Guid + ".png");


            CSVHelper.Write(Path.Combine(directoryPath, data.Date, "data.csv"), data);

            MessageBox.Show("新增成功");
            pictureBox1.Image.Dispose();
            pictureBox2.Image.Dispose();
            GC.Collect();

            pictureBox1.Image = Image.FromFile(upPicPath);
            pictureBox2.Image = Image.FromFile(upPicPath);
            imagePath1 = upPicPath;
            imagePath2 = upPicPath;
        }


        private T SetValue<T>(string rawData) where T : class, new()
        {
            T t = new T();
            PropertyInfo[] props = t.GetType().GetProperties();
            string[] datas = rawData.Split(',');

            for (int i = 0; i < props.Length; i++)
            {
                props[i].SetValue(t, datas[i]);
            }
            return t;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
