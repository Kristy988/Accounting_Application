using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Models.DTOs;

namespace 記帳APP.Contract
{
    internal class AnalyzeContract
    {
        internal interface IAnalyzeView
        {
            void GetRecordResponse(List<ShowAnalyzeDTO> showAnalyzeDTO);
        }
        internal interface IAnalyzePresenter
        {
            void GetRecord(DateTime fromDate, DateTime toDate, List<string> groups, Dictionary<string, List<string>> filters);
        }
    }
}
