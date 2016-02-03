using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PasswordTextBoxControl.Test.Gui
{
    public partial class MainForm : Form
    {
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
            if (PasswordCharPropertyTextBox.TextLength == 0)
            {
                PasswordTextBox.PasswordChar = '\0';
            }
            else
            {
                PasswordTextBox.PasswordChar = PasswordCharPropertyTextBox.Text[0];
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
