using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳APP.Repository.Model
{
    internal class DataDAO
    {
        public List<string> Category;
        public Dictionary<string, List<string>> Subcategory;
        public List<string> Target;
        public List<string> Payment;
        public DataDAO(List<string> Category, Dictionary<string, List<string>> Subcategory, List<string> Target, List<string> Payment)
        {
            this.Category = Category;
            this.Subcategory = Subcategory;
            this.Target = Target;
            this.Payment = Payment;
        }
    }
}
