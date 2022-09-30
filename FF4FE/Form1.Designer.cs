namespace FF4FE
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.itemsPanel = new System.Windows.Forms.Panel();
            this.currentViewingTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.AutoSize = true;
            this.buttonsPanel.Location = new System.Drawing.Point(12, 12);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(209, 38);
            this.buttonsPanel.TabIndex = 2;
            // 
            // itemsPanel
            // 
            this.itemsPanel.AutoSize = true;
            this.itemsPanel.Location = new System.Drawing.Point(241, 66);
            this.itemsPanel.Name = "itemsPanel";
            this.itemsPanel.Size = new System.Drawing.Size(400, 473);
            this.itemsPanel.TabIndex = 3;
            // 
            // currentViewingTextbox
            // 
            this.currentViewingTextbox.Font = new System.Drawing.Font("Courier New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.currentViewingTextbox.Location = new System.Drawing.Point(241, 12);
            this.currentViewingTextbox.Name = "currentViewingTextbox";
            this.currentViewingTextbox.Size = new System.Drawing.Size(400, 38);
            this.currentViewingTextbox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 556);
            this.Controls.Add(this.currentViewingTextbox);
            this.Controls.Add(this.itemsPanel);
            this.Controls.Add(this.buttonsPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Panel buttonsPanel;
        private Panel itemsPanel;
        private TextBox currentViewingTextbox;
    }
}