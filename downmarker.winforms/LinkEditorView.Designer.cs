namespace DownMarker.WinForms
{
    partial class LinkEditorView
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
         this.linkDescriptionTextBox = new System.Windows.Forms.TextBox();
         this.descriptionLabel = new System.Windows.Forms.Label();
         this.linkLabel = new System.Windows.Forms.Label();
         this.linkTargetTextBox = new System.Windows.Forms.TextBox();
         this.okButton = new System.Windows.Forms.Button();
         this.cancelButton = new System.Windows.Forms.Button();
         this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         this.browseButton = new System.Windows.Forms.Button();
         this.checkBoxCreateTarget = new System.Windows.Forms.CheckBox();
         ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
         this.SuspendLayout();
         // 
         // linkDescriptionTextBox
         // 
         this.linkDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.errorProvider.SetIconAlignment(this.linkDescriptionTextBox, System.Windows.Forms.ErrorIconAlignment.TopLeft);
         this.linkDescriptionTextBox.Location = new System.Drawing.Point(20, 96);
         this.linkDescriptionTextBox.Multiline = true;
         this.linkDescriptionTextBox.Name = "linkDescriptionTextBox";
         this.linkDescriptionTextBox.Size = new System.Drawing.Size(361, 88);
         this.linkDescriptionTextBox.TabIndex = 3;
         this.linkDescriptionTextBox.TextChanged += new System.EventHandler(this.HandleLinkDescriptionTextBoxTextChanged);
         // 
         // descriptionLabel
         // 
         this.descriptionLabel.AutoSize = true;
         this.descriptionLabel.Location = new System.Drawing.Point(17, 80);
         this.descriptionLabel.Name = "descriptionLabel";
         this.descriptionLabel.Size = new System.Drawing.Size(86, 13);
         this.descriptionLabel.TabIndex = 1;
         this.descriptionLabel.Text = "Link Description:";
         // 
         // linkLabel
         // 
         this.linkLabel.AutoSize = true;
         this.linkLabel.Location = new System.Drawing.Point(17, 7);
         this.linkLabel.Name = "linkLabel";
         this.linkLabel.Size = new System.Drawing.Size(64, 13);
         this.linkLabel.TabIndex = 2;
         this.linkLabel.Text = "Link Target:";
         // 
         // linkTargetTextBox
         // 
         this.linkTargetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.errorProvider.SetIconAlignment(this.linkTargetTextBox, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
         this.linkTargetTextBox.Location = new System.Drawing.Point(20, 23);
         this.linkTargetTextBox.Name = "linkTargetTextBox";
         this.linkTargetTextBox.Size = new System.Drawing.Size(288, 20);
         this.linkTargetTextBox.TabIndex = 0;
         this.linkTargetTextBox.Text = "http://";
         this.linkTargetTextBox.TextChanged += new System.EventHandler(this.HandleLinkTargetTextBoxTextChanged);
         // 
         // okButton
         // 
         this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.okButton.Location = new System.Drawing.Point(225, 190);
         this.okButton.Name = "okButton";
         this.okButton.Size = new System.Drawing.Size(75, 23);
         this.okButton.TabIndex = 4;
         this.okButton.Text = "&OK";
         this.okButton.UseVisualStyleBackColor = true;
         // 
         // cancelButton
         // 
         this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.cancelButton.Location = new System.Drawing.Point(306, 190);
         this.cancelButton.Name = "cancelButton";
         this.cancelButton.Size = new System.Drawing.Size(75, 23);
         this.cancelButton.TabIndex = 5;
         this.cancelButton.Text = "&Cancel";
         this.cancelButton.UseVisualStyleBackColor = true;
         // 
         // errorProvider
         // 
         this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
         this.errorProvider.ContainerControl = this;
         // 
         // browseButton
         // 
         this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.browseButton.Location = new System.Drawing.Point(314, 23);
         this.browseButton.Name = "browseButton";
         this.browseButton.Size = new System.Drawing.Size(67, 20);
         this.browseButton.TabIndex = 1;
         this.browseButton.Text = "Browse...";
         this.browseButton.UseVisualStyleBackColor = true;
         this.browseButton.Click += new System.EventHandler(this.HandleBrowseButtonClick);
         // 
         // checkBoxCreateTarget
         // 
         this.checkBoxCreateTarget.AutoSize = true;
         this.checkBoxCreateTarget.Location = new System.Drawing.Point(20, 50);
         this.checkBoxCreateTarget.Name = "checkBoxCreateTarget";
         this.checkBoxCreateTarget.Size = new System.Drawing.Size(114, 17);
         this.checkBoxCreateTarget.TabIndex = 2;
         this.checkBoxCreateTarget.Text = "Create Link Target";
         this.checkBoxCreateTarget.UseVisualStyleBackColor = true;
         this.checkBoxCreateTarget.CheckedChanged += new System.EventHandler(this.HandleCreateTargetCheckedChanged);
         // 
         // LinkEditorView
         // 
         this.AcceptButton = this.okButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.cancelButton;
         this.ClientSize = new System.Drawing.Size(404, 225);
         this.Controls.Add(this.checkBoxCreateTarget);
         this.Controls.Add(this.browseButton);
         this.Controls.Add(this.cancelButton);
         this.Controls.Add(this.okButton);
         this.Controls.Add(this.linkTargetTextBox);
         this.Controls.Add(this.linkLabel);
         this.Controls.Add(this.descriptionLabel);
         this.Controls.Add(this.linkDescriptionTextBox);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
         this.Name = "LinkEditorView";
         this.ShowInTaskbar = false;
         this.Text = "Link Editor";
         ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox linkDescriptionTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label linkLabel;
        private System.Windows.Forms.TextBox linkTargetTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.CheckBox checkBoxCreateTarget;
    }
}