namespace PasswordTextBoxControl.Test.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PasswordTextBoxLabel = new System.Windows.Forms.Label();
            this.PasswordTextBox = new PasswordTextBoxControl.PasswordTextBox();
            this.UseSystemPasswordCharPropertyCheckbox = new System.Windows.Forms.CheckBox();
            this.PasswordCharPropertyTextBox = new System.Windows.Forms.TextBox();
            this.TextPropertyLabel = new System.Windows.Forms.Label();
            this.TextPropertyTextBox = new System.Windows.Forms.TextBox();
            this.PasswordCharPropertyLabel = new System.Windows.Forms.Label();
            this.PasswordCharDelayPropertyTextBox = new System.Windows.Forms.TextBox();
            this.PasswordCharDelayPropertyLabel = new System.Windows.Forms.Label();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.PasswordTextBoxPanel = new System.Windows.Forms.Panel();
            this.PropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.PasswordCharDelayPropertyPanel = new System.Windows.Forms.Panel();
            this.PasswordCharPropertyPanel = new System.Windows.Forms.Panel();
            this.UseSystemPasswordCharPropertyPanel = new System.Windows.Forms.Panel();
            this.TextPropertyPanel = new System.Windows.Forms.Panel();
            this.PropertiesPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.PasswordTextBoxPanel.SuspendLayout();
            this.PropertiesGroupBox.SuspendLayout();
            this.PasswordCharDelayPropertyPanel.SuspendLayout();
            this.PasswordCharPropertyPanel.SuspendLayout();
            this.UseSystemPasswordCharPropertyPanel.SuspendLayout();
            this.TextPropertyPanel.SuspendLayout();
            this.PropertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PasswordTextBoxLabel
            // 
            this.PasswordTextBoxLabel.AutoEllipsis = true;
            this.PasswordTextBoxLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PasswordTextBoxLabel.Location = new System.Drawing.Point(18, 18);
            this.PasswordTextBoxLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasswordTextBoxLabel.Name = "PasswordTextBoxLabel";
            this.PasswordTextBoxLabel.Size = new System.Drawing.Size(223, 18);
            this.PasswordTextBoxLabel.TabIndex = 0;
            this.PasswordTextBoxLabel.Text = "PasswordTextBox:";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.AllowDrop = true;
            this.PasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PasswordTextBox.Location = new System.Drawing.Point(18, 36);
            this.PasswordTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(223, 23);
            this.PasswordTextBox.TabIndex = 1;
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            this.PasswordTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBoxControl_DragDrop);
            this.PasswordTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.TextBoxControl_DragOver);
            this.PasswordTextBox.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.TextBoxControl_QueryContinueDrag);
            // 
            // UseSystemPasswordCharPropertyCheckbox
            // 
            this.UseSystemPasswordCharPropertyCheckbox.AutoEllipsis = true;
            this.UseSystemPasswordCharPropertyCheckbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.UseSystemPasswordCharPropertyCheckbox.Location = new System.Drawing.Point(18, 9);
            this.UseSystemPasswordCharPropertyCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UseSystemPasswordCharPropertyCheckbox.Name = "UseSystemPasswordCharPropertyCheckbox";
            this.UseSystemPasswordCharPropertyCheckbox.Size = new System.Drawing.Size(179, 20);
            this.UseSystemPasswordCharPropertyCheckbox.TabIndex = 0;
            this.UseSystemPasswordCharPropertyCheckbox.Text = "UseSystemPasswordChar";
            this.UseSystemPasswordCharPropertyCheckbox.UseVisualStyleBackColor = true;
            this.UseSystemPasswordCharPropertyCheckbox.CheckedChanged += new System.EventHandler(this.UseSystemPasswordCharPropertyCheckbox_CheckedChanged);
            // 
            // PasswordCharPropertyTextBox
            // 
            this.PasswordCharPropertyTextBox.AllowDrop = true;
            this.PasswordCharPropertyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PasswordCharPropertyTextBox.Location = new System.Drawing.Point(18, 27);
            this.PasswordCharPropertyTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PasswordCharPropertyTextBox.MaxLength = 1;
            this.PasswordCharPropertyTextBox.Name = "PasswordCharPropertyTextBox";
            this.PasswordCharPropertyTextBox.Size = new System.Drawing.Size(179, 23);
            this.PasswordCharPropertyTextBox.TabIndex = 1;
            this.PasswordCharPropertyTextBox.TextChanged += new System.EventHandler(this.PasswordCharPropertyTextBox_TextChanged);
            this.PasswordCharPropertyTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBoxControl_DragDrop);
            this.PasswordCharPropertyTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.TextBoxControl_DragOver);
            this.PasswordCharPropertyTextBox.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.TextBoxControl_QueryContinueDrag);
            // 
            // TextPropertyLabel
            // 
            this.TextPropertyLabel.AutoEllipsis = true;
            this.TextPropertyLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TextPropertyLabel.Location = new System.Drawing.Point(18, 5);
            this.TextPropertyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextPropertyLabel.Name = "TextPropertyLabel";
            this.TextPropertyLabel.Size = new System.Drawing.Size(179, 18);
            this.TextPropertyLabel.TabIndex = 0;
            this.TextPropertyLabel.Text = "Text:";
            // 
            // TextPropertyTextBox
            // 
            this.TextPropertyTextBox.AllowDrop = true;
            this.TextPropertyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextPropertyTextBox.Location = new System.Drawing.Point(18, 23);
            this.TextPropertyTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextPropertyTextBox.Name = "TextPropertyTextBox";
            this.TextPropertyTextBox.Size = new System.Drawing.Size(179, 23);
            this.TextPropertyTextBox.TabIndex = 1;
            this.TextPropertyTextBox.TextChanged += new System.EventHandler(this.TextPropertyTextBox_TextChanged);
            this.TextPropertyTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBoxControl_DragDrop);
            this.TextPropertyTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.TextBoxControl_DragOver);
            this.TextPropertyTextBox.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.TextBoxControl_QueryContinueDrag);
            // 
            // PasswordCharPropertyLabel
            // 
            this.PasswordCharPropertyLabel.AutoEllipsis = true;
            this.PasswordCharPropertyLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PasswordCharPropertyLabel.Location = new System.Drawing.Point(18, 9);
            this.PasswordCharPropertyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasswordCharPropertyLabel.Name = "PasswordCharPropertyLabel";
            this.PasswordCharPropertyLabel.Size = new System.Drawing.Size(179, 18);
            this.PasswordCharPropertyLabel.TabIndex = 0;
            this.PasswordCharPropertyLabel.Text = "PasswordChar:";
            // 
            // PasswordCharDelayPropertyTextBox
            // 
            this.PasswordCharDelayPropertyTextBox.AllowDrop = true;
            this.PasswordCharDelayPropertyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PasswordCharDelayPropertyTextBox.Location = new System.Drawing.Point(18, 27);
            this.PasswordCharDelayPropertyTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PasswordCharDelayPropertyTextBox.Name = "PasswordCharDelayPropertyTextBox";
            this.PasswordCharDelayPropertyTextBox.Size = new System.Drawing.Size(179, 23);
            this.PasswordCharDelayPropertyTextBox.TabIndex = 1;
            this.PasswordCharDelayPropertyTextBox.TextChanged += new System.EventHandler(this.PasswordCharDelayPropertyTextBox_TextChanged);
            this.PasswordCharDelayPropertyTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBoxControl_DragDrop);
            this.PasswordCharDelayPropertyTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.TextBoxControl_DragOver);
            this.PasswordCharDelayPropertyTextBox.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.TextBoxControl_QueryContinueDrag);
            this.PasswordCharDelayPropertyTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.PasswordCharDelayPropertyTextBox_Validating);
            // 
            // PasswordCharDelayPropertyLabel
            // 
            this.PasswordCharDelayPropertyLabel.AutoEllipsis = true;
            this.PasswordCharDelayPropertyLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PasswordCharDelayPropertyLabel.Location = new System.Drawing.Point(18, 9);
            this.PasswordCharDelayPropertyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasswordCharDelayPropertyLabel.Name = "PasswordCharDelayPropertyLabel";
            this.PasswordCharDelayPropertyLabel.Size = new System.Drawing.Size(179, 18);
            this.PasswordCharDelayPropertyLabel.TabIndex = 0;
            this.PasswordCharDelayPropertyLabel.Text = "PasswordCharDelay:";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // PasswordTextBoxPanel
            // 
            this.PasswordTextBoxPanel.Controls.Add(this.PasswordTextBox);
            this.PasswordTextBoxPanel.Controls.Add(this.PasswordTextBoxLabel);
            this.PasswordTextBoxPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PasswordTextBoxPanel.Location = new System.Drawing.Point(0, 0);
            this.PasswordTextBoxPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PasswordTextBoxPanel.Name = "PasswordTextBoxPanel";
            this.PasswordTextBoxPanel.Padding = new System.Windows.Forms.Padding(18, 18, 18, 9);
            this.PasswordTextBoxPanel.Size = new System.Drawing.Size(259, 61);
            this.PasswordTextBoxPanel.TabIndex = 0;
            // 
            // PropertiesGroupBox
            // 
            this.PropertiesGroupBox.Controls.Add(this.PasswordCharDelayPropertyPanel);
            this.PropertiesGroupBox.Controls.Add(this.PasswordCharPropertyPanel);
            this.PropertiesGroupBox.Controls.Add(this.UseSystemPasswordCharPropertyPanel);
            this.PropertiesGroupBox.Controls.Add(this.TextPropertyPanel);
            this.PropertiesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertiesGroupBox.Location = new System.Drawing.Point(18, 14);
            this.PropertiesGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PropertiesGroupBox.Name = "PropertiesGroupBox";
            this.PropertiesGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PropertiesGroupBox.Size = new System.Drawing.Size(223, 243);
            this.PropertiesGroupBox.TabIndex = 0;
            this.PropertiesGroupBox.TabStop = false;
            this.PropertiesGroupBox.Text = "Properties";
            // 
            // PasswordCharDelayPropertyPanel
            // 
            this.PasswordCharDelayPropertyPanel.Controls.Add(this.PasswordCharDelayPropertyTextBox);
            this.PasswordCharDelayPropertyPanel.Controls.Add(this.PasswordCharDelayPropertyLabel);
            this.PasswordCharDelayPropertyPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PasswordCharDelayPropertyPanel.Location = new System.Drawing.Point(4, 170);
            this.PasswordCharDelayPropertyPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PasswordCharDelayPropertyPanel.Name = "PasswordCharDelayPropertyPanel";
            this.PasswordCharDelayPropertyPanel.Padding = new System.Windows.Forms.Padding(18, 9, 18, 18);
            this.PasswordCharDelayPropertyPanel.Size = new System.Drawing.Size(215, 68);
            this.PasswordCharDelayPropertyPanel.TabIndex = 3;
            // 
            // PasswordCharPropertyPanel
            // 
            this.PasswordCharPropertyPanel.Controls.Add(this.PasswordCharPropertyTextBox);
            this.PasswordCharPropertyPanel.Controls.Add(this.PasswordCharPropertyLabel);
            this.PasswordCharPropertyPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PasswordCharPropertyPanel.Location = new System.Drawing.Point(4, 109);
            this.PasswordCharPropertyPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PasswordCharPropertyPanel.Name = "PasswordCharPropertyPanel";
            this.PasswordCharPropertyPanel.Padding = new System.Windows.Forms.Padding(18, 9, 18, 9);
            this.PasswordCharPropertyPanel.Size = new System.Drawing.Size(215, 61);
            this.PasswordCharPropertyPanel.TabIndex = 2;
            // 
            // UseSystemPasswordCharPropertyPanel
            // 
            this.UseSystemPasswordCharPropertyPanel.Controls.Add(this.UseSystemPasswordCharPropertyCheckbox);
            this.UseSystemPasswordCharPropertyPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.UseSystemPasswordCharPropertyPanel.Location = new System.Drawing.Point(4, 76);
            this.UseSystemPasswordCharPropertyPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UseSystemPasswordCharPropertyPanel.Name = "UseSystemPasswordCharPropertyPanel";
            this.UseSystemPasswordCharPropertyPanel.Padding = new System.Windows.Forms.Padding(18, 9, 18, 9);
            this.UseSystemPasswordCharPropertyPanel.Size = new System.Drawing.Size(215, 33);
            this.UseSystemPasswordCharPropertyPanel.TabIndex = 1;
            // 
            // TextPropertyPanel
            // 
            this.TextPropertyPanel.Controls.Add(this.TextPropertyTextBox);
            this.TextPropertyPanel.Controls.Add(this.TextPropertyLabel);
            this.TextPropertyPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TextPropertyPanel.Location = new System.Drawing.Point(4, 19);
            this.TextPropertyPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextPropertyPanel.Name = "TextPropertyPanel";
            this.TextPropertyPanel.Padding = new System.Windows.Forms.Padding(18, 5, 18, 9);
            this.TextPropertyPanel.Size = new System.Drawing.Size(215, 57);
            this.TextPropertyPanel.TabIndex = 0;
            // 
            // PropertiesPanel
            // 
            this.PropertiesPanel.Controls.Add(this.PropertiesGroupBox);
            this.PropertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertiesPanel.Location = new System.Drawing.Point(0, 61);
            this.PropertiesPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PropertiesPanel.Name = "PropertiesPanel";
            this.PropertiesPanel.Padding = new System.Windows.Forms.Padding(18, 14, 18, 18);
            this.PropertiesPanel.Size = new System.Drawing.Size(259, 275);
            this.PropertiesPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 336);
            this.Controls.Add(this.PropertiesPanel);
            this.Controls.Add(this.PasswordTextBoxPanel);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(16, 374);
            this.Name = "MainForm";
            this.Text = "PasswordTextBox Control GUI Test";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.PasswordTextBoxPanel.ResumeLayout(false);
            this.PasswordTextBoxPanel.PerformLayout();
            this.PropertiesGroupBox.ResumeLayout(false);
            this.PasswordCharDelayPropertyPanel.ResumeLayout(false);
            this.PasswordCharDelayPropertyPanel.PerformLayout();
            this.PasswordCharPropertyPanel.ResumeLayout(false);
            this.PasswordCharPropertyPanel.PerformLayout();
            this.UseSystemPasswordCharPropertyPanel.ResumeLayout(false);
            this.TextPropertyPanel.ResumeLayout(false);
            this.TextPropertyPanel.PerformLayout();
            this.PropertiesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public PasswordTextBoxControl.PasswordTextBox PasswordTextBox;
        public System.Windows.Forms.Label PasswordTextBoxLabel;
        public System.Windows.Forms.CheckBox UseSystemPasswordCharPropertyCheckbox;
        public System.Windows.Forms.TextBox PasswordCharPropertyTextBox;
        public System.Windows.Forms.Label TextPropertyLabel;
        public System.Windows.Forms.TextBox TextPropertyTextBox;
        private System.Windows.Forms.Label PasswordCharPropertyLabel;
        public System.Windows.Forms.TextBox PasswordCharDelayPropertyTextBox;
        private System.Windows.Forms.Label PasswordCharDelayPropertyLabel;
        public System.Windows.Forms.ErrorProvider ErrorProvider;
        public System.Windows.Forms.Panel PropertiesPanel;
        public System.Windows.Forms.GroupBox PropertiesGroupBox;
        public System.Windows.Forms.Panel PasswordCharDelayPropertyPanel;
        public System.Windows.Forms.Panel PasswordCharPropertyPanel;
        public System.Windows.Forms.Panel UseSystemPasswordCharPropertyPanel;
        public System.Windows.Forms.Panel TextPropertyPanel;
        public System.Windows.Forms.Panel PasswordTextBoxPanel;
    }
}

