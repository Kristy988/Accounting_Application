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
                chartPresenter.GetRecord(fromDate, toDate, groups, filters, ChartType.Text, Compairision.Text);
            }, 400);
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
