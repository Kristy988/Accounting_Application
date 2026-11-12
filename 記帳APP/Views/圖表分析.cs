using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳APP.Extension;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Presenter;
using static 記帳APP.Contract.AnalyzeContract;
using static 記帳APP.Contract.ChartContract;

namespace 記帳APP.Views
{
    public partial class 圖表分析 : Form, IChartView
    {
        IChartPresenter chartPresenter;

        List<string> groups = new List<string>(); // 類型: 食衣住行育樂
        Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();

        public 圖表分析()
        {
            InitializeComponent();
            chartPresenter = new ChartPresenter(this);
            flowLayoutPanel1.GroupCheckBoxGenerated(OnCheckGroups, OnCheckFilters);
            flowLayoutPanel2.CheckBoxGenerated(OnCheckGroups, OnCheckFilters);
        }
        public void OnCheckGroups(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            //FlowLayoutPanel theFlow = checkBox.Parent as FlowLayoutPanel;
            bool isExist = groups.Any(x => x == checkBox.Text);

            if (checkBox.Checked)
            {
                if (!isExist)
                    groups.Add(checkBox.Text);
            }
            else
            {
                if (isExist)
                    groups.Remove(checkBox.Text);

            }
        }

        public void OnCheckFilters(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            FlowLayoutPanel theFlow = checkBox.Parent as FlowLayoutPanel;

            if (checkBox.Checked)
            {
                if (filters.Count == 0)
                {
                    filters.Add(theFlow.Name, new List<string> { checkBox.Text });
                }
                else
                {
                    if (filters.ContainsKey(theFlow.Name))
                    {
                        bool isExist = filters.Values.Any(x => x.Contains(checkBox.Text));
                        if (!isExist)
                            filters[theFlow.Name].Add(checkBox.Text);
                    }
                    else
                    {
                        filters.Add(theFlow.Name, new List<string> { checkBox.Text });
                    }
                }
            }

            else
            {
                //找出非全選的其他選項
                var otherCheck = theFlow.Controls.OfType<CheckBox>().Where(x => x.Name != theFlow.Name).ToList();
                //只要有一個打勾 則該類別顯示true 反之false
                var checkStatus = otherCheck.Any(x => x.Checked);
                if (checkStatus)
                {
                    //只刪除打勾選項
                    foreach (var item in filters)
                    {
                        item.Value.Remove(checkBox.Text);
                    }
                }
                //該類別已沒有任何選項 刪除其Key
                else
                {
                    filters.Remove(theFlow.Name);
                }
            }

        }



        //void IChartView.GetRecordResponse(List<ShowAnalyzeDTO> showAnalyzeDTOs, List<ShowAnalyzeDTO> showAnalyzeDTO2)
        //{
        //    if (ChartType.Text == "圓餅圖")
        //    {
        //        GeneratePieChart(showAnalyzeDTOs);
        //    }
        //    else if (ChartType.Text == "折線圖")
        //    {
        //        GenerateLinesChart(showAnalyzeDTOs, showAnalyzeDTO2);
        //    }

        //}
        //void IChartView.GetRecordResponse(Dictionary<string, ShowAnalyzeDTO> showAnalyzeDTOs)
        //{
        //    if (ChartType.Text == "堆疊圖")
        //    {
        //        GenerateStackChart(showAnalyzeDTOs);
        //    }
        //}
        void IChartView.GetChartResponse(Chart chart)
        {
            flowLayoutPanel3.Controls.Clear();
            chart.Width = flowLayoutPanel3.Width;
            flowLayoutPanel3.Controls.Add(chart);

        }

        private void AnalyzeBTN_Click(object sender, EventArgs e)
        {
            this.Debounce(() =>
            {
                DateTime fromDate = fromDatePicker.Value;
                DateTime toDate = toDatePicker.Value;
                if (ChartType.Text == "圓餅圖")
                {
                    chartPresenter.GetRecord(fromDate, toDate, groups, filters, "pie");
                }
                else if (Compairision.Text == "上月同期")
                    chartPresenter.GetRecord(fromDate, toDate, groups, filters, "lines", "Month");
                else if (Compairision.Text == "上週同期")
                    chartPresenter.GetRecord(fromDate, toDate, groups, filters, "lines", "Week");
                else if (ChartType.Text == "堆疊圖")
                    chartPresenter.GetRecord(fromDate, toDate, groups, filters, "stack", "none");

            }, 400);
        }


