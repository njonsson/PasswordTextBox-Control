using System.Diagnostics.CodeAnalysis;
using System.Timers;
using PasswordTextBoxControl.Facades;

namespace PasswordTextBoxControl.Test.Unit
{
    [SuppressMessage("Microsoft.Design",
                     "CA1063:ImplementIDisposableCorrectly")]
    public class MockTimer : ITimer
    {
        public event ElapsedEventHandler Elapsed
        {
            // ReSharper disable once ValueParameterNotUsed
            add { ElapsedEventSubscribers++; }

            // ReSharper disable once ValueParameterNotUsed
            remove { ElapsedEventSubscribers--; }
        }

        public int CallsToDispose { get; protected set; }

        public int CallsToStart { get; protected set; }

        public int CallsToStop { get; protected set; }

        public int ElapsedEventSubscribers { get; protected set; }

        public double Interval { get; set; }

        [SuppressMessage("Microsoft.Design",
                         "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            CallsToDispose++;
        }

        public void Start()
        {
            CallsToStart++;
        }

        public void Stop()
        {
            CallsToStop++;
        }
    }
}
