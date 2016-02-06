using System.Diagnostics.CodeAnalysis;
using PasswordTextBoxControl.Facades;
using SolidBrush = System.Drawing.SolidBrush;

namespace PasswordTextBoxControl.Test.Unit
{
    [SuppressMessage("Microsoft.Design",
                     "CA1063:ImplementIDisposableCorrectly")]
    public sealed class MockSolidBrush : ISolidBrush
    {
        public int CallsToDispose { get; private set; }

        SolidBrush ISolidBrush.Native => null;

        [SuppressMessage("Microsoft.Design",
                         "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            CallsToDispose++;
        }
    }
}
