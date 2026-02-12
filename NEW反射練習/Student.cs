using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEW反射練習
{
    internal class Student : IDisposable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }

        public void Dispose()
        {
            StreamReader reader = new StreamReader("");
            reader.Close();
        }
    }
}
