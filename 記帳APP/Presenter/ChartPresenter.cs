using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳APP.Builders;
using 記帳APP.Components;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Presenter.Service;
using 記帳APP.Repository;
using 記帳APP.Utility;
using static 記帳APP.Contract.ChartContract;


namespace 記帳APP.Presenter
{
    internal class ChartPresenter : IChartPresenter
    {
        IChartView chartView;

        public ChartPresenter(IChartView chartView)
        {
            this.chartView = chartView;
        }
        public void GetRecord(DateTime fromDate, DateTime toDate, List<string> groups, Dictionary<string, List<string>> filters, string distin, string period = "none")
        {
            var theBuilder = Assembly.GetExecutingAssembly().DefinedTypes
                .FirstOrDefault(x => x.BaseType == typeof(ABuilder) &&
                x.GetCustomAttribute<DisplayNameAttribute>().DisplayName == distin);
            ABuilder builder = (ABuilder)Activator.CreateInstance(theBuilder);
            builder.SetDateRange(fromDate, toDate);
            builder.SetFilter(groups, filters);
            builder.GetRecord();
            builder.SetPeriod(period);

            chartView.GetChartResponse(builder.BuildPattern());


        }

    }
}

