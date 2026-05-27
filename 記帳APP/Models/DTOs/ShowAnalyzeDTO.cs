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
        [DisplayName("分析項目")]
        public string Title { get; set; }
        [DisplayName("金額")]
        public string Price { get; set; }

    }
}
