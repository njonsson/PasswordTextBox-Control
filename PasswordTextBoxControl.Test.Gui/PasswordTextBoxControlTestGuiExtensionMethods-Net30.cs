using System.Drawing;
using System.Windows.Forms;

namespace PasswordTextBoxControl.Test.Gui
{
    public static class PasswordTextBoxControlTestGuiExtensionMethods
    {
        /// <remarks>
        /// GetCharIndexFromPosition is missing one caret position, as there is
        /// one extra caret position than there are characters (an extra one at
        /// the end). See http://stackoverflow.com/a/3874132.
        /// </remarks>
        public static int GetCaretIndex(TextBoxBase @this, int x, int y)
        {
            return GetCaretIndex(@this, new Point(x, y));
        }

        /// <remarks>
        /// GetCharIndexFromPosition is missing one caret position, as there is
        /// one extra caret position than there are characters (an extra one at
        /// the end). See http://stackoverflow.com/a/3874132.
        /// </remarks>
        public static int GetCaretIndex(TextBoxBase @this, Point point)
        {
            point = @this.PointToClient(point);
            var index = @this.GetCharIndexFromPosition(point);
            if (index == (@this.TextLength - 1))
            {
                var caretPoint = @this.GetPositionFromCharIndex(index);
                if (caretPoint.X < point.X) index += 1;
            }
            return index;
        }
    }
}
