
namespace MessageClient
{
    partial class MainForm
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
            this.messageBox = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.listChat = new System.Windows.Forms.ListBox();
            this.connectionStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(63, 380);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(565, 23);
            this.messageBox.TabIndex = 0;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(663, 380);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // listChat
            // 
            this.listChat.FormattingEnabled = true;
            this.listChat.ItemHeight = 15;
            this.listChat.Location = new System.Drawing.Point(63, 71);
            this.listChat.Name = "listChat";
            this.listChat.Size = new System.Drawing.Size(675, 289);
            this.listChat.TabIndex = 2;
            // 
            // connectionStatus
            // 
            this.connectionStatus.AutoSize = true;
            this.connectionStatus.Location = new System.Drawing.Point(63, 22);
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Size = new System.Drawing.Size(99, 15);
            this.connectionStatus.TabIndex = 3;
            this.connectionStatus.Text = "connectionStatus";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.connectionStatus);
            this.Controls.Add(this.listChat);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.messageBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ListBox listChat;
        private System.Windows.Forms.Label connectionStatus;
    }
}

