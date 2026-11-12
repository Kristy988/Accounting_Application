using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳APP.Models
{
    internal class LineChartModel
    {
        public string Name { get; set; }
        public List<string> x { get; set; }
        public List<double> y { get; set; }

    }
}
