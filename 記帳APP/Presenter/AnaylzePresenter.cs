using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Presenter.Service;
using 記帳APP.Repository;
using 記帳APP.Utility;
using static 記帳APP.Contract.AccountBookContract;
using static 記帳APP.Contract.AnalyzeContract;
using static 記帳APP.Contract.ChartContract;

namespace 記帳APP.Presenter
{
    internal class AnaylzePresenter : IAnalyzePresenter

    {
        IAnalyzeView analyzeView;
        public AnaylzePresenter(IAnalyzeView analyzeView)
        {
            this.analyzeView = analyzeView;
        }
        public void GetRecord(DateTime fromDate, DateTime toDate, List<string> groups, Dictionary<string, List<string>> filters)
        {
            AnalyzeService service = new AnalyzeService();
            analyzeView.GetRecordResponse(service.GetRecord(fromDate, toDate, groups, filters));
        }


    }
}
