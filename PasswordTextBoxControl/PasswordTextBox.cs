using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace PasswordTextBoxControl
{
    [DefaultProperty("PasswordCharDelay")]
    [Description("Enables the user to enter password input, momentarily showing each character entered.")]
    public class PasswordTextBox : TextBox
    {
        /// <summary>
        /// The default value of <see cref="PasswordCharDelay"/>.
        /// </summary>
        public const int  DefaultPasswordCharDelay     = 1000;

        /// <summary>
        /// The default value of <see cref="UseSystemPasswordChar"/>.
        /// </summary>
        public const bool DefaultUseSystemPasswordChar = true;

        private int                 passwordCharDelay;
        private string              textPrevious;
        private System.Timers.Timer timer;

        /// <summary>
        /// Instantiates a new <see cref="PasswordTextBox"/>.
        /// </summary>
        public PasswordTextBox()
        {
            passwordCharDelay = DefaultPasswordCharDelay;
            UseSystemPasswordChar = true;
            SetUpTimer(true);
        }

        /// <summary>
        /// A value indicating whether this is a multiline <see cref="TextBox"/>
        /// control.
        /// </summary>
        /// <returns><c>false</c></returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Multiline {
            get { return false; }

            set { }
        }

        /// <summary>
        /// Gets or sets the character used to mask characters of a password in
        /// a <see cref="PasswordTextBox"/> control.
        /// </summary>
        [Description("Indicates the character to display for password input in a PasswordTextBox control.")]
        public new char PasswordChar
        {
            get { return base.PasswordChar; }

            set { base.PasswordChar = value; }
        }

        /// <summary>
        /// Gets or sets the time in milliseconds during which password input is
        /// legible before appearing as the password character.
        /// </summary>
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

                try
                {
                    t.Interval = value;
                }
                catch (ArgumentException e)
                {
                    throw new ArgumentOutOfRangeException("Must be greater than zero.",
                                                          e);
                }
                passwordCharDelay = value;
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

            set { base.UseSystemPasswordChar = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SetUpTimer(false);
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Computes the password character actually in effect, based on the
        /// values of <see cref="UseSystemPasswordChar"/> and
        /// <see cref="PasswordChar"/>.
        /// </summary>
        /// <returns>A <see cref="char"/>, or <c>null</c> if no password
        /// character is in effect.</returns>
        protected char? EffectivePasswordChar()
        {
            if (UseSystemPasswordChar) return '•';

            return (PasswordChar == '\0') ? (char?)null : PasswordChar;
        }

        protected override void OnTextChanged(EventArgs e)
        {
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
        /// Paints the specified <paramref name="text"/> at the specified
        /// <paramref name="position"/>.
        /// </summary>
        /// <param name="text">A <see cref="string"/>.</param>
        /// <param name="position"></param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="position"/>
        /// is less than zero or not less than
        /// <see cref="PasswordTextBox.TextLength"/>.</exception>
        protected void PaintUnobscured(string text, int position)
        {
            if ((position < 0) || (TextLength <= position))
            {
                throw new ArgumentOutOfRangeException(nameof(position),
                                                      position,
                                                      "Must be greater than or equal to 0 and less than the value of TextLength.");
            }

            // If we're not obscuring the text at all then there's no need
            // to paint unobscured text over it.
            var effectivePasswordChar = EffectivePasswordChar();
            if (!effectivePasswordChar.HasValue) return;

            if (string.IsNullOrEmpty(text)) return;

            using (var graphics = CreateGraphics())
            {
                using (var foreBrush = new SolidBrush(ForeColor))
                {
                    using (var backBrush = new SolidBrush(BackColor))
                    {
                        var obscuredNewText = new string(effectivePasswordChar.Value,
                                                         text.Length);
                        var sizeOfObscuredNewText = graphics.MeasureString(obscuredNewText,
                                                                           Font);
                        var point = GetPositionFromCharIndex(position);

                        graphics.FillRectangle(backBrush,
                                               new RectangleF(point, sizeOfObscuredNewText));
                        graphics.DrawString(text,
                                            Font,
                                            foreBrush,
                                            point);
                    }
                }
            }
        }

        private void SetUpTimer(bool settingUp)
        {
            if (settingUp)
            {
                Debug.Assert(ReferenceEquals(timer, null));

                timer = new System.Timers.Timer(PasswordCharDelay)
                {
                    AutoReset = false,
                    SynchronizingObject = this
                };
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