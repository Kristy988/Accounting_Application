using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Models;
using 記帳APP.Models.DTOs;
using 記帳APP.Repository;
using 記帳APP.Utility;
using static 記帳APP.Contract.AccountBookContract;
using static 記帳APP.Contract.AnalyzeContract;

namespace 記帳APP.Presenter
{
    internal class AnaylzePresneter : IAnalyzePresenter

    {
        IAnalyzeView analyzeView;
        public AnaylzePresneter(IAnalyzeView analyzeView)
        {
            this.analyzeView = analyzeView;
        }
        public void GetRecord(DateTime fromDate, DateTime toDate, List<string> groups, Dictionary<string, List<string>> filters)
        {
            RecordRepository recordRepository = new RecordRepository();
            List<RecordDAO> recordDAOs = recordRepository.GetRecords(fromDate, toDate);
            IEnumerable<AnalyzeDTO> analyzeDTOs = Mapper.Map<RecordDAO, AnalyzeDTO>(recordDAOs);
            //篩選有DisplayName的屬性
            var attibuteAfterFilter = typeof(AnalyzeDTO).GetProperties().Where(x => x.GetCustomAttribute<DisplayNameAttribute>() != null).ToList();
            List<AnalyzeDTO> newList = new List<AnalyzeDTO>();

            if (filters.Count != 0)
            {
                var filterList = new List<PropertyInfo>();

                newList = analyzeDTOs.Where(x =>
                {
                    bool rawDataAfterFilter = attibuteAfterFilter.Any(y =>
                    {
                        //比對屬性是否存在於filter
                        if (!filters.ContainsKey(y.GetCustomAttribute<DisplayNameAttribute>().DisplayName))
                            return false;
                        var prop = filters.FirstOrDefault(z => z.Key == y.GetCustomAttribute<DisplayNameAttribute>().DisplayName);

                        //比對rawData是否符合filter要求
                        return filters[prop.Key].Contains(y.GetValue(x));
                    });
                    return rawDataAfterFilter;
                }).ToList();
            }
            else
                newList = analyzeDTOs.ToList();


            //if (filters.Count != 0)
            //{
            //    newList = analyzeDTOs.Where(x =>
            //    {
            //        int filterCount = filters.Count;
            //        List<bool> bools = new List<bool>();
            //        if (filters.Keys.Contains("對象"))
            //        {
            //            bools.Add(filters["對象"].Contains(x.Target));
            //        }
            //        if (filters.Keys.Contains("方式"))
            //        {
            //            bools.Add(filters["方式"].Contains(x.Payment));
            //        }
            //        string key = $"目的{x.Category}";

            //        if (filters.ContainsKey(key))
            //        {
            //            bools.Add(filters[key].Contains(x.Subcategory));
            //        }
            //        if (filters.Keys.Contains("類型"))
            //            filterCount--;
            //        return bools.All(boolean => boolean) && bools.Count() == filterCount;
            //    }).ToList();
            //}
            //else
            //    newList = analyzeDTOs.ToList();

            var con1 = groups.First();
            var con2 = groups.Last();
            var resultList = newList.GroupBy(x =>
            {
                string conA = "";
                string conB = "";
                if (con1 == "類型")
                    conA = x.Category;
                else if (con1.Contains("目的"))
                    conA = x.Subcategory;
                else if (con1 == "對象")
                    conA = x.Target;
                else if (con1 == "方式")
                    conA = x.Payment;

                if (con2 == "類型")
                    conB = x.Category;
                else if (con2.Contains("目的"))
                    conB = x.Subcategory;
                else if (con2 == "對象")
                    conB = x.Target;
                else if (con2 == "方式")
                    conB = x.Payment;

                return $"{conA}/{conB}";
            })
                .Select(x =>
                {
                    var priceSum = x.Sum(y => int.Parse(y.Price));
                    var result = new ShowAnalyzeDTO
                    {
                        Price = priceSum.ToString(),
                        Title = x.Key,
                    };
                    return result;
                }).ToList();




            analyzeView.GetRecordResponse(resultList);
        }


    }
}
