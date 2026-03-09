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
using 記帳APP.Utility;
using static 記帳APP.Contract.AddRecordContract;

namespace 記帳APP.Presenter
{
    internal class AddRecordPresenter : IAddRecordPresenter
    {
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
            string folderPath = Path.Combine(Program.DirectoryPath, recordDTO.Date);
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
            recordDAO.Picture1 = SaveImage(folderPath, recordDTO.Picture1);
            recordDAO.Picture2 = SaveImage(folderPath, recordDTO.Picture2);
            recordRepository.CreateRecord(recordDAO);
        }
        public string SaveImage(string folderPath, Image pic)
        {
            string pic1Guid = Guid.NewGuid().ToString();
            string pic1 = Path.Combine(folderPath, pic1Guid + ".png");
            string newPath = Path.Combine(folderPath, "small_" + pic1Guid + ".png");

            Bitmap image = ImageCompress.CompressImage(pic);
            image.Save(pic1);

            Bitmap imageSmall = ImageCompress.CompressSmallImage(pic, 40, 40);
            imageSmall.Save(newPath);

            return newPath;

        }

    }
}
