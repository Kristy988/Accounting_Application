using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NEW反射練習
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (Student student = new Student())
            {
                Console.WriteLine("AAAA");
            }


            //object obj = new Student();
            //object obj = SetValue<Student>();
            ////{
            ////    Id = 1,
            ////    Name = "Test",
            ////    Gender = "N"
            ////};

            //Type type = obj.GetType();
            //PropertyInfo[] props = type.GetProperties();
            //foreach (PropertyInfo prop in props)
            //{
            //    Console.WriteLine(prop.GetValue(obj));
            //}
        }

        public static T SetValue<T>() where T : class, new()
        {
            T t = new T();
            string[] datas = { "90", "Jolin", "F" };

            PropertyInfo[] props = t.GetType().GetProperties();


            for (int i = 0; i < props.Length; i++)
            {
                props[i].SetValue(t, datas[i]);
            }
            return t;
        }

    }
}
