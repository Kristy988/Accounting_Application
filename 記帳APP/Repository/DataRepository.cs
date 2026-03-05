using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Repository.Model;

namespace 記帳APP.Repository
{
    internal class DataRepository : IDataRepository
    {
        public List<string> Category { get; set; } = new List<string> { "飲食", "交通", "居家", "購物", "美容", "娛樂", "醫療", "教育", "社交", "其他" };

        public Dictionary<string, List<string>> Subcategory { get; set; } = new Dictionary<string, List<string>>
        {
            { "飲食", new List<string> { "早餐", "午餐", "晚餐", "飲料", "零食" } },
            { "交通", new List<string> { "公車","捷運", "計程車", "油錢", "停車費", "維修保養" } },
            { "居家", new List<string> { "超市", "水電瓦斯", "網路費", "日用品", "家具家電","電話費" } },
            { "購物", new List<string> { "蝦皮", "淘寶", "MOMO","實體消費"} },
            { "服飾", new List<string> { "衣服","褲子", "鞋子","襪子", "包包" } },
            { "美容", new List<string> { "美髮","臉部保養", "化妝品","保養品", "美甲","按摩" } },
            { "娛樂", new List<string> { "電影","KTV", "旅遊", "遊戲", "演唱會", "健身" } },
            { "醫療", new List<string> { "掛號費", "藥品", "保健食品" } },
            { "教育", new List<string> { "學費", "書籍","文具" } },
            { "社交", new List<string> { "朋友聚餐", "禮物紅包", "慈善捐款" } },
            { "其他", new List<string> { "手續費", "貸款", "遺失","代墊"} }

        };
        public List<string> Target { get; set; } = new List<string> { "自己", "家人", "朋友", "寵物", "公司代墊" };
        public List<string> Payment { get; set; } = new List<string> { "現金", "信用卡", "金融卡", "行動支付", "電子票證", "銀行轉帳" };

        public Model.DataDAO GetData()
        {
            Model.DataDAO data = new DataDAO(Category, Subcategory, Target, Payment);
            return data;
        }
        public List<string> GetSubcatData(string cateName)
        {
            return Subcategory[cateName];
        }
    }
}
