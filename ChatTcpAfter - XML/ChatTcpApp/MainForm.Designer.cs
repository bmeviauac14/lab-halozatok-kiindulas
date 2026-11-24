namespace ChatTcpApp
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
            this.bSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMyName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHistory = new System.Windows.Forms.TextBox();
            this.tbMessageToSend = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPartner = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bSend
            // 
            this.bSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSend.Location = new System.Drawing.Point(263, 318);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 0;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "My Name";
            // 
            // tbMyName
            // 
            this.tbMyName.Location = new System.Drawing.Point(12, 29);
            this.tbMyName.Name = "tbMyName";
            this.tbMyName.Size = new System.Drawing.Size(209, 20);
            this.tbMyName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "History";
            // 
            // tbHistory
            // 
            this.tbHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHistory.Location = new System.Drawing.Point(12, 68);
            this.tbHistory.Multiline = true;
            this.tbHistory.Name = "tbHistory";
            this.tbHistory.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbHistory.Size = new System.Drawing.Size(322, 142);
            this.tbHistory.TabIndex = 4;
            // 
            // tbMessageToSend
            // 
            this.tbMessageToSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessageToSend.Location = new System.Drawing.Point(12, 268);
            this.tbMessageToSend.Multiline = true;
            this.tbMessageToSend.Name = "tbMessageToSend";
            this.tbMessageToSend.Size = new System.Drawing.Size(326, 44);
            this.tbMessageToSend.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Message to send";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Partner address";
            // 
            // tbPartner
            // 
            this.tbPartner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbPartner.Location = new System.Drawing.Point(12, 229);
            this.tbPartner.Name = "tbPartner";
            this.tbPartner.Size = new System.Drawing.Size(213, 20);
            this.tbPartner.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 351);
            this.Controls.Add(this.tbPartner);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbMessageToSend);
            this.Controls.Add(this.tbHistory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMyName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bSend);
            this.MinimumSize = new System.Drawing.Size(256, 300);
            this.Name = "MainForm";
            this.Text = "Chat Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHistory;
        private System.Windows.Forms.TextBox tbMessageToSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPartner;
    }
}

