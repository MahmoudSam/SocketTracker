using System;
using System.Timers;

namespace Controls
{
    
    public class ProgressBar
    {
        #region Constructor

        /// <summary>
        /// Simple progress bar that allows you to change default values(Maximum, widht(not yet implemented)) at runtime. Don't use Console.WriteLine() while using ProgressBar 
        /// </summary>
        public ProgressBar()
        {
            Maximum = 100;
            Widht = Console.WindowWidth / 2;
        } 
        
        /// <param name="maximum">Maximum value allowed. Default is 100</param>
        public ProgressBar(int maximum)
        {
            Maximum = maximum;
            Widht = Console.WindowWidth / 2;
        }

        /// <param name="maximum">Maximum value allowed. Default is 100</param>
        /// <param name="widht">Widht of ProgressBar. Default is 2/Console.Widht</param>
        public ProgressBar(int maximum, int widht)
        {
            Maximum = maximum;
            Widht = widht;
        }
        #endregion

        private float progressPerChar;
        private uint blocksWithProgress;


        public double ElapsedTime{get;set;}
        //Is Bar drawed
        public bool IsDrawed{get;set;}

        /// <summary>
        /// Widht of ProgressBar
        /// </summary>
        public int Widht 
        {
            get
            {
                return _widht;
            }
            set
            {

                _widht = value;
                progressPerChar = (float) Maximum / Widht;

                if(IsDrawed == true)
                {
                    //Clean line
                    Console.CursorLeft = 0;
                    Console.Write(new string(' ', Console.WindowWidth));
                    Update(blocksWithProgress);

                    //Redraw
                    Draw();
                    progressPerChar = (float) _maximum / Widht;
                    blocksWithProgress = (uint) Math.Round(_value / progressPerChar);
                    Update(blocksWithProgress);
                }
            }
        }
        private int _widht;

        /// <summary>
        /// Maximum value(end value)
        /// </summary>
        public int Maximum 
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value; 
                
                progressPerChar = (float) _maximum / Widht;
                blocksWithProgress = (uint) Math.Round(_value / progressPerChar);

                if(IsDrawed == true)
                   Update(blocksWithProgress);
            }
        }
        private int _maximum;

        /// <summary>
        /// Current value
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if(value > Maximum)
                 _value = Maximum;
                else
                 _value = value;

                blocksWithProgress = (uint) Math.Round(_value / progressPerChar);

                if(IsDrawed == true)
                   Update(blocksWithProgress);
            }
        }
        private double _value;

        Timer timer;

        /// <summary>
        /// Draws empty ProgressBar
        /// </summary>
        public void Draw()
        {
            if(Widht == 0)
               Widht = Console.WindowWidth;

            Console.CursorLeft = 0;
            Console.Write("[");
            Console.CursorLeft = Widht;
            Console.Write("]");

            IsDrawed = true;
            
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            timer.Start();
        }

        private void Update(uint blocks)
        {
            Console.CursorLeft = 1;

            for(int i = 1; i < Widht; i++)
            {
                if(i <= blocks)
                  Console.Write("#");
                else
                  Console.Write(" ");
            }
            UpdateText();
        }
        
        
        private void timer_Elapsed(Object source, ElapsedEventArgs e)
        {
            ElapsedTime += 1;
        }

        private void UpdateText()
        {
            Console.CursorLeft = Widht + 1;

            //Bug that causes some text to be left...
            Console.Write("{0} of {1} ({2}%)", Math.Round(Value, 2), Maximum, (Value/Maximum) * 100);
        }

        /// <summary>
        /// Releases all resources
        /// </summary>
        public void Dispose()
        {
            timer.Dispose();
            /*Maximum = 0;
            Widht = 0;
            Value = 0;*/
        }
    }

}