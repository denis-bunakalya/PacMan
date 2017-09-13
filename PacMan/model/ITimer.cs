using System;

namespace PacMan.model
{
    interface ITimer
    {
        event EventHandler Tick;
        void Start();

        void Stop();
        bool IsOn();
        void SetInterval(TimeSpan timeSpan);
    }
}
