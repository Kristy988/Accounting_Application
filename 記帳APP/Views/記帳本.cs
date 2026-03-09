using CSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
using 記帳APP.Models.DTOs;
using 記帳APP.Presenter;
using static 記帳APP.Contract.AccountBookContract;

namespace 記帳APP.Views
{
    public partial class 記帳本 : Form, IAccountBookView
    {
        string delPicPath = ConfigurationManager.AppSettings["DelPicPath"];
        IAccountBookPresenter accountBookPresenter;
        List<RecordModel> recordData = new List<RecordModel>();
        public 記帳本()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;

            //DataGridView組成分為三種
            //DataGridViewColumn => 根據Model的每一個Property都創建一個 DataGridViewTextBoxColumn
            //DataGridViewRow => 針對 list跑迴圈，為每一筆創建 DataGridViewRow,再將row放進去Rows
            //DataGridViewCell => 會根據每一個Row去製作Property中每一個對應的DataGridViewTextBoxCell
            accountBookPresenter = new AccountBookPresenter(this);
        }
        void IAccountBookView.GetRecordResponse(List<AccountBookDTO> accountBookDTOs)
        {

            for (int i = 0; i < accountBookDTOs.Count; i++)
            {
                RecordModel recordModel = new RecordModel();
                recordModel.Date = accountBookDTOs[i].Date;
                recordModel.Category = accountBookDTOs[i].Category;
                recordModel.Subcategory = accountBookDTOs[i].Subcategory;
                recordModel.Price = accountBookDTOs[i].Price;
                recordModel.Target = accountBookDTOs[i].Target;
                recordModel.Payment = accountBookDTOs[i].Payment;
                recordModel.Picture1 = accountBookDTOs[i].Picture1;
                recordModel.Picture2 = accountBookDTOs[i].Picture2;
                recordData.Add(recordModel);
            }
            Show_Data();
        }


        //按查詢//
        private void button1_Click(object sender, EventArgs e)
        {
            this.Debounce(() =>
            {
                recordData.Clear();

                DateTime fromDate = fromDatePicker.Value;
                DateTime toDate = toDatePicker.Value;
                accountBookPresenter.GetRecord(fromDate, toDate);
                
                Show_Data();
            }, 400);

        }

        private void Show_Data()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = recordData;

            PropertyInfo[] theProperty = typeof(RecordModel).GetProperties();
            for (int i = 0; i < theProperty.Length; i++)
            {
                string name = theProperty[i].GetCustomAttribute<DisplayNameAttribute>().DisplayName;
                string propertyName = theProperty[i].Name;

                if (theProperty[i].GetCustomAttribute<ComboBoxColumnAttribute>() != null)
                {
                    dataGridView1.CreateComboBoxColumn(theProperty[i]);
                }
                if (theProperty[i].GetCustomAttribute<ImageColumnAttribute>() != null)
                {
                    dataGridView1.CreateImageColumn(theProperty[i]);
                }
            }

            dataGridView1.CreateImageColumn("刪除", "Delete", delPicPath);


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
                AccountBookDTO accountBookDTO = new AccountBookDTO();
                accountBookDTO.Date = dataGridView1.Rows[row].Cells["Date"].Value.ToString();
                accountBookDTO.Category = dataGridView1.Rows[row].Cells["Category"].Value.ToString();
                accountBookDTO.Subcategory = dataGridView1.Rows[row].Cells["Subcategory"].Value.ToString();
                accountBookDTO.Price = dataGridView1.Rows[row].Cells["Price"].Value.ToString();
                accountBookDTO.Target = dataGridView1.Rows[row].Cells["Target"].Value.ToString();
                accountBookDTO.Payment = dataGridView1.Rows[row].Cells["Payment"].Value.ToString();
                accountBookDTO.Picture1 = dataGridView1.Rows[row].Cells["Picture1"].Value.ToString();
                accountBookDTO.Picture2 = dataGridView1.Rows[row].Cells["Picture2"].Value.ToString();

                dataGridView1.Rows[row].Cells.OfType<DataGridViewImageCell>()
                    .ToList()
                    .ForEach(x =>
                    {
                        Image pic = (Image)dataGridView1.Rows[row].Cells[x.OwningColumn.Name].Value;
                        if (pic == null)
                            return;
                        pic.Dispose();
                        GC.Collect();
                    }
                    );

                accountBookPresenter.DeleteRecord(accountBookDTO);

                dataGridView1.DataSource = null;
                recordData.RemoveAt(row);
                dataGridView1.Columns.Clear();
                Show_Data();

            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;

            if (dataGridView1.Rows[row].Cells[col] is DataGridViewComboBoxCell ComboBoxCell && ComboBoxCell.OwningColumn.Tag.ToString() == "Category")
            {
                List<string> newData = DataModel.Subcategory[recordData[row].Category];
                DataGridViewComboBoxCell subCategoryCell = (DataGridViewComboBoxCell)dataGridView1.Rows[row].Cells["Subcategory_comboBox"];
                subCategoryCell.DataSource = newData;
                dataGridView1.Rows[row].Cells["Subcategory_comboBox"].Value = newData[0].ToString();
            }
            AccountBookDTO accountBookDTO = new AccountBookDTO();
            accountBookDTO.Date = dataGridView1.Rows[row].Cells["Date"].Value.ToString();
            accountBookDTO.Category = dataGridView1.Rows[row].Cells["Category"].Value.ToString();
            accountBookDTO.Subcategory = dataGridView1.Rows[row].Cells["Subcategory"].Value.ToString();
            accountBookDTO.Price = dataGridView1.Rows[row].Cells["Price"].Value.ToString();
            accountBookDTO.Target = dataGridView1.Rows[row].Cells["Target"].Value.ToString();
            accountBookDTO.Payment = dataGridView1.Rows[row].Cells["Payment"].Value.ToString();
            accountBookDTO.Picture1 = dataGridView1.Rows[row].Cells["Picture1"].Value.ToString();
            accountBookDTO.Picture2 = dataGridView1.Rows[row].Cells["Picture2"].Value.ToString();

            accountBookPresenter.UpdateRecord(accountBookDTO);

            //File.Delete(Path.Combine(directoryPath, date, "data.csv"));
            //List<RecordModel> newRecord = recordData.Where(x => x.Date == date).ToList();

            //if (newRecord.Count > 0)
            //{
            //    CSVHelper.Write(Path.Combine(directoryPath, date, "data.csv"), newRecord);
            //}

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


    }
}
