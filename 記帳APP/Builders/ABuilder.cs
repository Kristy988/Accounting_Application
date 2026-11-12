using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Repository;
using 記帳APP.Utility;


namespace 記帳APP.Builders
{
    internal abstract class ABuilder
    {
        private List<RecordDAO> recordDAOs;
        protected DateTime fromDate;
        protected DateTime toDate;
        protected List<AnalyzeDTO> newList;
        protected List<PropertyInfo> attributeAfterFilter = typeof(AnalyzeDTO).GetProperties().Where(x => x.GetCustomAttribute<DisplayNameAttribute>() != null).ToList();
        protected List<string> groups;
        protected Dictionary<string, List<string>> filters;

        //給圖表用
        public virtual ABuilder SetDateRange(DateTime fromDate, DateTime toDate)
        {
            this.fromDate = fromDate;
            this.toDate = toDate;
            RecordRepository recordRepository = new RecordRepository();
            recordDAOs = recordRepository.GetRecords(fromDate, toDate);
            return this;
        }
        public virtual ABuilder SetFilter(List<string> groups, Dictionary<string, List<string>> filters)
        {
            this.filters = filters;
            IEnumerable<AnalyzeDTO> analyzeDTOs = Mapper.Map<RecordDAO, AnalyzeDTO>(recordDAOs);
            //篩選有DisplayName的屬性
            var attributeAfterFilter = typeof(AnalyzeDTO).GetProperties().Where(x => x.GetCustomAttribute<DisplayNameAttribute>() != null).ToList();
            newList = new List<AnalyzeDTO>();
            this.groups = groups;

            if (filters.Count != 0)
            {
                newList = analyzeDTOs.Where(x =>
                {
                    List<bool> bools = new List<bool>();
                    int filterCount = 0;
                    if (filters.ContainsKey("方式"))
                        filterCount++;
                    if (filters.ContainsKey("對象"))
                        filterCount++;
                    //如果有目的 則filterCount+1
                    bool checkIfSub = filters.Any(z => z.Key.Contains("目的"));
                    if (checkIfSub)
                        filterCount++;

                    attributeAfterFilter.ForEach(y =>
                    {
                        string title = y.GetCustomAttribute<DisplayNameAttribute>().DisplayName;

                        if (title.Contains("目的"))
                            title = y.GetCustomAttribute<DisplayNameAttribute>().DisplayName + x.Category;

                        //比對屬性是否存在於filter,且欄位不是類型
                        if (filters.ContainsKey(title) && title != "類型")
                        {
                            var prop = filters.FirstOrDefault(z => z.Key == title);
                            //比對rawData是否符合filter要求
                            bools.Add(filters[prop.Key].Contains(y.GetValue(x)));
                        }
                    });

                    //符合所有篩選條件 則回傳true 反之則false
                    return bools.All(boolean => boolean) && bools.Count() == filterCount;
                }).ToList();
            }
            else
                newList = analyzeDTOs.ToList();
            return this;
        }
        public virtual ABuilder SetPeriod(string period = "none")
        {
            return this;
        }
        public abstract ABuilder GetRecord();
        public abstract Chart BuildPattern();
    }
}
