using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordTextBoxControl.Facades;
using SolidBrush = PasswordTextBoxControl.Facades.SolidBrush;

namespace PasswordTextBoxControl.Test.Unit.Facades
{
    [SuppressMessage("Microsoft.Design",
                     "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class SolidBrushTest
    {
        protected SolidBrush                SolidBrush;
        protected Color                     Color;
        protected System.Drawing.SolidBrush NativeSolidBrush;

        [TestInitialize]
        public void TestInitialize()
        {
            Color = Color.White;
            SolidBrush = new SolidBrush(Color);
            NativeSolidBrush = ((ISolidBrush)SolidBrush).Native;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            NativeSolidBrush?.Dispose();
            NativeSolidBrush = null;

            SolidBrush?.Dispose();
            SolidBrush = null;
        }

        [TestMethod]
        public void HasTheExpectedColor()
        {
            Assert.AreEqual(Color, SolidBrush.Color);
        }

        [TestMethod]
        public void HasANativeObjectWithTheExpectedColor()
        {
            Assert.AreEqual(Color, NativeSolidBrush.Color);
        }
    }
}
