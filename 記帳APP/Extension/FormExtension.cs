using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 記帳APP.Extension
{
    internal static class FormExtension
    {
        static System.Threading.Timer timer_count;
        private static Action DefaultAction;
        private static Form DefaultForm;
        public static void Debounce(this Form form, Action action, int dueTime)
        {
            TimerCallback Callback_way_Callback = new TimerCallback(Callback_way);
            if (timer_count == null)
                timer_count = new System.Threading.Timer(Callback_way_Callback, null, dueTime, -1);
            timer_count.Change(dueTime, -1);

            DefaultAction = action;
            DefaultForm = form;

        }

        private static void Callback_way(object state)
        {
            DefaultForm.Invoke(new Action(() =>
            {
                DefaultAction();
            }));
        }
    }
}
