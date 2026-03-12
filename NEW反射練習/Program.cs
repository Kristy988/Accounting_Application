using AutoMapper;
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

            Student student = new Student();
            student.Name = "Test";
            student.Title = "AAA";
            student.Gender = "F";
            student.Id = "10";
            Student student2 = new Student();
            student2.Title = "BBB";
            student2.Gender = "F";
            student2.Id = "11";
            student2.Name = "Test2";
            List<Student> students = new List<Student>();
            students.Add(student);
            students.Add(student2);

            StudentDTO studentDTO = new StudentDTO();
            studentDTO = Mapper.MappingName<Student, StudentDTO>
                (student, x => x.ForMember(y => y.NickName, z => z.MapFrom(o => o.Title)));

            List<StudentDTO> studentDTOs = new List<StudentDTO>();
            studentDTOs = Mapper.MappingName<Student, StudentDTO>
                (students, x => x.ForMember(y => y.NickName, z => z.MapFrom(o => o.Title)));
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Student, StudentDTO>()
            //       .ForMember(x => x.NickName, y => y.MapFrom(o => o.Title))
            //       .ReverseMap();

            //});

            //var Mapper = config.CreateMapper();
            //var newData = Mapper.Map<StudentDTO>(student);


            //多型 list 轉 list or DAO to DTO 


            //using (Student student = new Student())
            //{
            //    Console.WriteLine("AAAA");
            //}


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
            Console.ReadKey();
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
