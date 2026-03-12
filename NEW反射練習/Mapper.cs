using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEW反射練習
{
    internal static class Mapper
    {
        public static Tdestination MappingName<Tsource, Tdestination>
            (Tsource Source, Action<IMappingExpression<Tsource, Tdestination>> configure = null)
        {
            //action要給什麼型別 CFG才能用formember

            var config = new MapperConfiguration(cfg =>
            {
                var trigger = cfg.CreateMap<Tsource, Tdestination>();
                if (configure != null)
                {
                    configure.Invoke(trigger);
                }

            });

            var createMap = config.CreateMapper();
            var newData = createMap.Map<Tdestination>(Source);
            return newData;
        }

        public static List<Tdestination> MappingName<Tsource, Tdestination>
           (List<Tsource> Source, Action<IMappingExpression<Tsource, Tdestination>> configure = null)
        {
            //action要給什麼型別 CFG才能用formember
            List<Tdestination> newList = new List<Tdestination>();
            var config = new MapperConfiguration(cfg =>
            {
                var trigger = cfg.CreateMap<Tsource, Tdestination>();
                if (configure != null)
                {
                    configure.Invoke(trigger);
                }

            });

            var createMap = config.CreateMapper();
            foreach (var item in Source)
            {
                var newData = createMap.Map<Tdestination>(item);
                newList.Add(newData);
            }
            return newList;
        }
    }
}
