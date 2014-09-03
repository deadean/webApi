using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ViewModelLib
{
    public class csFunctionsGUI
    {
        public static void Wait(params WaitHandle[] waitHandles)
        {
            if (waitHandles == null || waitHandles.Length == 0)
                return;

            var remainingWaitHandles = waitHandles.ToList();

            foreach (var waitHandle in waitHandles.Where(waitHandle => waitHandle.WaitOne(0)))
            {
                remainingWaitHandles.Remove(waitHandle);
            }

            if (remainingWaitHandles.Count == 0)
                return;

            var timer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };

            var frame = new DispatcherFrame();

            timer.Tick += (o, e) =>
            {
                foreach (var waitHandle in remainingWaitHandles.Where(waitHandle => waitHandle.WaitOne(0)).ToList())
                {
                    remainingWaitHandles.Remove(waitHandle);
                }

                if (remainingWaitHandles.Count == 0)
                {
                    timer.Stop();

                    frame.Continue = false;
                }
            };

            timer.Start();

            Dispatcher.PushFrame(frame);
        }
    }
}
