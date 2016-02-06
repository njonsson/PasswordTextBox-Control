using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Text;
using PasswordTextBoxControl.Facades;

namespace PasswordTextBoxControl.Test.Unit
{
    [SuppressMessage("Microsoft.Design",
                     "CA1063:ImplementIDisposableCorrectly")]
    public class MockGraphics : IGraphics
    {
        public struct CallToDrawString
        {
            public Font Font;

            public PointF Point;

            public ISolidBrush SolidBrush;

            public string String;
        }

        public struct CallToFillRectangle
        {
            public RectangleF Rectangle;

            public ISolidBrush SolidBrush;
        }

        public struct CallToMeasureString
        {
            public Font Font;

            public string String;
        }

        public MockGraphics()
        {
            CallsToDrawString    = new List<CallToDrawString>();
            CallsToFillRectangle = new List<CallToFillRectangle>();
            CallsToMeasureString = new List<CallToMeasureString>();
        }

        public int CallsToDispose { get; protected set; }

        public List<CallToDrawString> CallsToDrawString { get; protected set; }

        public List<CallToFillRectangle> CallsToFillRectangle { get; protected set; }

        public List<CallToMeasureString> CallsToMeasureString { get; protected set; }

        public SizeF ReturnValueFromMeasureString { get; set; }

        public TextRenderingHint TextRenderingHint { get; set; }

        [SuppressMessage("Microsoft.Design",
                         "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            CallsToDispose++;
        }

        public void DrawString(string @string,
                               Font font,
                               ISolidBrush solidBrush,
                               PointF point)
        {
            CallsToDrawString.Add(new CallToDrawString
            {
                String = @string,
                Font = font,
                SolidBrush = solidBrush,
                Point = point
            });
        }

        public void FillRectangle(ISolidBrush solidBrush, RectangleF rectangle)
        {
            CallsToFillRectangle.Add(new CallToFillRectangle
            {
                SolidBrush = solidBrush,
                Rectangle = rectangle
            });
        }

        public SizeF MeasureString(string @string, Font font)
        {
            CallsToMeasureString.Add(new CallToMeasureString
            {
                String = @string,
                Font = font
            } );
            return ReturnValueFromMeasureString;
        }
    }
}
