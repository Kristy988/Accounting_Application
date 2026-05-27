using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳APP.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;

namespace 記帳APP.Extension
{
    internal static class CheckboxExtension
    {
        private static FlowLayoutPanel flowLayoutPanel1;
        private static FlowLayoutPanel flowLayoutPanel2;
        private static EventHandler onCheckGroups;
        private static EventHandler onCheckFilters;
        public static void GroupCheckBoxGenerated(this FlowLayoutPanel flowLayoutPanel1, EventHandler OnCheckGroups, EventHandler OnCheckFilters)
        {
            onCheckGroups = OnCheckGroups;
            onCheckFilters = OnCheckFilters;
            CheckboxExtension.flowLayoutPanel1 = flowLayoutPanel1;
            CheckBox checkBoxG1 = new CheckBox();
            checkBoxG1.Text = "類型";
            checkBoxG1.Tag = true;
            checkBoxG1.CheckedChanged += OnCheckGroups;
            checkBoxG1.CheckedChanged += GroupOnCheck;

            CheckBox checkBoxG4 = new CheckBox();
            checkBoxG4.Text = "目的";
            checkBoxG4.Tag = true;
            checkBoxG4.CheckedChanged += OnCheckGroups;

            CheckBox checkBoxG2 = new CheckBox();
            checkBoxG2.Text = "對象";
            checkBoxG2.Tag = true;
            checkBoxG2.CheckedChanged += OnCheckGroups;
            checkBoxG2.CheckedChanged += GroupOnCheck;

            CheckBox checkBoxG3 = new CheckBox();
            checkBoxG3.Text = "方式";
            checkBoxG3.Tag = true;
            checkBoxG3.CheckedChanged += OnCheckGroups;
            checkBoxG3.CheckedChanged += GroupOnCheck;

            flowLayoutPanel1.Controls.Add(checkBoxG1);
            flowLayoutPanel1.Controls.Add(checkBoxG4);
            flowLayoutPanel1.Controls.Add(checkBoxG2);
            flowLayoutPanel1.Controls.Add(checkBoxG3);

        }
        private static void GroupOnCheck(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            //找出該flow
            var theFlow = flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>().FirstOrDefault(x => x.Name == checkBox.Text);
            //找出該flow的全選按鍵
            var checkAllG1 = theFlow.Controls.OfType<CheckBox>().FirstOrDefault(x => x.Name == checkBox.Text);
            //該flow非全選的按鍵

            if (checkBox.Checked != checkAllG1.Checked)
            {
                checkAllG1.Checked = checkBox.Checked;
            }

        }

        public static void CheckBoxGenerated(this FlowLayoutPanel flowLayoutPanel2, EventHandler OnCheckGroups, EventHandler OnCheckFilters)
        {
            onCheckGroups = OnCheckGroups;
            onCheckFilters = OnCheckFilters;
            CheckboxExtension.flowLayoutPanel2 = flowLayoutPanel2;
            Label label = new Label();
            label.Text = "類型";
            //ComboToAllChecked -->SelectAllChecked的事件 但要可以勾起checkAll
            CheckBox checkBoxAll = new CheckBox();
            checkBoxAll.Text = "全選";
            checkBoxAll.Name = label.Text;
            checkBoxAll.Tag = true;
            checkBoxAll.CheckedChanged += SelectAllChecked;//主動

            FlowLayoutPanel flow1 = new FlowLayoutPanel();
            flow1.Name = label.Text;
            flow1.Width = flowLayoutPanel2.Width;
            flow1.Controls.Add(label);
            flow1.Controls.Add(checkBoxAll);
            for (int i = 0; i < DataModel.Category.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Tag = true;
                checkBox.CheckedChanged += SubCatGenerated;
                checkBox.CheckedChanged += OnCheck;//被動觸發
                //checkBox.CheckedChanged += OnCheckGroups;
                checkBox.CheckedChanged += OnCheckFilters;
                checkBox.Text = DataModel.Category[i];
                flow1.Controls.Add(checkBox);

                var flow = checkBox.Parent as FlowLayoutPanel;
                var theFlow = flow.Controls.OfType<FlowLayoutPanel>().FirstOrDefault(x => x.Name == "目的" + checkBox.Text);

                if (!checkBox.Checked && theFlow != null)
                {
                    var theCheck = flow.Controls.OfType<CheckBox>().FirstOrDefault(x => x.Name == "目的" + checkBox.Text);
                    theCheck.Checked = checkBox.Checked;
                }
            }

            Label label3 = new Label();
            label3.Text = "對象";
            FlowLayoutPanel flow3 = new FlowLayoutPanel();
            flow3.Width = flowLayoutPanel2.Width;
            flow3.Name = label3.Text;
            CheckBox checkBoxAll3 = new CheckBox();
            checkBoxAll3.Text = "全選";
            checkBoxAll3.CheckedChanged += SelectAllChecked;
            checkBoxAll3.Name = label3.Text;

            flow3.Controls.Add(label3);
            flow3.Controls.Add(checkBoxAll3);
            foreach (var item in DataModel.Target)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = item;
                checkBox.Tag = true;
                checkBox.CheckedChanged += OnCheck;
                //checkBox.CheckedChanged += OnCheckGroups;
                checkBox.CheckedChanged += OnCheckFilters;
                flow3.Controls.Add(checkBox);

            }

