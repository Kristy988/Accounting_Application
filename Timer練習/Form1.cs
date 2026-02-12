using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Timer練習
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer2;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form1_Load:" + Thread.CurrentThread.ManagedThreadId);
            timer1.Start();
            timer2 = new System.Timers.Timer();
            timer2.Interval = 100;
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(Timer2_Elapsed);
            timer2.SynchronizingObject = this;
            timer2.Start();
        }


        private void Timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            label2.Text = DateTime.Now.ToString("HH-mm-ss");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimerCallback callback = new TimerCallback(Do_Callback);
            System.Threading.Timer timer = new System.Threading.Timer(callback, null, 0, 1000);
        }
        private void Do_Callback(object state)
        {
            Console.WriteLine("Do_Callback:" + Thread.CurrentThread.ManagedThreadId);
            this.Invoke(new Action(() =>
            {
                label3.Text = DateTime.Now.ToString("HH-mm-ss");
                Console.WriteLine("Do_Callback_Invoke:" + Thread.CurrentThread.ManagedThreadId);

            }));
        }


        System.Threading.Timer timer_count;

        private void button2_Click(object sender, EventArgs e)
        {

            this.Debounce(() =>
            {
                int count = int.Parse(label4.Text) + 1;
                label4.Text = count.ToString();
            }, 400);


            //TimerCallback count_Callback = new TimerCallback(Do_Count);
            //if (timer_count == null)
            //    timer_count = new System.Threading.Timer(count_Callback, null, 400, -1);
            //timer_count.Change(400, -1);
        }

        private void Do_Count(object state)
        {
            int count = int.Parse(label4.Text) + 1;
            this.Invoke(new Action(() =>
            {
                label4.Text = count.ToString();
            }));
        }


    }
}
