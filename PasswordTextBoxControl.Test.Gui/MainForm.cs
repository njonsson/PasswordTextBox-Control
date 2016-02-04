using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PasswordTextBoxControl.Test.Gui
{
    public partial class MainForm : Form
    {
        private static bool SetMoveOrCopyString(DragEventArgs dragEventArgs)
        {
            if (!dragEventArgs.Data.GetDataPresent(DataFormats.StringFormat))
            {
                return false;
            }

            var result = false;

            if ((dragEventArgs.AllowedEffect & DragDropEffects.Move) ==
                DragDropEffects.Move)
            {
                dragEventArgs.Effect |= DragDropEffects.Move;
                result = true;
            }

            const int ctrl = 8;

            if (((dragEventArgs.AllowedEffect & DragDropEffects.Copy) ==
                 DragDropEffects.Copy) &&
               ((dragEventArgs.KeyState & ctrl) == ctrl))
            {
                dragEventArgs.Effect |= DragDropEffects.Copy;
                result = true;
            }

            return result;
        }

        private static bool SetScrollString(DragEventArgs dragEventArgs)
        {
            if (SetMoveOrCopyString(dragEventArgs))
            {
                dragEventArgs.Effect |= DragDropEffects.Scroll;
                return true;
            }

            return false;
        }

        public MainForm()
        {
            InitializeComponent();
            PasswordCharPropertyTextBox.Text = PasswordTextBox.PasswordChar.ToString();
            UseSystemPasswordCharPropertyCheckbox.Checked = PasswordTextBox.UseSystemPasswordChar;
            PasswordCharDelayPropertyTextBox.Text = PasswordTextBox.PasswordCharDelay.ToString();
        }

        // ReSharper disable once InconsistentNaming
        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            TextPropertyTextBox.TextChanged -= TextPropertyTextBox_TextChanged;
            try
            {
                TextPropertyTextBox.Text = PasswordTextBox.Text;
            }
            finally
            {
                TextPropertyTextBox.TextChanged += TextPropertyTextBox_TextChanged;
            }
        }

        // ReSharper disable once InconsistentNaming
        private void PasswordCharDelayPropertyTextBox_TextChanged(object sender,
                                                                  EventArgs e)
        {
            ErrorProvider.SetError(PasswordCharDelayPropertyTextBox, null);
        }

        // ReSharper disable once InconsistentNaming
        private void PasswordCharDelayPropertyTextBox_Validating(object sender,
                                                                 CancelEventArgs e)
        {
            int value;
            try
            {
                value = int.Parse(PasswordCharDelayPropertyTextBox.Text);
            }
            catch (FormatException)
            {
                e.Cancel = true;
                ErrorProvider.SetError(PasswordCharDelayPropertyTextBox,
                                       "Please enter a valid integer.");
                return;
            }

            if (value <= 0)
            {
                e.Cancel = true;
                ErrorProvider.SetError(PasswordCharDelayPropertyTextBox,
                                       "Please enter a positive integer.");
                return;
            }

            PasswordTextBox.PasswordCharDelay = value;
        }

        // ReSharper disable once InconsistentNaming
        private void PasswordCharPropertyTextBox_TextChanged(object sender, 
                                                             EventArgs e)
        {
            PasswordTextBox.PasswordChar = (PasswordCharPropertyTextBox.TextLength == 0) ?
                                           '\0'                                          :
                                           PasswordCharPropertyTextBox.Text[0];
        }

        // ReSharper disable once InconsistentNaming
        private void TextBoxControl_DragDrop(object sender, DragEventArgs e)
        {
            // SetEffect(e, DragDropEffects.Move, DragDropEffects.Copy);
            if (!SetMoveOrCopyString(e)) return;

            ((TextBox)sender).SelectedText = e.Data.GetData(DataFormats.StringFormat).ToString();
        }

        // ReSharper disable once InconsistentNaming
        private void TextBoxControl_DragOver(object sender, DragEventArgs e)
        {
            if (!SetScrollString(e)) return;

            ((Control)sender).Focus();
            var charIndex = ((TextBox)sender).GetCaretIndex(e.X, e.Y);
            ((TextBox)sender).Select(charIndex, 0);
            ((TextBox)sender).ScrollToCaret();
        }

        // ReSharper disable once InconsistentNaming
        private void TextBoxControl_QueryContinueDrag(object sender,
                                                      QueryContinueDragEventArgs e)
        {
            if (e.EscapePressed)
            {
                e.Action = DragAction.Cancel;
            }
            else
            {
                e.Action = DragAction.Continue;
            }
        }

        // ReSharper disable once InconsistentNaming
        private void TextPropertyTextBox_TextChanged(object sender,
                                                     EventArgs e)
        {
            PasswordTextBox.TextChanged -= PasswordTextBox_TextChanged;
            try
            {
                PasswordTextBox.Text = TextPropertyTextBox.Text;
            }
            finally
            {
                PasswordTextBox.TextChanged += PasswordTextBox_TextChanged;
            }
        }

        // ReSharper disable once InconsistentNaming
        private void UseSystemPasswordCharPropertyCheckbox_CheckedChanged(object sender,
                                                                          EventArgs e)
        {
            PasswordTextBox.UseSystemPasswordChar = PasswordCharPropertyTextBox.ReadOnly = UseSystemPasswordCharPropertyCheckbox.Checked;
        }
    }
}
