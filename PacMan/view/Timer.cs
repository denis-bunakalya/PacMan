using System;
using System.Windows.Threading;
using PacMan.model;

namespace PacMan.view
{
    sealed class Timer : DispatcherTimer, ITimer
    {
        public bool IsOn()
        {
            return IsEnabled;
        }

        public void SetInterval(TimeSpan timeSpan)
        {
            if (timeSpan == null)
            {
                throw new ArgumentException("time span must be not null");
            }
            Interval = timeSpan;
        }
    }
}
