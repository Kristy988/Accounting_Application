using CSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳APP.Attributes;
using 記帳APP.Extension;
using 記帳APP.Models;

namespace 記帳APP.Views
{
    public partial class 記帳本 : Form
    {
        public 記帳本()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;

            //DataGridView組成分為三種
            //DataGridViewColumn => 根據Model的每一個Property都創建一個 DataGridViewTextBoxColumn
            //DataGridViewRow => 針對 list跑迴圈，為每一筆創建 DataGridViewRow,再將row放進去Rows
            //DataGridViewCell => 會根據每一個Row去製作Property中每一個對應的DataGridViewTextBoxCell

        }
        List<RecordModel> recordData = new List<RecordModel>();

        //按查詢//
        private void button1_Click(object sender, EventArgs e)
        {
            this.Debounce(() =>
            {
                recordData.Clear();

                DateTime fromDate = fromDatePicker.Value;
                DateTime toDate = toDatePicker.Value;
                TimeSpan diff = toDate - fromDate;
                int count = diff.Days;


                for (int i = 0; i < count + 1; i++)
                {
                    if (File.Exists($@"C:\Users\krist\Desktop\家教課\記帳APP_資料記載\{fromDate.AddDays(i).ToString("yyyy-MM-dd")}\data.csv"))
                    {
                        recordData.AddRange(CSVHelper.Read<RecordModel>($@"C:\Users\krist\Desktop\家教課\記帳APP_資料記載\{fromDate.AddDays(i).ToString("yyyy-MM-dd")}\data.csv"));
                    }
                }

                //recordData = CSVHelper.Read<RecordModel>(@"C:\Users\krist\Desktop\家教課\記帳APP_資料記載\data.csv");
                Show_Data();
            }, 400);

        }

        private void Show_Data()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = recordData;

            //typeof(RecordModel).GetProperty("Category").GetCustomAttribute<ComboBoxColumnAttribute>();
            PropertyInfo[] theProperty = typeof(RecordModel).GetProperties();
            DataGridViewComboBoxColumn comboBoxColumn;
            DataGridViewImageColumn imageColumn;
            for (int i = 0; i < theProperty.Length; i++)
            {
                string name = theProperty[i].GetCustomAttribute<DisplayNameAttribute>().DisplayName;
                string propertyName = theProperty[i].Name;
                if (propertyName == "Subcategory")
                {
                    comboBoxColumn = new DataGridViewComboBoxColumn();
                    comboBoxColumn.HeaderText = name;
                    comboBoxColumn.Name = name + "comboBox";
                    dataGridView1.Columns[propertyName].Visible = false;
                    dataGridView1.Columns.Add(comboBoxColumn);
                }
                if (theProperty[i].GetCustomAttribute<ComboBoxColumnAttribute>() != null && propertyName != "Subcategory")
                {
                    comboBoxColumn = new DataGridViewComboBoxColumn();
                    comboBoxColumn.HeaderText = name;
                    comboBoxColumn.Name = name + "comboBox";
                    dataGridView1.Columns[propertyName].Visible = false;
                    comboBoxColumn.DataSource = typeof(DataModel).GetProperty(propertyName).GetValue(null);
                    dataGridView1.Columns.Add(comboBoxColumn);
                }
                if (theProperty[i].GetCustomAttribute<ImageColumnAttribute>() != null)
                {
                    imageColumn = new DataGridViewImageColumn();
                    imageColumn.HeaderText = name;
                    imageColumn.Name = name + "image";
                    dataGridView1.Columns[propertyName].Visible = false;
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dataGridView1.Columns.Add(imageColumn);
                }
            }


            //DataGridViewImageColumn picColumn = new DataGridViewImageColumn();
            //picColumn.HeaderText = "圖片1";
            //picColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //DataGridViewImageColumn picColumn2 = new DataGridViewImageColumn();
            //picColumn2.HeaderText = "圖片2";
            //picColumn2.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
            //comboBoxColumn.HeaderText = "分類";
            //comboBoxColumn.DataSource = DataModel.Category;

            //DataGridViewComboBoxColumn comboBoxColumn2 = new DataGridViewComboBoxColumn();
            //comboBoxColumn2.HeaderText = "類別";
            //comboBoxColumn2.DataSource = DataModel.SubCategory[DataModel.Category[0]];

            //DataGridViewComboBoxColumn comboBoxColumn3 = new DataGridViewComboBoxColumn();
            //comboBoxColumn3.HeaderText = "消費對象";
            //comboBoxColumn3.DataSource = DataModel.Target;

            //DataGridViewComboBoxColumn comboBoxColumn4 = new DataGridViewComboBoxColumn();
            //comboBoxColumn4.HeaderText = "支付方式";
            //comboBoxColumn4.DataSource = DataModel.Payment;

            DataGridViewImageColumn delpicColumn = new DataGridViewImageColumn();
            delpicColumn.HeaderText = "刪除";
            delpicColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            //dataGridView1.Columns.Add(comboBoxColumn);
            //dataGridView1.Columns.Add(comboBoxColumn2);
            //dataGridView1.Columns.Add(comboBoxColumn3);
            //dataGridView1.Columns.Add(comboBoxColumn4);
            //dataGridView1.Columns.Add(picColumn);
            //dataGridView1.Columns.Add(picColumn2);
            dataGridView1.Columns.Add(delpicColumn);

