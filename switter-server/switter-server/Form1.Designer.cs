namespace switter_server
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
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_port = new System.Windows.Forms.TextBox();
            this.button_start = new System.Windows.Forms.Button();
            this.richtextbox_log = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Port:";
            // 
            // textbox_port
            // 
            this.textbox_port.Location = new System.Drawing.Point(120, 39);
            this.textbox_port.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textbox_port.Name = "textbox_port";
            this.textbox_port.Size = new System.Drawing.Size(132, 22);
            this.textbox_port.TabIndex = 1;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(274, 37);
            this.button_start.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(100, 28);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // richtextbox_log
            // 
            this.richtextbox_log.Location = new System.Drawing.Point(21, 89);
            this.richtextbox_log.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richtextbox_log.Name = "richtextbox_log";
            this.richtextbox_log.Size = new System.Drawing.Size(800, 368);
            this.richtextbox_log.TabIndex = 3;
            this.richtextbox_log.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 533);
            this.Controls.Add(this.richtextbox_log);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.textbox_port);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Server - Encrypted Messaging App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textbox_port;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.RichTextBox richtextbox_log;
    }
}

