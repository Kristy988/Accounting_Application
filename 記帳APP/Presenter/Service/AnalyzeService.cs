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
using static 記帳APP.Contract.AnalyzeContract;

namespace 記帳APP.Presenter.Service
{
    internal class AnalyzeService
    {
        public List<ShowAnalyzeDTO> GetRecord(DateTime fromDate, DateTime toDate, List<string> groups, Dictionary<string, List<string>> filters, string distin = "none")
        {
            RecordRepository recordRepository = new RecordRepository();
            List<RecordDAO> recordDAOs = recordRepository.GetRecords(fromDate, toDate);
            IEnumerable<AnalyzeDTO> analyzeDTOs = Mapper.Map<RecordDAO, AnalyzeDTO>(recordDAOs);
            //篩選有DisplayName的屬性
            var attributeAfterFilter = typeof(AnalyzeDTO).GetProperties().Where(x => x.GetCustomAttribute<DisplayNameAttribute>() != null).ToList();
            List<AnalyzeDTO> newList = new List<AnalyzeDTO>();

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

            var resultList = new List<ShowAnalyzeDTO>();
            if (distin == "none")
            {
                if (groups.Count != 0)
                {
                    resultList = newList.GroupBy(x =>
                    {
                        string theGroup = "";
                        attributeAfterFilter.ForEach(y =>
                        {
                            string title = y.GetCustomAttribute<DisplayNameAttribute>().DisplayName;

                            //假如有這個群組的話
                            if (groups.Contains(title))
                            {
                                theGroup += y.GetValue(x).ToString() + "/";
                            }
                        });
                        theGroup = theGroup.TrimEnd('/');

                        return theGroup;
                    }).Select(x =>
                    {
                        var priceSum = x.Sum(y => int.Parse(y.Price));
                        var result = new ShowAnalyzeDTO
                        {

                            Price = priceSum.ToString(),
                            Title = x.Key,
                        };
                        return result;
                    }).ToList();
                }
                else
                {
                    int sum = newList.Sum(x => int.Parse(x.Price));
                    ShowAnalyzeDTO item = new ShowAnalyzeDTO();
                    item.Price = sum.ToString();
                    item.Title = "總計";
                    resultList.Add(item);
                }


            }
            if (distin == "date")
            {
                if (groups.Count != 0)
                {
                    resultList = newList.GroupBy(x => x.Date).Select(x =>
                    {
                        var priceSum = x.Sum(y => int.Parse(y.Price));
                        var result = new ShowAnalyzeDTO
                        {
                            Date = x.Key,
                            Price = priceSum.ToString(),
                        };
                        return result;
                    }).ToList();
                }
                else
                {
                    int sum = newList.Sum(x => int.Parse(x.Price));
                    ShowAnalyzeDTO item = new ShowAnalyzeDTO();
                    item.Price = sum.ToString();
                    item.Title = "總計";
                    resultList.Add(item);
                }

            }
            //if (distin == "stack")
            //{
            //    var dateList = newList.Select(x => x.Date).Distinct().OrderBy(x => x).ToList();
            //    resultList = newList.GroupBy(x =>
            //    {
            //        string theGroup = "";
            //        attributeAfterFilter.ForEach(y =>
            //        {
            //            string title = y.GetCustomAttribute<DisplayNameAttribute>().DisplayName;

            //            //假如有這個群組的話
            //            if (groups.Contains(title))
            //            {
            //                theGroup += y.GetValue(x).ToString() + "/";
            //            }
            //        });
            //        theGroup = theGroup.TrimEnd('/');

            //        return theGroup;
            //    }).ToDictionary(x =>x.Key,y=>
            //    {
            //        var prices = dateList.Select(date=> y.Where(z=> z.Date == date).Sum(z => int.Parse (z.Price))).ToList();
            //        var result = new ShowAnalyzeDTO
            //        {
            //            DateList = dateList,
            //            TitlePrice = 
            //        };
            //        return result;
            //    }).ToList();
            //}


            return resultList;
        }

        public Dictionary<string, ShowAnalyzeDTO> Stack_GetRecord(DateTime fromDate, DateTime toDate, List<string> groups, Dictionary<string, List<string>> filters, string distin = "none")
        {
            RecordRepository recordRepository = new RecordRepository();
            List<RecordDAO> recordDAOs = recordRepository.GetRecords(fromDate, toDate);
            IEnumerable<AnalyzeDTO> analyzeDTOs = Mapper.Map<RecordDAO, AnalyzeDTO>(recordDAOs);
            //篩選有DisplayName的屬性
            var attributeAfterFilter = typeof(AnalyzeDTO).GetProperties().Where(x => x.GetCustomAttribute<DisplayNameAttribute>() != null).ToList();
            List<AnalyzeDTO> newList = new List<AnalyzeDTO>();

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

            var resultList = new Dictionary<string, ShowAnalyzeDTO>();
            if (distin == "stack")
            {
                var dateList = newList.Select(x => x.Date).Distinct().OrderBy(x => x).ToList();
                resultList = newList.GroupBy(x =>
                {
                    string theGroup = "";
                    attributeAfterFilter.ForEach(y =>
                    {
                        string title = y.GetCustomAttribute<DisplayNameAttribute>().DisplayName;

                        //假如有這個群組的話
                        if (groups.Contains(title))
                        {
                            theGroup += y.GetValue(x).ToString() + "/";
                        }
                    });
                    theGroup = theGroup.TrimEnd('/');

                    return theGroup;
                }).ToDictionary(x => x.Key, y =>
                {
                    var prices = dateList.Select(date => y.Where(z => z.Date == date).Sum(z => int.Parse(z.Price))).ToList();
                    var result = new ShowAnalyzeDTO
                    {
                        DateList = dateList,
                        PriceList = prices,
                    };
                    return result;
                });
            }


            return resultList;
        }
    }
}