        private void GeneratePieChart(List<ShowAnalyzeDTO> showAnalyzeDTOs)
        {
            flowLayoutPanel3.Controls.Clear();
            Chart chart1 = new Chart();
            chart1.Width = flowLayoutPanel3.Width;
            List<string> x = new List<string>();
            List<double> y = new List<double>();
            showAnalyzeDTOs.ForEach(data =>
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
            flowLayoutPanel3.Controls.Add(chart1);
        }

        //只看日期與filter選擇的項目
        private void GenerateLineChartbyDate(List<ShowAnalyzeDTO> showAnalyzeDTOs)
        {
            flowLayoutPanel3.Controls.Clear();
            Chart chart1 = new Chart();
            chart1.Width = flowLayoutPanel3.Width;
            List<string> x = new List<string>();
            List<double> y = new List<double>();
            showAnalyzeDTOs.ForEach(data =>
            {
                x.Add(data.Date);
                y.Add(double.Parse(data.Price));

            });

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();

            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);

            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            //chart1.Location = new System.Drawing.Point(38, 61);
            chart1.Name = "chart1";
            //chart1.Size = new System.Drawing.Size(572, 377);
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



            chart1.Legends.Add(legend2);
            chart1.Legends[0].Position.Auto = true;
            chart1.Series[0].IsValueShownAsLabel = true;
            //是否显示图例
            chart1.Series[0].IsVisibleInLegend = true;
            //饼图折线
            chart1.Series[0]["PieLineColor"] = "Blue";
            //绑定数据
            flowLayoutPanel3.Controls.Add(chart1);
        }
        //做出時間區隔
        private void GenerateLinesChart(List<ShowAnalyzeDTO> showAnalyzeDTOs, List<ShowAnalyzeDTO> showAnalyzeDTO2)
        {
            flowLayoutPanel3.Controls.Clear();
            Chart chart1 = new Chart();
            chart1.Width = flowLayoutPanel3.Width;
            List<string> x = new List<string>();
            List<double> y = new List<double>();

            List<string> x2 = new List<string>();
            List<double> y2 = new List<double>();


            showAnalyzeDTOs.ForEach(data =>
            {
                x.Add(data.Date.ToString());
                y.Add(double.Parse(data.Price));
            });

            showAnalyzeDTO2.ForEach(data =>
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
            flowLayoutPanel3.Controls.Add(chart1);
        }
        private void GenerateLineChartbyMonth(List<ShowAnalyzeDTO> showAnalyzeDTOs)
        {
            flowLayoutPanel3.Controls.Clear();
            Chart chart1 = new Chart();
            chart1.Width = flowLayoutPanel3.Width;
            List<string> x = new List<string>();
            List<double> y = new List<double>();
            showAnalyzeDTOs.GroupBy(time => DateTime.Parse(time.Date).Month)
            .Select(data =>
            {
                double sum = data.Sum(p => int.Parse(p.Price));
                var result = new
                {
                    Price = sum,
                    Date = data.Key,
                };
                return result;
            }).ToList().ForEach(data =>
            {
                x.Add(data.Date.ToString());
                y.Add(data.Price);
            });

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();

            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);

            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            //chart1.Location = new System.Drawing.Point(38, 61);
            chart1.Name = "chart1";
            //chart1.Size = new System.Drawing.Size(572, 377);
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



            chart1.Legends.Add(legend2);
            chart1.Legends[0].Position.Auto = true;
            chart1.Series[0].IsValueShownAsLabel = true;
            //是否显示图例
            chart1.Series[0].IsVisibleInLegend = true;
            //饼图折线
            chart1.Series[0]["PieLineColor"] = "Blue";
            //绑定数据
            flowLayoutPanel3.Controls.Add(chart1);
        }
        private void GenerateStackChart(Dictionary<string, ShowAnalyzeDTO> showAnalyzeDTOs)
        {
            flowLayoutPanel3.Controls.Clear();
            Chart chart1 = new Chart();
            chart1.Width = flowLayoutPanel3.Width;
            List<string> x = showAnalyzeDTOs.Select(dates => dates.Value.DateList).FirstOrDefault();
            if (x == null) { return; }
            var res = showAnalyzeDTOs.Select(data =>
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

            flowLayoutPanel3.Controls.Add(chart1);

        }
        private void ChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string chartType = ChartType.Text;
            if (chartType == "折線圖")
            {
                Compairision.Enabled = true;
            }
            else
                Compairision.Enabled = false;
        }
    }
}
