using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Models.DTOs;
using System.Windows.Forms.DataVisualization.Charting;


namespace 記帳APP.Contract
{
    internal class ChartContract
    {
        internal interface IChartView
        {
            //void GetRecordResponse(List<ShowAnalyzeDTO> showAnalyzeDTO);
            //void GetRecordResponse(List<ShowAnalyzeDTO> showAnalyzeDTO, List<ShowAnalyzeDTO> showAnalyzeDTO2);
            //void GetRecordResponse(Dictionary<string, ShowAnalyzeDTO> showAnalyzeDTO);
            void GetChartResponse(Chart chart);
        }
        internal interface IChartPresenter
        {
            void GetRecord(DateTime fromDate, DateTime toDate, List<string> groups, Dictionary<string, List<string>> filters, string distin, string period = "none");
        }
    }
}
