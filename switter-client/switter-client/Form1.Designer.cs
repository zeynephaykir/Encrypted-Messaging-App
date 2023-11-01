namespace switter_client
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.label_username = new System.Windows.Forms.Label();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.textBox_finduser = new System.Windows.Forms.TextBox();
            this.button_connectuser = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_disconnectfromuser = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(150, 64);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(116, 22);
            this.textBox_ip.TabIndex = 3;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(150, 105);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(116, 22);
            this.textBox_port.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(108, 202);
            this.button_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(93, 27);
            this.button_connect.TabIndex = 6;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.HideSelection = false;
            this.logs.Location = new System.Drawing.Point(423, 50);
            this.logs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(483, 442);
            this.logs.TabIndex = 8;
            this.logs.Text = "";
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(304, 405);
            this.button_send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(98, 32);
            this.button_send.TabIndex = 10;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 413);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Message:";
            // 
            // textBox_message
            // 
            this.textBox_message.Enabled = false;
            this.textBox_message.Location = new System.Drawing.Point(94, 410);
            this.textBox_message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(200, 22);
            this.textBox_message.TabIndex = 9;
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(150, 148);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(116, 22);
            this.textBox_username.TabIndex = 5;
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Location = new System.Drawing.Point(66, 148);
            this.label_username.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(73, 16);
            this.label_username.TabIndex = 13;
            this.label_username.Text = "Username:";
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(230, 201);
            this.button_disconnect.Margin = new System.Windows.Forms.Padding(4);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(100, 28);
            this.button_disconnect.TabIndex = 14;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // textBox_finduser
            // 
            this.textBox_finduser.Enabled = false;
            this.textBox_finduser.Location = new System.Drawing.Point(184, 286);
            this.textBox_finduser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_finduser.Name = "textBox_finduser";
            this.textBox_finduser.Size = new System.Drawing.Size(129, 22);
            this.textBox_finduser.TabIndex = 16;
            // 
            // button_connectuser
            // 
            this.button_connectuser.Enabled = false;
            this.button_connectuser.Location = new System.Drawing.Point(51, 325);
            this.button_connectuser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_connectuser.Name = "button_connectuser";
            this.button_connectuser.Size = new System.Drawing.Size(144, 39);
            this.button_connectuser.TabIndex = 17;
            this.button_connectuser.Text = "Connect To User";
            this.button_connectuser.UseVisualStyleBackColor = true;
            this.button_connectuser.Click += new System.EventHandler(this.button_connectuser_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 289);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 16);
            this.label5.TabIndex = 25;
            this.label5.Text = "Username:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(420, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 30;
            this.label7.Text = "Feed";
            // 
            // button_disconnectfromuser
            // 
            this.button_disconnectfromuser.Enabled = false;
            this.button_disconnectfromuser.Location = new System.Drawing.Point(232, 325);
            this.button_disconnectfromuser.Name = "button_disconnectfromuser";
            this.button_disconnectfromuser.Size = new System.Drawing.Size(170, 39);
            this.button_disconnectfromuser.TabIndex = 31;
            this.button_disconnectfromuser.Text = "Disconnect From User";
            this.button_disconnectfromuser.UseVisualStyleBackColor = true;
            this.button_disconnectfromuser.Click += new System.EventHandler(this.button_disconnectfromuser_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(278, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "(Empty for localhost)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 537);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_disconnectfromuser);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_connectuser);
            this.Controls.Add(this.textBox_finduser);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Encrypted Messaging App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.TextBox textBox_finduser;
        private System.Windows.Forms.Button button_connectuser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_disconnectfromuser;
        private System.Windows.Forms.Label label4;
    }
}

