using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Builders;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Presenter.Service;
using 記帳APP.Repository;
using 記帳APP.Utility;
using static 記帳APP.Contract.ChartContract;
using System.Windows.Forms.DataVisualization.Charting;


namespace 記帳APP.Presenter
{
    internal class ChartPresenter : IChartPresenter
    {
        IChartView chartView;

        public ChartPresenter(IChartView chartView)
        {
            this.chartView = chartView;
        }
        //public void GetRecord(DateTime fromDate, DateTime toDate, List<string> groups, Dictionary<string, List<string>> filters, string distin, string period = "none")
        //{
        //    AnalyzeService service = new AnalyzeService();
        //    if (distin == "stack")
        //    {
        //        chartView.GetRecordResponse(service.Stack_GetRecord(fromDate, toDate, groups, filters, distin));
        //    }
        //    else if (period == "Month")
        //    {
        //        chartView.GetRecordResponse(service.GetRecord(fromDate, toDate, groups, filters, distin), service.GetRecord(fromDate.AddMonths(-1), toDate.AddMonths(-1), groups, filters, distin));
        //    }
        //    else if (period == "Week")
        //    {
        //        chartView.GetRecordResponse(service.GetRecord(fromDate, toDate, groups, filters, distin), service.GetRecord(fromDate.AddDays(-7), toDate.AddDays(-7), groups, filters, distin));
        //    }

        //    else
        //        chartView.GetRecordResponse(service.GetRecord(fromDate, toDate, groups, filters, distin), service.GetRecord(fromDate, toDate, groups, filters, distin));
        //}

        public void GetRecord(DateTime fromDate, DateTime toDate, List<string> groups, Dictionary<string, List<string>> filters, string distin, string period = "none")
        {
            if (distin == "pie")
            {
                ABuilder builder = new Pie_Builder();
                builder.SetDateRange(fromDate, toDate);
                builder.SetFilter(groups, filters);
                builder.SetPeriod();
                builder.GetRecord();
                chartView.GetChartResponse(builder.BuildPattern());
            }
            else if (distin == "stack")
            {
                ABuilder builder = new Stack_Builder();
                builder.SetDateRange(fromDate, toDate);
                builder.SetFilter(groups, filters);
                builder.SetPeriod();
                builder.GetRecord();

                chartView.GetChartResponse(builder.BuildPattern());
            }
            else if (distin == "lines")
            {
                ABuilder builder = new Lines_Builder();
                builder.SetDateRange(fromDate, toDate);
                builder.SetFilter(groups, filters);
                builder.GetRecord();
                builder.SetPeriod(period);

                chartView.GetChartResponse(builder.BuildPattern());
            }
        }

    }
}

