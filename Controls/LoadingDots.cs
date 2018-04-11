using System;
using System.Timers;

namespace Controls
{
    public class LoadingDots
    {
        #region Constructor
    
        /// <summary>
        /// Control that shows that program is doing or loading something 
        /// </summary>
        public LoadingDots(string mainText)
        {
            MainText = mainText;
        }

        public LoadingDots(string mainText, int interval, string endtext)
        {
            MainText = mainText;
            Interval = Interval;
            EndText = endtext;
        }

        public LoadingDots(string mainText, int interval)
        {
            MainText = mainText;
            Interval = Interval;
        }

        #endregion


        public string MainText {get;set;}
        public int Interval {get;set;}
        public string EndText{get;set;}

        Timer timer;
        public void Start()
        {
            Console.Write(MainText);

            timer = new Timer();

            if(Interval != 0)
             timer.Interval = Interval;
            else
             timer.Interval = 500;
            
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            timer.Start();
        }

        private void timer_Elapsed(Object source, ElapsedEventArgs e)
        {
            Console.Write(".");
        }

        public void Stop()
        {
            if(EndText != null)
                Console.Write(EndText);
            else
                Console.Write("Done!");

            timer.Stop();
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}