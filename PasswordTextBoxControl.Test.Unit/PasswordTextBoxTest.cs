using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordTextBoxControl.Facades;

namespace PasswordTextBoxControl.Test.Unit
{
    [SuppressMessage("Microsoft.Design",
                     "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class PasswordTextBoxTest
    {
        public struct CallToInjectTimer
        {
            public bool AutoReset;

            public double Interval;

            public ISynchronizeInvoke SynchronizingObject;
        }

        protected PasswordTextBox PasswordTextBox;

        protected Func<Control, IGraphics> OriginalGraphicsConstructor;
        protected List<Control>      CallsToNewGraphics;
        protected List<MockGraphics> GraphicsObjects;

        protected Func<Color, ISolidBrush> OriginalSolidBrushConstructor;
        protected List<Color>          CallsToNewSolidBrush;
        protected List<MockSolidBrush> SolidBrushes;

        protected Func<bool, double, ISynchronizeInvoke, ITimer> OriginalTimerConstructor;
        protected List<CallToInjectTimer> CallsToNewTimer;
        protected List<MockTimer>         Timers;

        [TestInitialize]
        public void TestInitialize()
        {
            OriginalGraphicsConstructor = PasswordTextBox.NewGraphics;
            CallsToNewGraphics = new List<Control>();
            GraphicsObjects = new List<MockGraphics>();
            PasswordTextBox.NewGraphics = delegate(Control control)
            {
                CallsToNewGraphics.Add(control);
                var graphics = new MockGraphics();
                GraphicsObjects.Add(graphics);
                return graphics;
            };

            OriginalSolidBrushConstructor = PasswordTextBox.NewSolidBrush;
            CallsToNewSolidBrush = new List<Color>();
            SolidBrushes = new List<MockSolidBrush>();
            PasswordTextBox.NewSolidBrush = delegate(Color color)
            {
                CallsToNewSolidBrush.Add(color);
                var solidBrush = new MockSolidBrush();
                SolidBrushes.Add(solidBrush);
                return solidBrush;
            };

            OriginalTimerConstructor = PasswordTextBox.NewTimer;
            CallsToNewTimer = new List<CallToInjectTimer>();
            Timers = new List<MockTimer>();
            PasswordTextBox.NewTimer = delegate(bool autoReset,
                                                double interval,
                                                ISynchronizeInvoke synchronizingObject)
            {
                CallsToNewTimer.Add(new CallToInjectTimer
                {
                    AutoReset = autoReset,
                    Interval = interval,
                    SynchronizingObject = synchronizingObject
                });
                var timer = new MockTimer();
                Timers.Add(timer);
                return timer;
            };

            PasswordTextBox = new PasswordTextBox();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PasswordTextBox.NewGraphics   = OriginalGraphicsConstructor;
            PasswordTextBox.NewSolidBrush = OriginalSolidBrushConstructor;
            PasswordTextBox.NewTimer      = OriginalTimerConstructor;

            PasswordTextBox.Dispose();
        }

        [TestMethod]
        public void HasExpectedPasswordChar()
        {
            Assert.AreEqual('\0', PasswordTextBox.PasswordChar);
        }

        [TestMethod]
        public void HasExpectedPasswordCharDelay()
        {
            Assert.AreEqual(1000, PasswordTextBox.PasswordCharDelay);
        }

        [TestMethod]
        public void HasExpectedPasswordCharEffective()
        {
            Assert.AreEqual('*', PasswordTextBox.PasswordCharEffective);
        }

        [TestMethod]
        public void UsesSystemPasswordChar()
        {
            Assert.IsTrue(PasswordTextBox.UseSystemPasswordChar);
        }

        [TestMethod]
        public void IsNotMultiline()
        {
            Assert.IsFalse(PasswordTextBox.Multiline);
        }

        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes=false)]
        [TestMethod]
        public void RejectsMultiline()
        {
            try
            {
                PasswordTextBox.Multiline = true;
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual($"{PasswordTextBox.GetType().FullName} does not support Multiline.",
                                e.Message);
                Assert.IsNull(e.ParamName);
                throw;
            }
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException),
                           AllowDerivedTypes=false)]
        [TestMethod]
        public void PasswordCharDelayRejectsANegativeValue()
        {
            PasswordTextBox.NewTimer = OriginalTimerConstructor;
            PasswordTextBox = new PasswordTextBox();
            try
            {
                PasswordTextBox.PasswordCharDelay = -1;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(-1, e.ActualValue);
                StringAssert.StartsWith(e.Message,
                                        "Must be greater than zero.");
                Assert.IsNull(e.ParamName);
                throw;
            }
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException),
                           AllowDerivedTypes=false)]
        [TestMethod]
        public void PasswordCharDelayRejectsAZeroValue()
        {
            PasswordTextBox.NewTimer = OriginalTimerConstructor;
            PasswordTextBox = new PasswordTextBox();
            try
            {
                PasswordTextBox.PasswordCharDelay = 0;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(0, e.ActualValue);
                StringAssert.StartsWith(e.Message,
                                        "Must be greater than zero.");
                Assert.IsNull(e.ParamName);
                throw;
            }
        }

        [TestMethod]
        public void PasswordCharDelayWrapsTimerIntervalProperty()
        {
            PasswordTextBox.PasswordCharDelay = 123;
            Assert.AreEqual(1, Timers.Count);
            Assert.AreEqual(123, Timers[0].Interval);
        }
    }
}
