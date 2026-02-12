using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Attributes;

namespace 記帳APP.Models
{
    internal class RecordModel
    {
        [DisplayName("日期")]
        public string Date { get; set; }

        [DisplayName("消費金額")]
        public string Price { get; set; }

        [DisplayName("分類")]
        [ComboBoxColumn]
        public string Category { get; set; }

        [DisplayName("類別")]
        [ComboBoxColumn]
        public string Subcategory { get; set; }

        [DisplayName("消費對象")]
        [ComboBoxColumn]
        public string Target { get; set; }

        [DisplayName("支付方式")]
        [ComboBoxColumn]
        public string Payment { get; set; }

        [DisplayName("圖片1")]
        [ImageColumn]
        public string Picture1 { get; set; }

        [DisplayName("圖片2")]
        [ImageColumn]
        public string Picture2 { get; set; }

    }
}
