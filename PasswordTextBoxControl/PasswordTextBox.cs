using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Timers;
using System.Windows.Forms;
using PasswordTextBoxControl.ComponentModel;
using PasswordTextBoxControl.Facades;
using Graphics = PasswordTextBoxControl.Facades.Graphics;
using SolidBrush = PasswordTextBoxControl.Facades.SolidBrush;
using Timer = PasswordTextBoxControl.Facades.Timer;

namespace PasswordTextBoxControl
{
    [DefaultProperty("PasswordCharDelay")]
    [Description("Enables the user to enter password input, momentarily showing each character entered.")]
    [ToolboxBitmap(typeof(PasswordTextBox), "toolbox.bmp")]
    [ToolboxItem(true)]
    [ToolboxItemFilter("Common Controls")]
    public class PasswordTextBox : TextBox
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Func<bool> IsFontSmoothingEnabled = () => SystemInformation.IsFontSmoothingEnabled;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Func<Control, IGraphics> NewGraphics = self => new Graphics(self);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Func<Color, ISolidBrush> NewSolidBrush = color => new SolidBrush(color);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Func<bool, double, ISynchronizeInvoke, ITimer> NewTimer = (autoReset, interval, synchronizingObject) => new Timer(autoReset, interval, synchronizingObject);

        /// <summary>
        /// The default value of <see cref="PasswordChar"/>.
        /// </summary>
        public const char DefaultPasswordChar = '\0';

        /// <summary>
        /// The default value of <see cref="PasswordCharDelay"/>.
        /// </summary>
        public const int DefaultPasswordCharDelay = 1000;

        /// <summary>
        /// The default value of <see cref="UseSystemPasswordChar"/>.
        /// </summary>
        public const bool DefaultUseSystemPasswordChar = true;

        private char   passwordChar;
        private int    passwordCharDelay;
        private string textPrevious;
        private ITimer timer;

        /// <summary>
        /// Instantiates a new <see cref="PasswordTextBox"/>.
        /// </summary>
        public PasswordTextBox()
        {
            passwordChar               = DefaultPasswordChar;
            passwordCharDelay          = DefaultPasswordCharDelay;
            base.UseSystemPasswordChar = DefaultUseSystemPasswordChar;
            SetUpTimer(true);
        }

        /// <summary>
        /// Occurs before the <see cref="PasswordChar"/> property value changes.
        /// </summary>
        public event EventHandler<CancelChangeEventArgs<char>> PasswordCharChanging;

        /// <summary>
        /// Occurs when the <see cref="PasswordChar"/> property value changes.
        /// </summary>
        public event EventHandler<ChangeEventArgs<char>> PasswordCharChanged;

        /// <summary>
        /// Occurs before the <see cref="PasswordCharDelay"/> property value
        /// changes.
        /// </summary>
        public event EventHandler<CancelChangeEventArgs<int>> PasswordCharDelayChanging;

        /// <summary>
        /// Occurs when the <see cref="PasswordCharDelay"/> property value
        /// changes.
        /// </summary>
        public event EventHandler<ChangeEventArgs<int>> PasswordCharDelayChanged;

        /// <summary>
        /// Occurs before the <see cref="UseSystemPasswordChar"/> property value
        /// changes.
        /// </summary>
        public event EventHandler<CancelChangeEventArgs<bool>> UseSystemPasswordCharChanging;

