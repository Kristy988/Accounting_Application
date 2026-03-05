using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Attributes;

namespace 記帳APP.Models
{
    internal class RecordDAO
    {
        public string Date { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Target { get; set; }
        public string Payment { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }

    }
}
