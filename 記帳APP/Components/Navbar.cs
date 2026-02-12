using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳APP.Views;

namespace 記帳APP.Components
{
    public partial class Navbar : UserControl
    {
        static Form form;
        public Navbar()
        {
            InitializeComponent();

            var pages = Assembly.GetExecutingAssembly().DefinedTypes
                .Where(x => x.BaseType == typeof(Form) &&
                x.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Any(y => y.FieldType == typeof(Navbar)))
                .Select(x => x.Name)
                .ToList();


            for (int i = 0; i < pages.Count; i++)
            {
                Button button = new Button();
                button.Text = pages[i];
                button.Height = buttonContainer.Height;
                button.Width = buttonContainer.Width / pages.Count;
                button.Click += ChangePage_Click;
                buttonContainer.Controls.Add(button);
            }

        }
        public void Button_Click_Status(string pageName)
        {

            buttonContainer.Controls
               .OfType<Button>()
               .ToList()
               .ForEach(x => x.Enabled = x.Text != pageName);
        }
        private void ChangePage_Click(object sender, EventArgs e)
        {
            Button buttonClick = (Button)sender;
            if (!buttonClick.Enabled)
                return;

            form = SingletonForm.GetForm(buttonClick.Text);
            form.Show();

        }



        private void Navbar_SizeChanged(object sender, EventArgs e)
        {
            var allButtons = buttonContainer.Controls
               .OfType<Button>() //從集合中找出所有型別是button的
               .ToList();

            foreach (var button in allButtons)
            {
                button.Margin = new Padding(0);//Margin元件與其他元件間的外距
                button.Width = this.Size.Width / allButtons.Count;
            }
        }
    }
}
