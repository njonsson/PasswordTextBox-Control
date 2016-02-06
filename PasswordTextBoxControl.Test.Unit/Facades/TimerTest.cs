using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Timer = PasswordTextBoxControl.Facades.Timer;

namespace PasswordTextBoxControl.Test.Unit.Facades
{
    [SuppressMessage("Microsoft.Design",
                     "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class TimerTest
    {
        public class ASynchronizinObject : ISynchronizeInvoke
        {
            public bool InvokeRequired => false;

            public IAsyncResult BeginInvoke(Delegate method, object[] args)
            {
                return null;
            }

            public object EndInvoke(IAsyncResult result)
            {
                return null;
            }

            public object Invoke(Delegate method, object[] args)
            {
                return null;
            }
        }

        protected Timer               Timer;
        protected bool                AutoReset;
        protected double              Interval;
        protected ISynchronizeInvoke  SynchronizingObject;
        protected System.Timers.Timer NativeTimer;

        [TestInitialize]
        public void TestInitialize()
        {
            AutoReset = true;
            Interval = 123;
            SynchronizingObject = new ASynchronizinObject();
            Timer = new Timer(AutoReset, Interval, SynchronizingObject);
            NativeTimer = (System.Timers.Timer)Timer;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            NativeTimer?.Dispose();
            NativeTimer = null;

            Timer?.Dispose();
            Timer = null;
        }

        [TestMethod]
        public void HasTheExpectedAutoReset()
        {
            Assert.AreEqual(AutoReset, Timer.AutoReset);
        }

        [TestMethod]
        public void HasANativeObjectWithTheExpectedAutoReset()
        {
            Assert.AreEqual(AutoReset, NativeTimer.AutoReset);
        }

        [TestMethod]
        public void HasTheExpectedInterval()
        {
            Assert.AreEqual(Interval, Timer.Interval);
        }

        [TestMethod]
        public void HasANativeObjectWithTheExpectedInterval()
        {
            Assert.AreEqual(Interval, NativeTimer.Interval);
        }

        [TestMethod]
        public void HasTheExpectedSynchronizingObject()
        {
            Assert.AreSame(SynchronizingObject, Timer.SynchronizingObject);
        }

        [TestMethod]
        public void HasANativeObjectWithTheExpectedSynchronizingObject()
        {
            Assert.AreSame(SynchronizingObject,
                           NativeTimer.SynchronizingObject);
        }

        [TestMethod]
        public void WrapsTimerElapsedEvent()
        {
            NativeTimer.AutoReset = false;
            NativeTimer.Interval = 1;
            var callsToElapsed = 0;
            Timer.Elapsed += (sender, e) => callsToElapsed++;
            var thread = new Thread(() => NativeTimer.Start());
            thread.Start();
            Thread.Sleep(100);
            thread.Join();
            Assert.AreEqual(1, callsToElapsed);
        }

        [TestMethod]
        public void WrapsTimerIntervalProperty()
        {
            NativeTimer.Interval = 123;
            Assert.AreEqual(123, Timer.Interval);
            Timer.Interval = 456;
            Assert.AreEqual(456, NativeTimer.Interval);
        }

        [TestMethod]
        public void WrapsTimerStartMethod()
        {
            NativeTimer.AutoReset = false;
            NativeTimer.Interval = 1;
            var callsToElapsed = 0;
            NativeTimer.Elapsed += (sender, e) => callsToElapsed++;
            var thread = new Thread(() => Timer.Start());
            thread.Start();
            Thread.Sleep(100);
            thread.Join();
            Assert.AreEqual(1, callsToElapsed);
        }

        [TestMethod]
        public void WrapsTimerStopMethod()
        {
            NativeTimer.AutoReset = true;
            NativeTimer.Interval = 1;
            var callsToElapsed = 0;
            NativeTimer.Elapsed += (sender, e) => callsToElapsed++;
            var thread = new Thread(() => NativeTimer.Start());
            thread.Start();
            Thread.Sleep(100);
            thread.Join();
            Timer.Stop();
            var callsToElapsedSoFar = callsToElapsed;
            Thread.Sleep(100);
            thread.Join();
            Assert.AreEqual(callsToElapsedSoFar, callsToElapsed);
        }
    }
}
