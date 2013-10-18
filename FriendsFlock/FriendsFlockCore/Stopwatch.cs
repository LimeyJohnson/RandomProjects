using System;
using System.Diagnostics;
namespace FriendsFlockCore
{
    public class Stopwatch
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public bool Started { get; private set; }
        public bool Stopped { get; private set; }

        public TimeSpan TotalTime { get; private set; }

        private string _OperationName;

        public Stopwatch()
        {
            Started = false;
            Stopped = false;
        }

        public void Start(string OperationName)
        {
            StartTime = DateTime.Now;
            Started = true;
            _OperationName = OperationName;
            Debug.WriteLine(string.Format("Start: {0}", _OperationName));
        }

        //Restart
        public void Restart(string OperationName)
        {
            Stop();
            Start(OperationName);
        }

        public void Stop()
        {
            EndTime = DateTime.Now;
            Stopped = true;

            TotalTime = EndTime - StartTime;
            Debug.WriteLine(string.Format("Stop: {0}\tTime: {1}:{2}", _OperationName, TotalTime.Minutes, TotalTime.Seconds));
        }

        public string Ellapsed()
        {
            if (Started == false)
            {
                TotalTime = TimeSpan.Zero;
                return TotalTime.ToString();
            }

            if (Stopped == false)
            {
                Stop();
            }

            return TotalTime.ToString();
        }
        public int SecondsEllapsed()
        {
            if (Started == false)
            {
                TotalTime = TimeSpan.Zero;
                return 0;
            }

            if (Stopped == false)
            {
                Stop();
            }

            return TotalTime.Seconds;
        }
    }
}
