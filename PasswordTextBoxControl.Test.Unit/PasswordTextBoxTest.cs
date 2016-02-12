using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordTextBoxControl.ComponentModel;
using PasswordTextBoxControl.Facades;

namespace PasswordTextBoxControl.Test.Unit
{
    [SuppressMessage("Microsoft.Design",
                     "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class PasswordTextBoxTest
    {
        public class Subclass : PasswordTextBox
        {
            public Subclass()
            {
                CallsToOnPasswordCharChanged = new List<ChangeEventArgs<char>>();
                CallsToOnPasswordCharChanging = new List<CancelChangeEventArgs<char>>();
                CallsToOnPasswordCharDelayChanged = new List<ChangeEventArgs<int>>();
                CallsToOnPasswordCharDelayChanging = new List<CancelChangeEventArgs<int>>();
                CallsToOnUseSystemPasswordCharChanged = new List<ChangeEventArgs<bool>>();
                CallsToOnUseSystemPasswordCharChanging = new List<CancelChangeEventArgs<bool>>();
            }

            public List<ChangeEventArgs<char>> CallsToOnPasswordCharChanged { get; protected set; }

            public List<CancelChangeEventArgs<char>> CallsToOnPasswordCharChanging { get; protected set; }

            public List<ChangeEventArgs<int>> CallsToOnPasswordCharDelayChanged { get; protected set; }

            public List<CancelChangeEventArgs<int>> CallsToOnPasswordCharDelayChanging { get; protected set; }

            public List<ChangeEventArgs<bool>> CallsToOnUseSystemPasswordCharChanged { get; protected set; }

            public List<CancelChangeEventArgs<bool>> CallsToOnUseSystemPasswordCharChanging { get; protected set; }

            public bool CancelOnPasswordCharChanging { get; set; }

            public bool CancelOnPasswordCharDelayChanging { get; set; }

            public bool CancelOnUseSystemPasswordCharChanging { get; set; }

            protected override void OnPasswordCharChanged(ChangeEventArgs<char> e)
            {
                CallsToOnPasswordCharChanged.Add(e);

                base.OnPasswordCharChanged(e);
            }

            protected override void OnPasswordCharChanging(CancelChangeEventArgs<char> e)
            {
                CallsToOnPasswordCharChanging.Add(e);
                e.Cancel = CancelOnPasswordCharChanging;

                base.OnPasswordCharChanging(e);
            }

            protected override void OnPasswordCharDelayChanged(ChangeEventArgs<int> e)
            {
                CallsToOnPasswordCharDelayChanged.Add(e);

                base.OnPasswordCharDelayChanged(e);
            }

            protected override void OnPasswordCharDelayChanging(CancelChangeEventArgs<int> e)
            {
                CallsToOnPasswordCharDelayChanging.Add(e);
                e.Cancel = CancelOnPasswordCharDelayChanging;

                base.OnPasswordCharDelayChanging(e);
            }

            protected override void OnUseSystemPasswordCharChanged(ChangeEventArgs<bool> e)
            {
                CallsToOnUseSystemPasswordCharChanged.Add(e);

                base.OnUseSystemPasswordCharChanged(e);
            }

            protected override void OnUseSystemPasswordCharChanging(CancelChangeEventArgs<bool> e)
            {
                CallsToOnUseSystemPasswordCharChanging.Add(e);
                e.Cancel = CancelOnUseSystemPasswordCharChanging;

                base.OnUseSystemPasswordCharChanging(e);
            }
        }

        public struct CallToInjectTimer
        {
            public bool AutoReset;

            public double Interval;

            public ISynchronizeInvoke SynchronizingObject;
        }

        protected PasswordTextBox PasswordTextBox;

        protected Func<Control, IGraphics> OriginalGraphicsConstructor;
        protected List<Control>            CallsToNewGraphics;
        protected List<MockGraphics>       GraphicsObjects;

        protected Func<Color, ISolidBrush> OriginalSolidBrushConstructor;
        protected List<Color>              CallsToNewSolidBrush;
        protected List<MockSolidBrush>     SolidBrushes;

        protected Func<bool, double, ISynchronizeInvoke, ITimer> OriginalTimerConstructor;
        protected List<CallToInjectTimer>                        CallsToNewTimer;
        protected List<MockTimer>                                Timers;

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
        public void IsNotMultiline()
        {
            Assert.IsFalse(PasswordTextBox.Multiline);
        }

        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
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

        [TestMethod]
        public void HasExpectedPasswordChar()
        {
            Assert.AreEqual('\0', PasswordTextBox.PasswordChar);
        }

        [TestMethod]
        public void RaisesPasswordCharChangingAsExpected()
        {
            var actual = new List<CancelChangeEventArgs<char>>();
            PasswordTextBox.PasswordCharChanging += delegate(object sender,
                                                             CancelChangeEventArgs<char> e)
            {
                actual.Add(e);
            };
            PasswordTextBox.PasswordChar = '|';
            var expected = new[]
            {
                new CancelChangeEventArgs<char>('\0', '|', false)
            };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CallsOnPasswordCharChangingAsExpected()
        {
            var subclassed = new Subclass { PasswordChar = '|' };
            var expected = new[]
            {
                new CancelChangeEventArgs<char>('\0', '|', false)
            };
            CollectionAssert.AreEqual(expected,
                                      subclassed.CallsToOnPasswordCharChanging);
        }

        [TestMethod]
        public void HonorsPasswordCharChangingCancelation()
        {
            var cancel = false;
            PasswordTextBox.PasswordCharChanging += delegate (object sender,
                                                             CancelChangeEventArgs<char> e)
            {
                // ReSharper disable once AccessToModifiedClosure
                e.Cancel = cancel;
            };
            PasswordTextBox.PasswordChar = '|';
            cancel = true;
            PasswordTextBox.PasswordChar = '+';
            Assert.AreEqual('|', PasswordTextBox.PasswordChar);
        }

        [TestMethod]
        public void HonorsOnPasswordCharChangingCancelation()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var subclassed = new Subclass { PasswordChar = '|' };
            subclassed.CancelOnPasswordCharChanging = true;
            subclassed.PasswordChar = '+';
            Assert.AreEqual('|', subclassed.PasswordChar);
        }

        [TestMethod]
        public void RaisesPasswordCharChangedAsExpected()
        {
            var actual = new List<ChangeEventArgs<char>>();
            PasswordTextBox.PasswordCharChanged += delegate (object sender,
                                                             ChangeEventArgs<char> e)
            {
                actual.Add(e);
            };
            PasswordTextBox.PasswordChar = '|';
            var expected = new[] { new ChangeEventArgs<char>('\0', '|') };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CallsOnPasswordCharChangedAsExpected()
        {
            var subclassed = new Subclass { PasswordChar = '|' };
            var expected = new[]
            {
                new ChangeEventArgs<char>('\0', '|')
            };
            CollectionAssert.AreEqual(expected,
                                      subclassed.CallsToOnPasswordCharChanged);
        }

        [TestMethod]
        public void HasExpectedPasswordCharDelay()
        {
            Assert.AreEqual(1000, PasswordTextBox.PasswordCharDelay);
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

        [TestMethod]
        public void RaisesPasswordCharDelayChangingAsExpected()
        {
            var actual = new List<CancelChangeEventArgs<int>>();
            PasswordTextBox.PasswordCharDelayChanging += delegate (object sender,
                                                                   CancelChangeEventArgs<int> e)
            {
                actual.Add(e);
            };
            PasswordTextBox.PasswordCharDelay = 999;
            var expected = new[]
            {
                new CancelChangeEventArgs<int>(1000, 999, false)
            };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CallsOnPasswordCharDelayChangingAsExpected()
        {
            var subclassed = new Subclass { PasswordCharDelay = 999 };
            var expected = new[]
            {
                new CancelChangeEventArgs<int>(1000, 999, false)
            };
            CollectionAssert.AreEqual(expected,
                                      subclassed.CallsToOnPasswordCharDelayChanging);
        }

        [TestMethod]
        public void HonorsPasswordCharDelayChangingCancelation()
        {
            var cancel = false;
            PasswordTextBox.PasswordCharDelayChanging += delegate (object sender,
                                                                   CancelChangeEventArgs<int> e)
            {
                // ReSharper disable once AccessToModifiedClosure
                e.Cancel = cancel;
            };
            PasswordTextBox.PasswordCharDelay = 999;
            cancel = true;
            PasswordTextBox.PasswordCharDelay = 998;
            Assert.AreEqual(999, PasswordTextBox.PasswordCharDelay);
        }

        [TestMethod]
        public void HonorsOnPasswordCharDelayChangingCancelation()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var subclassed = new Subclass { PasswordCharDelay = 999 };
            subclassed.CancelOnPasswordCharDelayChanging = true;
            subclassed.PasswordCharDelay = 998;
            Assert.AreEqual(999, subclassed.PasswordCharDelay);
        }

        [TestMethod]
        public void RaisesPasswordCharDelayChangedAsExpected()
        {
            var actual = new List<ChangeEventArgs<int>>();
            PasswordTextBox.PasswordCharDelayChanged += delegate (object sender,
                                                                  ChangeEventArgs<int> e)
            {
                actual.Add(e);
            };
            PasswordTextBox.PasswordCharDelay = 999;
            var expected = new[] { new ChangeEventArgs<int>(1000, 999) };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CallsOnPasswordCharDelayChangedAsExpected()
        {
            var subclassed = new Subclass { PasswordCharDelay = 999 };
            var expected = new[]
            {
                new ChangeEventArgs<int>(1000, 999)
            };
            CollectionAssert.AreEqual(expected,
                                      subclassed.CallsToOnPasswordCharDelayChanged);
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
        public void RaisesUseSystemPasswordCharChangingAsExpected()
        {
            var actual = new List<CancelChangeEventArgs<bool>>();
            PasswordTextBox.UseSystemPasswordCharChanging += delegate (object sender,
                                                                       CancelChangeEventArgs<bool> e)
            {
                actual.Add(e);
            };
            PasswordTextBox.UseSystemPasswordChar = false;
            var expected = new[]
            {
                new CancelChangeEventArgs<bool>(true, false, false)
            };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CallsOnUseSystemPasswordCharChangingAsExpected()
        {
            var subclassed = new Subclass { UseSystemPasswordChar = false };
            var expected = new[]
            {
                new CancelChangeEventArgs<bool>(true, false, false)
            };
            CollectionAssert.AreEqual(expected,
                                      subclassed.CallsToOnUseSystemPasswordCharChanging);
        }

        [TestMethod]
        public void HonorsUseSystemPasswordCharChangingCancelation()
        {
            var cancel = false;
            PasswordTextBox.UseSystemPasswordCharChanging += delegate (object sender,
                                                                       CancelChangeEventArgs<bool> e)
            {
                // ReSharper disable once AccessToModifiedClosure
                e.Cancel = cancel;
            };
            PasswordTextBox.UseSystemPasswordChar = false;
            cancel = true;
            PasswordTextBox.UseSystemPasswordChar = true;
            Assert.IsFalse(PasswordTextBox.UseSystemPasswordChar);
        }

        [TestMethod]
        public void HonorsOnUseSystemPasswordCharChangingCancelation()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var subclassed = new Subclass { UseSystemPasswordChar = false };
            subclassed.CancelOnUseSystemPasswordCharChanging = true;
            subclassed.UseSystemPasswordChar = true;
            Assert.IsFalse(subclassed.UseSystemPasswordChar);
        }

        [TestMethod]
        public void RaisesUseSystemPasswordCharChangedAsExpected()
        {
            var actual = new List<ChangeEventArgs<bool>>();
            PasswordTextBox.UseSystemPasswordCharChanged += delegate (object sender,
                                                                      ChangeEventArgs<bool> e)
            {
                actual.Add(e);
            };
            PasswordTextBox.UseSystemPasswordChar = false;
            var expected = new[] { new ChangeEventArgs<bool>(true, false) };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CallsOnUseSystemPasswordCharChangedAsExpected()
        {
            var subclassed = new Subclass { UseSystemPasswordChar = false };
            var expected = new[]
            {
                new ChangeEventArgs<bool>(true, false)
            };
            CollectionAssert.AreEqual(expected,
                                      subclassed.CallsToOnUseSystemPasswordCharChanged);
        }
    }
}