        /// <summary>
        /// Occurs when the <see cref="UseSystemPasswordChar"/> property value
        /// changes.
        /// </summary>
        public event EventHandler<ChangeEventArgs<bool>> UseSystemPasswordCharChanged;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Multiline {
            get { return base.Multiline; }

            set
            {
                if (value)
                {
                    throw new ArgumentException($"{GetType().FullName} does not support Multiline.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the character used to mask characters of a password in
        /// a <see cref="PasswordTextBox"/> control.
        /// </summary>
        /// <returns>A <see cref="char"/>. Set the value of this property to
        /// <c>'\0'</c> (character value) if you do not want the control to mask
        /// characters as they are typed. Equals 0 (character value) by default.</returns>
        [DefaultValue(DefaultPasswordChar)]
        [Description("Indicates the character to display for password input in a PasswordTextBox control.")]
        public new char PasswordChar
        {
            get { return passwordChar; }

            set
            {
                if ((passwordChar == value) && (base.PasswordChar == value))
                {
                    return;
                }

                var eventArgs = CancelChangeEventArgs<char>.DoIf(this,
                                                                 PasswordCharChanging,
                                                                 OnPasswordCharChanging,
                                                                 PasswordChar,
                                                                 value);
                if (eventArgs?.Cancel == true) return;

                var oldValue = PasswordChar;
                passwordChar = base.PasswordChar = value;
                ChangeEventArgs<char>.DoIf(this,
                                           PasswordCharChanged,
                                           OnPasswordCharChanged,
                                           oldValue,
                                           PasswordChar);
            }
        }

        /// <summary>
        /// Gets or sets the time in milliseconds during which password input is
        /// legible before appearing as the password character.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Value is less than or
        /// equal to zero.</exception>
        [Category("Behavior")]
        [DefaultValue(DefaultPasswordCharDelay)]
        [Description("Indicates the time in milliseconds during which password input is legible before appearing as the password character.")]
        public int PasswordCharDelay
        {
            get { return passwordCharDelay; }

            set
            {
                var t = timer;
                if (ReferenceEquals(t, null))
                {
                    Debug.WriteLine($"Ignoring {GetType().FullName}#PasswordCharDelay property setting {value} at:");
#if DEBUG
                    Util.DebugWithIndentation(() => Util.DebugStackTrace());
#endif
                    return;
                }

                if ((passwordCharDelay == value) && ((int)t.Interval == value))
                {
                    return;
                }

                var eventArgs = CancelChangeEventArgs<int>.DoIf(this,
                                                                PasswordCharDelayChanging,
                                                                OnPasswordCharDelayChanging,
                                                                PasswordCharDelay,
                                                                value);
                if (eventArgs?.Cancel == true) return;

                try
                {
                    t.Interval = value;
                }
                catch (ArgumentException)
                {
                    throw new ArgumentOutOfRangeException(null,
                                                          value,
                                                          "Must be greater than zero.");
                }

                var oldValue = PasswordCharDelay;
                passwordCharDelay = value;
                ChangeEventArgs<int>.DoIf(this,
                                          PasswordCharDelayChanged,
                                          OnPasswordCharDelayChanged,
                                          oldValue,
                                          PasswordCharDelay);
            }
        }

        /// <summary>
        /// Computes the password character actually in effect, based on the
        /// values of <see cref="UseSystemPasswordChar"/> and
        /// <see cref="PasswordChar"/>.
        /// </summary>
        /// <returns>A <see cref="char"/>.</returns>
        [Browsable(false)]
        public char PasswordCharEffective
        {
            get
            {
                // ReSharper disable once ConvertPropertyToExpressionBody
                return UseSystemPasswordChar ? base.PasswordChar : passwordChar;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text in the
        /// <see cref="PasswordTextBox"/> control should appear as the default
        /// password character.
        /// </summary>
        [DefaultValue(DefaultUseSystemPasswordChar)]
        [Description("Indicates if the text in the PasswordTextBox control should appear as the default password character.")]
        public new bool UseSystemPasswordChar
        {
            get { return base.UseSystemPasswordChar; }

            set
            {
                if (base.UseSystemPasswordChar == value) return;

                var eventArgs = CancelChangeEventArgs<bool>.DoIf(this,
                                                                 UseSystemPasswordCharChanging,
                                                                 OnUseSystemPasswordCharChanging,
                                                                 UseSystemPasswordChar,
                                                                 value);
                if (eventArgs?.Cancel == true) return;

                var oldValue = UseSystemPasswordChar;
                base.UseSystemPasswordChar = value;
                ChangeEventArgs<bool>.DoIf(this,
                                           UseSystemPasswordCharChanged,
                                           OnUseSystemPasswordCharChanged,
                                           oldValue,
                                           UseSystemPasswordChar);
            }
        }

        /// <summary>
        /// Disposes unmanaged resources and, optionally, managed resources.
        /// </summary>
        /// <param name="disposing">If <c>true</c>, managed resources will be
        /// disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SetUpTimer(false);
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Raises the <see cref="PasswordCharChanged"/> event with the
        /// specified <see cref="ChangeEventArgs{T}"/> of <see cref="char"/>.
        /// </summary>
        /// <param name="e">A <see cref="ChangeEventArgs{T}"/> of
        /// <see cref="char"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="e"/> is
        /// <c>null</c>.</exception>>
        protected virtual void OnPasswordCharChanged(ChangeEventArgs<char> e)
        {
            if (ReferenceEquals(e, null))
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (!ReferenceEquals(PasswordCharChanged, null))
            {
                PasswordCharChanged(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="PasswordCharChanging"/> event with the
        /// specified <see cref="CancelChangeEventArgs{T}"/> of
        /// <see cref="char"/>.
        /// </summary>
        /// <param name="e">A <see cref="CancelChangeEventArgs{T}"/> of
        /// <see cref="char"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="e"/> is
        /// <c>null</c>.</exception>>
        protected virtual void OnPasswordCharChanging(CancelChangeEventArgs<char> e)
        {
            if (ReferenceEquals(e, null))
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (!ReferenceEquals(PasswordCharChanging, null))
            {
                PasswordCharChanging(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="PasswordCharDelayChanged"/> event with the
        /// specified <see cref="ChangeEventArgs{T}"/> of <see cref="int"/>.
        /// </summary>
        /// <param name="e">A <see cref="ChangeEventArgs{T}"/> of
        /// <see cref="int"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="e"/> is
        /// <c>null</c>.</exception>>
        protected virtual void OnPasswordCharDelayChanged(ChangeEventArgs<int> e)
        {
            if (ReferenceEquals(e, null))
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (!ReferenceEquals(PasswordCharDelayChanged, null))
            {
                PasswordCharDelayChanged(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="PasswordCharDelayChanging"/> event with the
        /// specified <see cref="CancelChangeEventArgs{T}"/> of
        /// <see cref="int"/>.
        /// </summary>
        /// <param name="e">A <see cref="CancelChangeEventArgs{T}"/> of
        /// <see cref="int"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="e"/> is
        /// <c>null</c>.</exception>>
        protected virtual void OnPasswordCharDelayChanging(CancelChangeEventArgs<int> e)
        {
            if (ReferenceEquals(e, null))
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (!ReferenceEquals(PasswordCharDelayChanging, null))
            {
                PasswordCharDelayChanging(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="TextBox.TextChanged"/> event with the
        /// specified <see cref="EventArgs"/>.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="e"/> is
        /// <c>null</c>.</exception>>
        protected override void OnTextChanged(EventArgs e)
        {
            if (ReferenceEquals(e, null))
            {
                throw new ArgumentNullException(nameof(e));
            }

            timer?.Stop();
            timer?.Start();

            var text = Text ?? "";
            if ((0 < text.Length)               &&
                (text.Length <= SelectionStart) &&
                ((textPrevious ?? "").Length < text.Length))
            {
                PaintUnobscured(text.Substring(text.Length - 1, 1),
                                               text.Length - 1);
            }
            textPrevious = text;

            base.OnTextChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="UseSystemPasswordCharChanged"/> event with the
        /// specified <see cref="ChangeEventArgs{T}"/> of <see cref="bool"/>.
        /// </summary>
        /// <param name="e">A <see cref="ChangeEventArgs{T}"/> of
        /// <see cref="bool"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="e"/> is
        /// <c>null</c>.</exception>>
        protected virtual void OnUseSystemPasswordCharChanged(ChangeEventArgs<bool> e)
        {
            if (ReferenceEquals(e, null))
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (!ReferenceEquals(UseSystemPasswordCharChanged, null))
            {
                UseSystemPasswordCharChanged(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="UseSystemPasswordCharChanging"/> event with
        /// the specified <see cref="CancelChangeEventArgs{T}"/> of
        /// <see cref="bool"/>.
        /// </summary>
        /// <param name="e">A <see cref="CancelChangeEventArgs{T}"/> of
        /// <see cref="bool"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="e"/> is
        /// <c>null</c>.</exception>>
        protected virtual void OnUseSystemPasswordCharChanging(CancelChangeEventArgs<bool> e)
        {
            if (ReferenceEquals(e, null))
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (!ReferenceEquals(UseSystemPasswordCharChanging, null))
            {
                UseSystemPasswordCharChanging(this, e);
            }
        }

        /// <summary>
        /// Paints the specified <paramref name="string"/> at the specified
        /// <paramref name="position"/>.
        /// </summary>
        /// <param name="string">A <see cref="string"/>.</param>
        /// <param name="position"></param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="position"/>
        /// is less than zero or not less than
        /// <see cref="PasswordTextBox.TextLength"/>.</exception>
        protected void PaintUnobscured(string @string, int position)
        {
            if ((position < 0) || (TextLength <= position))
            {
                throw new ArgumentOutOfRangeException(nameof(position),
                                                      position,
                                                      "Must be greater than or equal to 0 and less than the value of TextLength.");
            }

            // If we're not obscuring the text at all then there's no need
            // to paint unobscured text over it.
            if (PasswordCharEffective == '\0') return;

            if (string.IsNullOrEmpty(@string)) return;

            using (var graphics = NewGraphics(this))
            {
                if (IsFontSmoothingEnabled())
                {
                    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                }
                using (var foreBrush = NewSolidBrush(ForeColor))
                {
                    using (var backBrush = NewSolidBrush(BackColor))
                    {
                        var obscuredNewText = new string(PasswordCharEffective,
                                                         @string.Length);
                        var sizeOfObscuredNewText = graphics.MeasureString(obscuredNewText,
                                                                           Font);
                        var point = GetPositionFromCharIndex(position);
                        point.Offset(-(int)Math.Round(Math.Pow(Font.SizeInPoints, 0.15)),
                                      (int)Math.Round(Math.Pow(Font.SizeInPoints, 0.05)));

                        graphics.FillRectangle(backBrush,
                                               new RectangleF(point,
                                                              sizeOfObscuredNewText));
                        graphics.DrawString(@string, Font, foreBrush, point);
                    }
                }
            }
        }

        private void SetUpTimer(bool settingUp)
        {
            if (settingUp)
            {
                Debug.Assert(ReferenceEquals(timer, null));

                timer = NewTimer(false, PasswordCharDelay, this);
                timer.Elapsed += timer_Elapsed;
            }
            else
            {
                timer?.Dispose();
                timer = null;
            }
        }

        // ReSharper disable once InconsistentNaming
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Invalidate();
        }
    }
}