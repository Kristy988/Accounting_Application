using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳APP.Repository
{
    internal interface IDataRepository
    {
        Model.DataDAO GetData();
        List<string> GetSubcatData(string cateName);
    }
}
