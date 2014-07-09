namespace RabbitProducer
{
    partial class MessageInputView
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
            this.SendARabbit = new System.Windows.Forms.Button();
            this.FarmName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SendARabbit
            // 
            this.SendARabbit.Location = new System.Drawing.Point(35, 70);
            this.SendARabbit.Name = "SendARabbit";
            this.SendARabbit.Size = new System.Drawing.Size(208, 23);
            this.SendARabbit.TabIndex = 0;
            this.SendARabbit.Text = "Send A Rabbit";
            this.SendARabbit.UseVisualStyleBackColor = true;
            this.SendARabbit.Click += new System.EventHandler(this.SendARabbitClick);
            // 
            // FarmName
            // 
            this.FarmName.Location = new System.Drawing.Point(35, 29);
            this.FarmName.Name = "FarmName";
            this.FarmName.Size = new System.Drawing.Size(208, 22);
            this.FarmName.TabIndex = 1;
            // 
            // MessageInputView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 338);
            this.Controls.Add(this.FarmName);
            this.Controls.Add(this.SendARabbit);
            this.Name = "MessageInputView";
            this.Text = "Rabbot Producer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendARabbit;
        private System.Windows.Forms.TextBox FarmName;
    }
}

