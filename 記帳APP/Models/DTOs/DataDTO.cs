using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳APP.Models.DTOs
{
    internal class DataDTO
    {
        public string Date { get; set; }
        public string Price { get; set; }
        public List<string> Category { get; set; }
        public Dictionary<string, List<string>> Subcategory { get; set; }
        public List<string> Target { get; set; }
        public List<string> Payment { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }


    }
}
