using CSV;
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

namespace 記帳APP.Repository
{
    internal class RecordRepository : IRecordRepository
    {
        string directoryPath = ConfigurationManager.AppSettings["DirectoryPath"];

        public void CreateRecord(RecordDAO data)
        {
            string folderPath = Path.Combine(directoryPath, data.Date);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            CSVHelper.Write(Path.Combine(directoryPath, data.Date, "data.csv"), data);
        }

        public void DeleteRecord(RecordDAO recordData)
        {

            List<RecordDAO> records = GetRecords(DateTime.Parse(recordData.Date));
            RecordDAO theRecord = records.FirstOrDefault(x => x.Picture1 == recordData.Picture1);
            records.Remove(theRecord);
            File.Delete(Path.Combine(directoryPath, recordData.Date, "data.csv"));

            if (records.Count > 0)
            {
                CSVHelper.Write(Path.Combine(directoryPath, recordData.Date, "data.csv"), records);
            }
            else
            {
                Directory.Delete(Path.Combine(directoryPath, recordData.Date));
            }
        }


        public void UpdateRecord(RecordDAO recordData)
        {
            List<RecordDAO> records = GetRecords(DateTime.Parse(recordData.Date));
            //RecordDAO theRecord = records.FirstOrDefault(x => x.Picture1 == recordData.Picture1);
            //records.Remove(theRecord); //刪除舊的資料
            //records.Add(recordData); //添加改過的資料
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Picture1 == recordData.Picture1)
                {
                    records[i] = recordData;
                    break;
                }
            }
            File.Delete(Path.Combine(directoryPath, recordData.Date, "data.csv"));

            if (records.Count > 0)
            {
                CSVHelper.Write(Path.Combine(directoryPath, recordData.Date, "data.csv"), records);
            }

        }

        public List<RecordDAO> GetRecords(DateTime fromDate, DateTime toDate)
        {
            List<RecordDAO> recordData = new List<RecordDAO>();
            TimeSpan diff = toDate - fromDate;
            int count = diff.Days;


            for (int i = 0; i < count + 1; i++)
            {

                if (File.Exists(Path.Combine(directoryPath, fromDate.AddDays(i).ToString("yyyy-MM-dd"), "data.csv")))
                {
                    recordData.AddRange(CSVHelper.Read<RecordDAO>(Path.Combine(directoryPath, fromDate.AddDays(i).ToString("yyyy-MM-dd"), "data.csv")));
                }
            }
            return recordData;
        }

        public List<RecordDAO> GetRecords(DateTime date)
        {
            List<RecordDAO> recordData = CSVHelper.Read<RecordDAO>(Path.Combine(directoryPath, date.ToString("yyyy-MM-dd"), "data.csv"));
            return recordData;
        }

    }
}
