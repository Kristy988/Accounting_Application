using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using 記帳APP.Attributes;
using 記帳APP.Extension;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Presenter;
using 記帳APP.Utility;
using static 記帳APP.Contract.AccountBookContract;
using static 記帳APP.Contract.AnalyzeContract;

namespace 記帳APP.Views
{
    public partial class 帳務分析 : Form, IAnalyzeView
    {
        IAnalyzePresenter analyzePresenter;
        List<ShowAnalyzeDTO> recordData = new List<ShowAnalyzeDTO>();

        List<string> groups = new List<string>(); // 類型: 食衣住行育樂
        Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();
        // 篩選條件: 食, [午餐,晚餐]

        //勾了自己 >>groups.add(對象) >>filters <對象,自己>

        public 帳務分析()
        {
            InitializeComponent();
            analyzePresenter = new AnaylzePresneter(this);
            flowLayoutPanel1.GroupCheckBoxGenerated(OnCheckGroups, OnCheckFilters);
            flowLayoutPanel2.CheckBoxGenerated(OnCheckGroups, OnCheckFilters);
        }

        //
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

        void IAnalyzeView.GetRecordResponse(List<ShowAnalyzeDTO> showAnalyzeDTOs)
        {
            recordData = showAnalyzeDTOs;

            Show_Data();
        }

        private void Show_Data()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = recordData;
            PropertyInfo[] theProperty = typeof(ShowAnalyzeDTO).GetProperties();


        }

        private void AnalyzeBTN_Click(object sender, EventArgs e)
        {
            this.Debounce(() =>
            {
                recordData.Clear();

                DateTime fromDate = fromDatePicker.Value;
                DateTime toDate = toDatePicker.Value;
                analyzePresenter.GetRecord(fromDate, toDate, groups, filters);

                Show_Data();
            }, 400);
        }


    }
}
