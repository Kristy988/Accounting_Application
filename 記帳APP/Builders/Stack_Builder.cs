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
using 記帳APP.Models.DTOs;
using 記帳APP.Presenter.Service;

namespace 記帳APP.Builders
{
    [DisplayName("堆疊圖")]

    internal class Stack_Builder : ABuilder
    {
        private Dictionary<string, ShowAnalyzeDTO> resultList;
        public override ABuilder GetRecord()
        {
            //var resultList = new Dictionary<string, ShowAnalyzeDTO>();
            var dateList = newList.Select(x => x.Date).Distinct().OrderBy(x => x).ToList();
            resultList = newList.GroupBy(x =>
            {
                string theGroup = "";
                attributeAfterFilter.ForEach(y =>
                {
                    string title = y.GetCustomAttribute<DisplayNameAttribute>().DisplayName;

                    //假如有這個群組的話
                    if (groups.Contains(title))
                    {
                        theGroup += y.GetValue(x).ToString() + "/";
                    }
                });
                theGroup = theGroup.TrimEnd('/');

                return theGroup;
            }).ToDictionary(x => x.Key, y =>
            {
                var prices = dateList.Select(date => y.Where(z => z.Date == date).Sum(z => int.Parse(z.Price))).ToList();
                var result = new ShowAnalyzeDTO
                {
                    DateList = dateList,
                    PriceList = prices,
                };
                return result;
            });

            return this;
        }

        public override Chart BuildPattern()
        {

            Chart chart1 = new Chart();
            List<string> x = resultList.Select(dates => dates.Value.DateList).FirstOrDefault();
            if (x == null) { return null; }
            var res = resultList.Select(data =>
            {
                var result = new
                {
                    Title = data.Key,
                    Values = data.Value.PriceList,
                };
                return result;
            }).ToList();

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            for (int i = 0; i < res.Count; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
                series1.ChartArea = "ChartArea1";
                series1.Legend = "Legend1";
                series1.Name = $"Series{i + 1}";
                chart1.Series.Add(series1);
            }
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new System.Drawing.Point(38, 61);
            chart1.Name = "chart1";
            chart1.Size = new System.Drawing.Size(572, 377);
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


            for (int i = 0; i < res.Count; i++)
            {
                chart1.Series[i].Label = "#VAL";                //设置显示X Y的值    
                chart1.Series[i].ToolTip = "#VALX:#VAL(元)";     //鼠标移动到对应点显示数值
                chart1.Series[i].XValueType = ChartValueType.String;  //设置X轴上的值类型
                chart1.Series[i].LabelForeColor = Color.Blue;

                chart1.Series[i].ChartType = SeriesChartType.StackedColumn;
                chart1.Series[i].LegendText = res[i].Title;
                chart1.Series[i].IsValueShownAsLabel = true;
                chart1.Series[i].CustomProperties = "PieLabelStyle = Outside";

                chart1.Series[i].Points.DataBindXY(x, res[i].Values);
            }

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
