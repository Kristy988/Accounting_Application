using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳APP.Components;

namespace 記帳APP.Views
{
    internal class SingletonForm
    {
        static Form form;
        static Dictionary<string, Form> formCheck = new Dictionary<string, Form>();
        public SingletonForm()
        {
        }
        public static Form GetForm(string pageText)
        {

            form?.Hide();
            if (formCheck.ContainsKey(pageText))
            {
                form = formCheck[pageText];
            }
            else
            {
                Type type = Type.GetType($"記帳APP.Views.{pageText}"); //放入完整類別的全稱內容
                form = (Form)Activator.CreateInstance(type);
                form.StartPosition = FormStartPosition.CenterScreen;
                formCheck.Add(pageText, form);
                form.FormClosed += Form_FormClosed;

            }

            FieldInfo fieldInfo = form.GetType().GetField("navbar1", BindingFlags.NonPublic | BindingFlags.Instance);
            Navbar navbar = (Navbar)fieldInfo.GetValue(form);
            navbar.Button_Click_Status(pageText);


            return form;
        }

        private static void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
