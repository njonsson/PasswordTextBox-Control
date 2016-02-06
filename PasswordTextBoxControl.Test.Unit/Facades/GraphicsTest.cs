using System.Diagnostics.CodeAnalysis;
using System.Drawing.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphics = PasswordTextBoxControl.Facades.Graphics;

namespace PasswordTextBoxControl.Test.Unit.Facades
{
    [SuppressMessage("Microsoft.Design",
                     "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [TestClass]
    public class GraphicsTest
    {
        protected Graphics                Graphics;
        protected Control                 Control;
        protected System.Drawing.Graphics NativeGraphics;

        [TestInitialize]
        public void TestInitialize()
        {
            Control = new Control();
            Graphics = new Graphics(Control);
            NativeGraphics = (System.Drawing.Graphics)Graphics;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            NativeGraphics?.Dispose();
            NativeGraphics = null;

            Graphics?.Dispose();
            Graphics = null;

            Control?.Dispose();
            Control = null;
        }

        [TestMethod]
        public void WrapsGraphicsTextRenderingHintProperty()
        {
            NativeGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Assert.AreEqual(TextRenderingHint.AntiAliasGridFit,
                            Graphics.TextRenderingHint);
            Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Assert.AreEqual(TextRenderingHint.ClearTypeGridFit,
                            NativeGraphics.TextRenderingHint);
        }

        [Ignore]
        [TestMethod]
        public void WrapsGraphicsDrawStringMethod()
        {
            // TODO: Figure out how to test this
        }

        [Ignore]
        [TestMethod]
        public void WrapsGraphicsFillRectangleMethod()
        {
            // TODO: Figure out how to test this
        }

        [Ignore]
        [TestMethod]
        public void WrapsGraphicsMeasureStringMethod()
        {
            // TODO: Figure out how to test this
        }
    }
}
