namespace UnityWcfExtensionsTestClient
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
            this.dataRichTextBox = new System.Windows.Forms.RichTextBox();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.serviceHostBaseButton = new System.Windows.Forms.Button();
            this.callContextButton = new System.Windows.Forms.Button();
            this.operationContextButton = new System.Windows.Forms.Button();
            this.instanceContextButton = new System.Windows.Forms.Button();
            this.bindingGroupBox = new System.Windows.Forms.GroupBox();
            this.basicHttpRadioButton = new System.Windows.Forms.RadioButton();
            this.netTcpRadioButton = new System.Windows.Forms.RadioButton();
            this.buttonPanel.SuspendLayout();
            this.bindingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataRichTextBox
            // 
            this.dataRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRichTextBox.Location = new System.Drawing.Point(0, 81);
            this.dataRichTextBox.Name = "dataRichTextBox";
            this.dataRichTextBox.Size = new System.Drawing.Size(466, 562);
            this.dataRichTextBox.TabIndex = 1;
            this.dataRichTextBox.Text = "";
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.bindingGroupBox);
            this.buttonPanel.Controls.Add(this.serviceHostBaseButton);
            this.buttonPanel.Controls.Add(this.callContextButton);
            this.buttonPanel.Controls.Add(this.operationContextButton);
            this.buttonPanel.Controls.Add(this.instanceContextButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(466, 81);
            this.buttonPanel.TabIndex = 2;
            // 
            // serviceHostBaseButton
            // 
            this.serviceHostBaseButton.Location = new System.Drawing.Point(259, 13);
            this.serviceHostBaseButton.Name = "serviceHostBaseButton";
            this.serviceHostBaseButton.Size = new System.Drawing.Size(75, 23);
            this.serviceHostBaseButton.TabIndex = 7;
            this.serviceHostBaseButton.Text = "Host";
            this.serviceHostBaseButton.UseVisualStyleBackColor = true;
            this.serviceHostBaseButton.Click += new System.EventHandler(this.serviceHostBaseButton_Click);
            // 
            // callContextButton
            // 
            this.callContextButton.Location = new System.Drawing.Point(177, 12);
            this.callContextButton.Name = "callContextButton";
            this.callContextButton.Size = new System.Drawing.Size(75, 23);
            this.callContextButton.TabIndex = 6;
            this.callContextButton.Text = "Call";
            this.callContextButton.UseVisualStyleBackColor = true;
            this.callContextButton.Click += new System.EventHandler(this.callContextButton_Click);
            // 
            // operationContextButton
            // 
            this.operationContextButton.Location = new System.Drawing.Point(12, 12);
            this.operationContextButton.Name = "operationContextButton";
            this.operationContextButton.Size = new System.Drawing.Size(75, 23);
            this.operationContextButton.TabIndex = 5;
            this.operationContextButton.Text = "Operation";
            this.operationContextButton.UseVisualStyleBackColor = true;
            this.operationContextButton.Click += new System.EventHandler(this.operationContextButton_Click);
            // 
            // instanceContextButton
            // 
            this.instanceContextButton.Location = new System.Drawing.Point(95, 12);
            this.instanceContextButton.Name = "instanceContextButton";
            this.instanceContextButton.Size = new System.Drawing.Size(75, 23);
            this.instanceContextButton.TabIndex = 4;
            this.instanceContextButton.Text = "Instance";
            this.instanceContextButton.UseVisualStyleBackColor = true;
            this.instanceContextButton.Click += new System.EventHandler(this.instanceContextButton_Click);
            // 
            // bindingGroupBox
            // 
            this.bindingGroupBox.Controls.Add(this.netTcpRadioButton);
            this.bindingGroupBox.Controls.Add(this.basicHttpRadioButton);
            this.bindingGroupBox.Location = new System.Drawing.Point(341, 12);
            this.bindingGroupBox.Name = "bindingGroupBox";
            this.bindingGroupBox.Size = new System.Drawing.Size(113, 58);
            this.bindingGroupBox.TabIndex = 8;
            this.bindingGroupBox.TabStop = false;
            this.bindingGroupBox.Text = "Binding";
            // 
            // basicHttpRadioButton
            // 
            this.basicHttpRadioButton.AutoSize = true;
            this.basicHttpRadioButton.Checked = true;
            this.basicHttpRadioButton.Location = new System.Drawing.Point(7, 11);
            this.basicHttpRadioButton.Name = "basicHttpRadioButton";
            this.basicHttpRadioButton.Size = new System.Drawing.Size(70, 17);
            this.basicHttpRadioButton.TabIndex = 0;
            this.basicHttpRadioButton.TabStop = true;
            this.basicHttpRadioButton.Text = "basicHttp";
            this.basicHttpRadioButton.UseVisualStyleBackColor = true;
            // 
            // netTcpRadioButton
            // 
            this.netTcpRadioButton.AutoSize = true;
            this.netTcpRadioButton.Location = new System.Drawing.Point(7, 35);
            this.netTcpRadioButton.Name = "netTcpRadioButton";
            this.netTcpRadioButton.Size = new System.Drawing.Size(59, 17);
            this.netTcpRadioButton.TabIndex = 1;
            this.netTcpRadioButton.Text = "netTcp";
            this.netTcpRadioButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 643);
            this.Controls.Add(this.dataRichTextBox);
            this.Controls.Add(this.buttonPanel);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.buttonPanel.ResumeLayout(false);
            this.bindingGroupBox.ResumeLayout(false);
            this.bindingGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox dataRichTextBox;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button callContextButton;
        private System.Windows.Forms.Button operationContextButton;
        private System.Windows.Forms.Button instanceContextButton;
        private System.Windows.Forms.Button serviceHostBaseButton;
        private System.Windows.Forms.GroupBox bindingGroupBox;
        private System.Windows.Forms.RadioButton netTcpRadioButton;
        private System.Windows.Forms.RadioButton basicHttpRadioButton;
    }
}

