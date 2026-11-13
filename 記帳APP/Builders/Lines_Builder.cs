using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Repository;

namespace 記帳APP.Builders
{
    [DisplayName("折線圖")]

    internal class Lines_Builder : ABuilder
    {

        private List<ShowAnalyzeDTO> resultList = new List<ShowAnalyzeDTO>();
        private List<ShowAnalyzeDTO> resultList2 = new List<ShowAnalyzeDTO>();

        private List<ShowAnalyzeDTO> InitialGetList()
        {
            List<ShowAnalyzeDTO> theList = new List<ShowAnalyzeDTO>();
            if (groups.Count != 0)
            {
                theList = newList.GroupBy(x => x.Date).Select(x =>
                {
                    var priceSum = x.Sum(y => int.Parse(y.Price));
                    var result = new ShowAnalyzeDTO
                    {
                        Date = x.Key,
                        Price = priceSum.ToString(),
                    };
                    return result;
                }).ToList();
            }
            else
            {
                int sum = newList.Sum(x => int.Parse(x.Price));
                ShowAnalyzeDTO item = new ShowAnalyzeDTO();
                item.Price = sum.ToString();
                item.Title = "總計";
                theList.Add(item);
            }
            return theList;
        }
        public override ABuilder GetRecord()
        {
            resultList = InitialGetList();
            return this;
        }

        public override ABuilder SetPeriod(string period)
        {
            if (period == "上週同期")
            {
                SetDateRange(fromDate.AddDays(-7), toDate.AddDays(-7));
            }

            else if (period == "上月同期")
            {
                SetDateRange(fromDate.AddMonths(-1), toDate.AddMonths(-1));
            }
            SetFilter(groups, filters);
            resultList2 = InitialGetList();
            return this;
        }


        public override Chart BuildPattern()
        {
            Chart chart1 = new Chart();
            List<string> x = new List<string>();
            List<double> y = new List<double>();

            List<string> x2 = new List<string>();
            List<double> y2 = new List<double>();


            resultList.ForEach(data =>
            {
                x.Add(data.Date.ToString());
                y.Add(double.Parse(data.Price));
            });

            resultList2.ForEach(data =>
            {
                x2.Add(data.Date.ToString());
                y2.Add(double.Parse(data.Price));
            });


            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();

            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            chart1.Series.Add(series2);
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Name = "chart1";
            chart1.TabIndex = 0;

            //控件背景
            chart1.BackColor = Color.Transparent;
            //图表区背景
            chart1.ChartAreas[0].BackColor = Color.Transparent;
            chart1.ChartAreas[0].BorderColor = Color.Transparent;

            //图例样式
            Legend legend2 = new Legend("#VALX");
            legend2.TitleBackColor = Color.Transparent;
            legend2.BackColor = Color.Transparent;
            legend2.TitleForeColor = Color.Blue;
            legend2.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            legend2.ForeColor = Color.Blue;

            chart1.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart1.Series[0].LabelForeColor = Color.Blue;

            chart1.Series[0].Label = "#VAL";                //设置显示X Y的值    
            chart1.Series[0].ToolTip = "#VALX:#VAL(元)";     //鼠标移动到对应点显示数值
            chart1.Series[0].ChartType = SeriesChartType.Line;    //图类型(折线)
            chart1.Series[0].LegendText = "支出金額";
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].CustomProperties = "PieLabelStyle = Outside";
            chart1.Series[0].Points.DataBindXY(x, y);

            chart1.Series[1].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart1.Series[1].LabelForeColor = Color.Blue;

            chart1.Series[1].Label = "#VAL";                //设置显示X Y的值    
            chart1.Series[1].ToolTip = "#VALX:#VAL(元)";     //鼠标移动到对应点显示数值
            chart1.Series[1].ChartType = SeriesChartType.Line;    //图类型(折线)
            chart1.Series[1].LegendText = "比較期間支出金額";
            chart1.Series[1].IsValueShownAsLabel = true;
            chart1.Series[1].CustomProperties = "PieLabelStyle = Outside";
            chart1.Series[1].Points.DataBindXY(x2, y2);

            chart1.Legends.Add(legend2);
            chart1.Legends[0].Position.Auto = true;
            chart1.Series[0].IsValueShownAsLabel = true;
            //是否显示图例
            chart1.Series[0].IsVisibleInLegend = true;
            //饼图折线
            chart1.Series[0]["PieLineColor"] = "Blue";
            //绑定数据
            return chart1;
        }
    }
}
