using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳APP.Views;

namespace 記帳APP
{
    //[Flags]
    //internal enum FileAccess
    //{
    //    None = 0, // 000
    //    Read = 1, // 001
    //    Write = 2, // 010
    //    Exec = 4, // 100

    //    //Read & Write => 011
    //    //Write & Exec => 110
    //    //Read & Write & Exec => 111

    //}
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {

            //FileAccess fileA = FileAccess.Read | FileAccess.Exec;
            ////101
            ////100
            ////-----
            ////100

            //Console.WriteLine(fileA);
            //if ((fileA & FileAccess.Exec) == FileAccess.Exec)
            //{

            //}





            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(SingletonForm.GetForm("記一筆"));

        }
    }
}
