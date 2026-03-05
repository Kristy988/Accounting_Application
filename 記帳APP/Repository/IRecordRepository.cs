using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Models;

namespace 記帳APP.Repository
{
    internal interface IRecordRepository
    {
        void CreateRecord(RecordDAO recordModel);
        void DeleteRecord(RecordDAO recordData);
        void UpdateRecord(RecordDAO recordData);
        List<RecordDAO> GetRecords(DateTime fromDate, DateTime toDate);
        List<RecordDAO> GetRecords(DateTime date);

    }
}
