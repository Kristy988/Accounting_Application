using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Repository;
using 記帳APP.Repository.Model;
using static 記帳APP.Contract.AddRecordContract;

namespace 記帳APP.Presenter
{
    internal class AddRecordPresenter : IAddRecordPresenter
    {
        string directoryPath = ConfigurationManager.AppSettings["DirectoryPath"];
        DataRepository dataRepository = new DataRepository();
        IAddRecordView view;
        RecordRepository recordRepository = new RecordRepository();
        public AddRecordPresenter(IAddRecordView view)
        {
            this.view = view;
        }
        public void GetComboBoxData()
        {
            DataDAO dataDAO = dataRepository.GetData();
            DataDTO dataDTO = new DataDTO();
            dataDTO.Category = dataDAO.Category;
            dataDTO.Subcategory = dataDAO.Subcategory;
            dataDTO.Target = dataDAO.Target;
            dataDTO.Payment = dataDAO.Payment;
            view.GetComboBoxDataResponse(dataDTO);

        }
        public void ChangeSubcatComboBox(string CategoryName)
        {
            view.GetSubcatComboBoxResponse(dataRepository.GetSubcatData(CategoryName));
        }
        public void CreateRecord(RecordDTO recordDTO)
        {
            string folderPath = Path.Combine(directoryPath, recordDTO.Date);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            RecordDAO recordDAO = new RecordDAO();
            recordDAO.Date = recordDTO.Date;
            recordDAO.Category = recordDTO.Category;
            recordDAO.Price = recordDTO.Price;
            recordDAO.Subcategory = recordDTO.Subcategory;
            recordDAO.Target = recordDTO.Target;
            recordDAO.Payment = recordDTO.Payment;
            recordDAO.Picture1 = PictureCompress(recordDTO, recordDTO.Picture1);
            recordDAO.Picture2 = PictureCompress(recordDTO, recordDTO.Picture2);
            recordRepository.CreateRecord(recordDAO);
        }
        string PictureCompress(RecordDTO recordDTO, Image Pic)
        {
            string pic1Guid = Guid.NewGuid().ToString();
            string pic1 = Path.Combine(directoryPath, recordDTO.Date, recordDTO.Date + pic1Guid + ".png");
            using (Bitmap bmp1 = new Bitmap(Pic))
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

            Bitmap originalImage = new Bitmap(Pic);

            int newWidth = 40;
            int newHeight = 40;

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            resizedImage.Save(Path.Combine(directoryPath, recordDTO.Date, "small_" + recordDTO.Date + pic1Guid + ".png"), ImageFormat.Jpeg);
            string newPath = Path.Combine(directoryPath, recordDTO.Date, "small_" + recordDTO.Date + pic1Guid + ".png");
            return newPath;
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