            Label label4 = new Label();
            label4.Text = "方式";
            FlowLayoutPanel flow4 = new FlowLayoutPanel();
            flow4.Width = flowLayoutPanel2.Width;
            flow4.Name = label4.Text;
            CheckBox checkBoxAll4 = new CheckBox();
            checkBoxAll4.Text = "全選";
            checkBoxAll4.CheckedChanged += SelectAllChecked;
            checkBoxAll4.Name = label4.Text;
            flow4.Controls.Add(label4);
            flow4.Controls.Add(checkBoxAll4);
            foreach (var item in DataModel.Payment)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = item;
                checkBox.Tag = true;
                checkBox.CheckedChanged += OnCheck;
                //checkBox.CheckedChanged += OnCheckGroups;
                checkBox.CheckedChanged += OnCheckFilters;
                flow4.Controls.Add(checkBox);
            }
            flowLayoutPanel2.Controls.Add(flow1);
            flowLayoutPanel2.Controls.Add(flow3);
            flowLayoutPanel2.Controls.Add(flow4);
        }


        private static void SelectAllChecked(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Tag is bool CheckAll && !CheckAll)
            {
                checkBox.Tag = true;
                return;
            }
            //找出該flow
            var theFlow = checkBox.Parent as FlowLayoutPanel;
            //找出該flow的全選按鍵
            var checkAll = theFlow.Controls.OfType<CheckBox>().FirstOrDefault(x => x.Name == theFlow.Name);
            //該flow非全選的按鍵

            var otherCheck = theFlow.Controls.OfType<CheckBox>().Where(x => x.Name != checkAll.Name).ToList();
            //凍結使用其他選項
            var blockCheckBox = otherCheck.Select(x => { return x.Tag = false; }).ToList();
            otherCheck.ForEach(x =>
            {
                if (x.Checked != checkAll.Checked)
                {
                    x.Checked = checkAll.Checked;
                }
            });

            //開放其他選項的使用
            var returnCheckBox = otherCheck.Select(x => { return x.Tag = true; }).ToList();
        }
        private static void OnCheck(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            //找出該flow
            var theFlow = checkBox.Parent as FlowLayoutPanel;
            //找出該flow的全選按鍵
            var checkAll = theFlow.Controls.OfType<CheckBox>().FirstOrDefault(x => x.Name == theFlow.Name);
            //該flow非全選的按鍵
            var otherCheck = theFlow.Controls.OfType<CheckBox>().Where(x => x.Name != checkAll.Name).ToList();
            var checkStatus = otherCheck.All(x => x.Checked);
            //檢查非全選選項是否有被凍結
            var ifCheckBoxisNotBlook = otherCheck.All(x => x.Tag is bool state && state);

            if (checkAll.Checked != checkStatus && ifCheckBoxisNotBlook)
            {
                checkAll.Tag = false;
                checkAll.Checked = checkStatus;
            }

        }

        private static void SubCatGenerated(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string theCat = checkBox.Text;
            var theSub = DataModel.Subcategory.Where(x => x.Key == theCat).ToList();
            var findTheFlow = flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>().FirstOrDefault(x => x.Name == "目的" + checkBox.Text);
            Label label2 = new Label();
            label2.Text = "目的";
            CheckBox checkBoxAll2 = new CheckBox();
            checkBoxAll2.Text = "全選";
            checkBoxAll2.Name = label2.Text + theCat;
            checkBoxAll2.CheckedChanged += SelectAllChecked;
            FlowLayoutPanel flow2 = new FlowLayoutPanel();
            flow2.Name = label2.Text + theCat;
            flow2.Width = flowLayoutPanel2.Width;
            flow2.Controls.Add(label2);
            flow2.Controls.Add(checkBoxAll2);
            if (findTheFlow == null)
            {
                foreach (var checkList in theSub)
                {
                    foreach (var item in checkList.Value)
                    {
                        CheckBox checkBox2 = new CheckBox();
                        checkBox2.Text = item;
                        checkBox2.Tag = true;
                        checkBox2.CheckedChanged += OnCheck;
                        //checkBox2.CheckedChanged += onCheckGroups;
                        checkBox2.CheckedChanged += onCheckFilters;
                        flow2.Controls.Add(checkBox2);

                    }
                }
            }

            if (checkBox.Checked)
            {
                flowLayoutPanel2.Controls.Add(flow2);
                checkBoxAll2.Checked = true;

            }
            else
            {
                var theCheckAll = findTheFlow.Controls.OfType<CheckBox>().FirstOrDefault(x => x.Name == findTheFlow.Name);
                theCheckAll.Checked = false;

                var removeItem = flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>().FirstOrDefault(x => x.Name == label2.Text + theCat);
                if (removeItem != null)
                {
                    flowLayoutPanel2.Controls.Remove(removeItem);
                }
            }


        }
    }
}