            //dataGridView1.Columns[2].Visible = false;
            //dataGridView1.Columns[3].Visible = false;
            //dataGridView1.Columns[4].Visible = false;
            //dataGridView1.Columns[5].Visible = false;
            //dataGridView1.Columns[6].Visible = false;
            //dataGridView1.Columns[7].Visible = false;


            for (int i = 0; i < recordData.Count; i++)
            {
                dataGridView1.Rows[i].Cells[12].Dispose();
                dataGridView1.Rows[i].Cells[13].Dispose();
                GC.Collect();

                DataGridViewComboBoxCell cell9 = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[9];
                cell9.DataSource = DataModel.Subcategory[(string)dataGridView1.Rows[i].Cells[2].Value];

                dataGridView1.Rows[i].Cells[8].Value = dataGridView1.Rows[i].Cells[2].Value;
                dataGridView1.Rows[i].Cells[9].Value = dataGridView1.Rows[i].Cells[3].Value;
                dataGridView1.Rows[i].Cells[10].Value = dataGridView1.Rows[i].Cells[4].Value;
                dataGridView1.Rows[i].Cells[11].Value = dataGridView1.Rows[i].Cells[5].Value;
                dataGridView1.Rows[i].Cells[12].Value = new Bitmap(recordData[i].Picture1);
                dataGridView1.Rows[i].Cells[13].Value = new Bitmap(recordData[i].Picture2);
                dataGridView1.Rows[i].Cells[14].Value = new Bitmap(@"C:\Users\krist\Desktop\家教課\記帳APP\垃圾桶.png");

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            string path = "";
            //if (dataGridView1.Rows[row].Cells[col] is DataGridViewImageCell imageCell)
            //{
            //    ShowPic showPic = new ShowPic((Bitmap)imageCell.Value);
            //    showPic.Show();
            //}
            if (col == 12)
            {
                path = (string)dataGridView1.Rows[row].Cells[6].Value;
                ShowPic showPic = new ShowPic(path);
                showPic.Show();
            }
            if (col == 13)
            {
                path = (string)dataGridView1.Rows[row].Cells[7].Value;
                ShowPic showPic = new ShowPic(path);
                showPic.Show();

            }

            //刪除
            if (col == 14)
            {
                Image p1 = (Image)dataGridView1.Rows[row].Cells[12].Value;
                Image p2 = (Image)dataGridView1.Rows[row].Cells[13].Value;
                p1.Dispose();
                p2.Dispose();
                GC.Collect();

                string date = dataGridView1.Rows[row].Cells[0].Value.ToString();


                dataGridView1.DataSource = null;
                string pic1 = recordData[row].Picture1;
                string pic2 = recordData[row].Picture2;

                recordData.RemoveAt(row);
                dataGridView1.Columns.Clear();


                File.Delete(pic1);
                File.Delete(pic2);
                File.Delete($@"C:\Users\krist\Desktop\家教課\記帳APP_資料記載\{date}\data.csv");

                Show_Data();


                List<RecordModel> newRecord = recordData.Where(x => x.Date == date).ToList();
                string pic1_origin = pic1.Replace("small_" + date, date);
                string pic2_origin = pic2.Replace("small_" + date, date);
                File.Delete(pic1_origin);
                File.Delete(pic2_origin);


                if (newRecord.Count > 0)
                {
                    CSVHelper.Write($@"C:\Users\krist\Desktop\家教課\記帳APP_資料記載\{date}\data.csv", newRecord);
                }
                else
                {
                    Directory.Delete($@"C:\Users\krist\Desktop\家教課\記帳APP_資料記載\{date}");
                }

            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;


            string date = dataGridView1.Rows[row].Cells[0].Value.ToString();

            if (col == 1)
            {
                recordData[row].Price = dataGridView1.Rows[row].Cells[1].Value.ToString();
            }
            if (col == 8)
            {
                recordData[row].Category = dataGridView1.Rows[row].Cells[8].Value.ToString();

                List<string> newData = DataModel.Subcategory[(string)dataGridView1.Rows[row].Cells[8].Value];
                recordData[row].Subcategory = newData[0].ToString();
                DataGridViewComboBoxCell subCategoryCell = (DataGridViewComboBoxCell)dataGridView1.Rows[row].Cells[9];
                subCategoryCell.DataSource = newData;
                dataGridView1.Rows[row].Cells[9].Value = newData[0].ToString();
            }
            if (col == 9)
            {
                recordData[row].Subcategory = dataGridView1.Rows[row].Cells[9].Value.ToString();
            }
            if (col == 10)
            {
                recordData[row].Target = dataGridView1.Rows[row].Cells[10].Value.ToString();
            }
            if (col == 11)
            {
                recordData[row].Payment = dataGridView1.Rows[row].Cells[11].Value.ToString();
            }

            File.Delete($@"C:\Users\krist\Desktop\家教課\記帳APP_資料記載\{date}\data.csv");
            List<RecordModel> newRecord = recordData.Where(x => x.Date == date).ToList();

            if (newRecord.Count > 0)
            {
                CSVHelper.Write($@"C:\Users\krist\Desktop\家教課\記帳APP_資料記載\{date}\data.csv", newRecord);
            }

        }

        // Source - https://stackoverflow.com/a
        // Posted by OhBeWise, modified by community. See post 'Timeline' for change history
        // Retrieved 2026-01-14, License - CC BY-SA 3.0


        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.IsCurrentCellDirty && this.dataGridView1.CurrentCell is DataGridViewComboBoxCell)
            {
                this.dataGridView1.EndEdit();
                //  dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void 記帳本_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fromDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
