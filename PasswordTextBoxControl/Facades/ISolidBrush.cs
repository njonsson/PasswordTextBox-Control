﻿using System;
using System.Drawing;

namespace PasswordTextBoxControl.Facades
{
    /// <summary>
    /// An abstraction of <see cref="System.Drawing.SolidBrush"/>.
    /// </summary>
    /// <remarks>Exists in order to make test assertions on usage of
    /// <see cref="System.Drawing.SolidBrush"/>.</remarks>
    public interface ISolidBrush : IDisposable
    {
        /// <summary>
        /// The <see cref="System.Drawing.SolidBrush"/> of which the
        /// <see cref="SolidBrush"/> is an abstraction.
        /// </summary>
        System.Drawing.SolidBrush Native { get; }
    }
}
