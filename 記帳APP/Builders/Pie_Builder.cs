using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳APP.Models.DTOs;

namespace 記帳APP.Builders
{
    internal class Pie_Builder : ABuilder
    {
        private List<ShowAnalyzeDTO> resultList = new List<ShowAnalyzeDTO>();

        public override ABuilder GetRecord()
        {
            if (groups.Count != 0)
            {
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
                }).Select(x =>
                {
                    var priceSum = x.Sum(y => int.Parse(y.Price));
                    var result = new ShowAnalyzeDTO
                    {

                        Price = priceSum.ToString(),
                        Title = x.Key,
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
                resultList.Add(item);
            }
            return this;
        }
        public override Chart BuildPattern()
        {
            Chart chart1 = new Chart();
            List<string> x = new List<string>();
            List<double> y = new List<double>();
            resultList.ForEach(data =>
            {
                x.Add(data.Title);

                y.Add(double.Parse(data.Price));

            });
            var total = y.Sum();
            chart1.Titles.Add("圓餅圖分析");
            chart1.Titles[0].ForeColor = Color.Blue;
            chart1.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart1.Titles[0].Alignment = ContentAlignment.TopCenter;
            chart1.Titles.Add($"總計：{total} 元");
            chart1.Titles[1].ForeColor = Color.Blue;
            chart1.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            chart1.Titles[1].Alignment = ContentAlignment.TopRight;


            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            //chart1.Location = new System.Drawing.Point(38, 61);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            //chart1.Size = new System.Drawing.Size(572, 377);
            chart1.TabIndex = 0;
            chart1.Text = "chart1";

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
            chart1.Series[0].Label = "#VAL (#PERCENT)";                //设置显示X Y的值    
            chart1.Series[0].LabelForeColor = Color.Blue;
            chart1.Series[0].ToolTip = "#VALX:#VAL(元)";     //鼠标移动到对应点显示数值
            chart1.Series[0].ChartType = SeriesChartType.Pie;    //图类型(圓餅)

            chart1.Series[0].LegendText = legend2.Name;
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].CustomProperties = "PieLabelStyle = Outside";
            chart1.Legends.Add(legend2);
            chart1.Legends[0].Position.Auto = true;
            chart1.Series[0].IsValueShownAsLabel = true;
            //是否显示图例
            chart1.Series[0].IsVisibleInLegend = true;
            //饼图折线
            chart1.Series[0]["PieLineColor"] = "Blue";
            //绑定数据
            chart1.Series[0].Points.DataBindXY(x, y);
            return chart1;
        }


    }
}
