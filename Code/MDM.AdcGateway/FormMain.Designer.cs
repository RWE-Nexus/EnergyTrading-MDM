namespace MDM.AdcGateway
{
    partial class FormMain
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
            this.ProcessButton = new System.Windows.Forms.Button();
            this.CounterpartyRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(236, 24);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(75, 23);
            this.ProcessButton.TabIndex = 0;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // CounterpartyRadioButton
            // 
            this.CounterpartyRadioButton.AutoSize = true;
            this.CounterpartyRadioButton.Location = new System.Drawing.Point(30, 24);
            this.CounterpartyRadioButton.Name = "CounterpartyRadioButton";
            this.CounterpartyRadioButton.Size = new System.Drawing.Size(85, 17);
            this.CounterpartyRadioButton.TabIndex = 1;
            this.CounterpartyRadioButton.TabStop = true;
            this.CounterpartyRadioButton.Text = "Counterparty";
            this.CounterpartyRadioButton.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 252);
            this.Controls.Add(this.CounterpartyRadioButton);
            this.Controls.Add(this.ProcessButton);
            this.Name = "FormMain";
            this.Text = "MDM ADC Gateway";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.RadioButton CounterpartyRadioButton;
    }
}

