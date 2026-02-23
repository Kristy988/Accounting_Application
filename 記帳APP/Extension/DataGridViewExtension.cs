using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using 記帳APP.Attributes;
using 記帳APP.Models;

namespace 記帳APP.Extension
{
    internal static class DataGridViewExtension
    {
        public static void CreateComboBoxColumn(this DataGridView dataGridView, PropertyInfo theProperty)
        {
            DataGridViewComboBoxColumn comboBoxColumn;
            string name = theProperty.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
            string propertyName = theProperty.Name;


            comboBoxColumn = new DataGridViewComboBoxColumn();
            comboBoxColumn.HeaderText = name;
            comboBoxColumn.Name = propertyName + "_comboBox";
            comboBoxColumn.DataPropertyName = propertyName;
            comboBoxColumn.Tag = propertyName;
            dataGridView.Columns[propertyName].Visible = false;
            dataGridView.Columns.Add(comboBoxColumn);


            if (propertyName != "Subcategory")
            {
                comboBoxColumn.DataSource = typeof(DataModel).GetProperty(propertyName).GetValue(null);

            }


        }

        public static void CreateImageColumn(this DataGridView dataGridView, PropertyInfo theProperty)
        {
            DataGridViewImageColumn imageColumn;
            string name = theProperty.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
            string propertyName = theProperty.Name;


            imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = name;
            imageColumn.Name = propertyName + "_image";
            imageColumn.Tag = propertyName;
            dataGridView.Columns[propertyName].Visible = false;
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView.Columns.Add(imageColumn);


        }
        public static void CreateImageColumn(this DataGridView dataGridView, string headerText, string theName, string URL)
        {
            DataGridViewImageColumn newImageColumn = new DataGridViewImageColumn();
            newImageColumn.HeaderText = headerText;
            newImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            newImageColumn.Name = theName;
            newImageColumn.DefaultCellStyle = new DataGridViewCellStyle()
            {
                NullValue = new Bitmap(URL)
            };
            //DefaultCellStyle 預設儲存格樣式
            //NullValue 沒有圖片時，給的預設值

            dataGridView.Columns.Add(newImageColumn);


        }
    }
}
