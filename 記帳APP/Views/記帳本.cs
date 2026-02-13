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

                Show_Data();
            }, 400);

        }

        private void Show_Data()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = recordData;

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
                    comboBoxColumn.Name = propertyName + "_comboBox";
                    comboBoxColumn.Tag = propertyName;
                    dataGridView1.Columns[propertyName].Visible = false;
                    dataGridView1.Columns.Add(comboBoxColumn);
                }
                if (theProperty[i].GetCustomAttribute<ComboBoxColumnAttribute>() != null && propertyName != "Subcategory")
                {
                    //datagridview.CreateComboBoxColumn(property);
                    comboBoxColumn = new DataGridViewComboBoxColumn();
                    comboBoxColumn.HeaderText = name;
                    comboBoxColumn.Name = propertyName + "_comboBox";
                    comboBoxColumn.Tag = propertyName;
                    dataGridView1.Columns[propertyName].Visible = false;
                    comboBoxColumn.DataSource = typeof(DataModel).GetProperty(propertyName).GetValue(null);
                    dataGridView1.Columns.Add(comboBoxColumn);
                }
                if (theProperty[i].GetCustomAttribute<ImageColumnAttribute>() != null)
                {
                    imageColumn = new DataGridViewImageColumn();
                    imageColumn.HeaderText = name;
                    imageColumn.Name = propertyName + "_image";
                    imageColumn.Tag = propertyName;
                    dataGridView1.Columns[propertyName].Visible = false;
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dataGridView1.Columns.Add(imageColumn);
                }
            }



            DataGridViewImageColumn delpicColumn = new DataGridViewImageColumn();
            delpicColumn.HeaderText = "刪除";
            delpicColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            delpicColumn.Name = "Delete";
            delpicColumn.DefaultCellStyle = new DataGridViewCellStyle()
            {
                NullValue = new Bitmap(@"C:\Users\krist\Desktop\家教課\記帳APP\垃圾桶.png")
            };
            //DefaultCellStyle 預設儲存格樣式
            //NullValue 沒有圖片時，給的預設值

            dataGridView1.Columns.Add(delpicColumn);

            for (int i = 0; i < recordData.Count; i++)
            {
                DataGridViewComboBoxCell cell9 = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells["Subcategory_comboBox"];
                cell9.DataSource = DataModel.Subcategory[(string)dataGridView1.Rows[i].Cells["Category"].Value];

                //方法1 cells中找出所有的comboBox
                dataGridView1.Rows[i].Cells.OfType<DataGridViewComboBoxCell>()
                    .ToList()
                    .ForEach(x =>
                    {
                        dataGridView1.Rows[i].Cells[x.OwningColumn.Name].Value = dataGridView1.Rows[i].Cells[x.OwningColumn.Tag.ToString()].Value;
                    });
                dataGridView1.Rows[i].Cells.OfType<DataGridViewImageCell>()
                    .ToList()
                    .ForEach(x =>
                    {
                        if (x.OwningColumn.Name != "Delete")
                        {
                            dataGridView1.Rows[i].Cells[x.OwningColumn.Name].Dispose();
                            GC.Collect();
                            dataGridView1.Rows[i].Cells[x.OwningColumn.Name].Value = new Bitmap(dataGridView1.Rows[i].Cells[x.OwningColumn.Tag.ToString()].Value.ToString());
                        }
                    });

                //方法2  從RecordModel反找所有屬性
                //typeof(RecordModel).GetProperties().Where(x =>
                //x.GetCustomAttribute<ComboBoxColumnAttribute>() != null ||
                //x.GetCustomAttribute<ImageColumnAttribute>() != null)
                //    .ToList()
                //    .ForEach(x =>
                //    {
                //        if (x.GetCustomAttribute<ComboBoxColumnAttribute>() is ComboBoxColumnAttribute comboBoxAttribute)
                //        {
                //            dataGridView1.Rows[i].Cells[$"{x.Name}_comboBox"].Value = dataGridView1.Rows[i].Cells[x.Name].Value;

                //        }
                //        if (x.GetCustomAttribute<ImageColumnAttribute>() is ImageColumnAttribute imageColumnAttribute)
                //        {
                //            dataGridView1.Rows[i].Cells[$"{x.Name}_image"].Dispose();
                //            GC.Collect();
                //            dataGridView1.Rows[i].Cells[$"{x.Name}_image"].Value = new Bitmap(x.GetValue(recordData[i]).ToString());

                //        }
                //    });

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;

            if (dataGridView1.Rows[row].Cells[col] is DataGridViewImageCell imageColumn && imageColumn.OwningColumn.Name != "Delete")
            {
                string path = dataGridView1.Rows[row].Cells[imageColumn.OwningColumn.Tag.ToString()].Value.ToString();
                ShowPic showPic = new ShowPic(path);
                showPic.Show();
            }

            //刪除
            if (dataGridView1.Rows[row].Cells[col] is DataGridViewImageCell deleteColumn && deleteColumn.OwningColumn.Name == "Delete")
            {
                string date = dataGridView1.Rows[row].Cells["Date"].Value.ToString();
                dataGridView1.Rows[row].Cells.OfType<DataGridViewImageCell>()
                    .ToList()
                    .ForEach(x =>
                    {
                        Image pic = (Image)dataGridView1.Rows[row].Cells[x.OwningColumn.Name].Value;
                        if (pic == null)
                            return;
                        pic.Dispose();
                        GC.Collect();
                        string picPath = dataGridView1.Rows[row].Cells[x.OwningColumn.Tag.ToString()].Value.ToString();
                        string picPath_origin = picPath.Replace("small_" + date, date);
                        File.Delete(picPath);
                        File.Delete(picPath_origin);
                    }
                    );


                dataGridView1.DataSource = null;
                recordData.RemoveAt(row);
                dataGridView1.Columns.Clear();
                File.Delete($@"C:\Users\krist\Desktop\家教課\記帳APP_資料記載\{date}\data.csv");
                Show_Data();

                List<RecordModel> newRecord = recordData.Where(x => x.Date == date).ToList();
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
            string date = dataGridView1.Rows[row].Cells["Date"].Value.ToString();

            if (dataGridView1.Rows[row].Cells[col] is DataGridViewTextBoxCell textBoxCell && textBoxCell.OwningColumn.Name == "Price")
            {
                recordData[row].Price = dataGridView1.Rows[row].Cells["Price"].Value.ToString();
            }
            

            if (dataGridView1.Rows[row].Cells[col] is DataGridViewComboBoxCell ComboBoxCell)
            {
                if (ComboBoxCell.OwningColumn.Tag.ToString() == "Category")
                {
                    recordData[row].Category = dataGridView1.Rows[row].Cells[ComboBoxCell.OwningColumn.Name].Value.ToString();

                    List<string> newData = DataModel.Subcategory[(string)dataGridView1.Rows[row].Cells[ComboBoxCell.OwningColumn.Name].Value];
                    recordData[row].Subcategory = newData[0].ToString();
                    DataGridViewComboBoxCell subCategoryCell = (DataGridViewComboBoxCell)dataGridView1.Rows[row].Cells["Subcategory_comboBox"];
                    subCategoryCell.DataSource = newData;
                    dataGridView1.Rows[row].Cells["Subcategory_comboBox"].Value = newData[0].ToString();
                    return;
                }

                typeof(RecordModel).GetProperty(ComboBoxCell.OwningColumn.Tag.ToString())
                    .SetValue(recordData[row], dataGridView1.Rows[row].Cells[ComboBoxCell.OwningColumn.Name].Value);

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
