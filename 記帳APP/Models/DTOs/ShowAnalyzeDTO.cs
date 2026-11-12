using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳APP.Models.DTOs
{
    internal class ShowAnalyzeDTO
    {
        public string Date { get; set; }
        [DisplayName("分析項目")]
        public string Title { get; set; }
        [DisplayName("金額")]
        public string Price { get; set; }
        public List<string> DateList { get; set; }
        public List<int> PriceList { get; set; }

        public Dictionary<string, List<int>> TitlePrice { get; set; }
    }
}
