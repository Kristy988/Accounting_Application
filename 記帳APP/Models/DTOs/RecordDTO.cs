using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳APP.Models.DTOs
{
    internal class RecordDTO
    {
        public string Date { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Target { get; set; }
        public string Payment { get; set; }
        public Image Picture1 { get; set; }
        public Image Picture2 { get; set; }
    }
}
