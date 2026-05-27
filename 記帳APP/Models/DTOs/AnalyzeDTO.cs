using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳APP.Models.DTOs
{
    internal class AnalyzeDTO
    {
        public string Date { get; set; }
        public string Price { get; set; }
        [DisplayName("類型")]
        public string Category { get; set; }
        [DisplayName("目的")]
        public string Subcategory { get; set; }
        [DisplayName("對象")]
        public string Target { get; set; }
        [DisplayName("方式")]
        public string Payment { get; set; }
    }
}
