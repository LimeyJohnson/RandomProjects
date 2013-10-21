namespace FantasyLeague
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
            this.lbl_URL = new System.Windows.Forms.Label();
            this.btn_Test = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtGW = new System.Windows.Forms.TextBox();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_URL
            // 
            this.lbl_URL.AutoSize = true;
            this.lbl_URL.Location = new System.Drawing.Point(12, 689);
            this.lbl_URL.Name = "lbl_URL";
            this.lbl_URL.Size = new System.Drawing.Size(0, 13);
            this.lbl_URL.TabIndex = 4;
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(12, 10);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(75, 23);
            this.btn_Test.TabIndex = 12;
            this.btn_Test.Text = "Algorithm";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "RunAllPlayers";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtGW
            // 
            this.txtGW.Location = new System.Drawing.Point(93, 41);
            this.txtGW.Name = "txtGW";
            this.txtGW.Size = new System.Drawing.Size(27, 20);
            this.txtGW.TabIndex = 14;
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Location = new System.Drawing.Point(297, 16);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(0, 13);
            this.lbl_Status.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 189);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.txtGW);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Test);
            this.Controls.Add(this.lbl_URL);
            this.Name = "Form1";
            this.Text = "Fantasy League Scraper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_URL;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtGW;
        private System.Windows.Forms.Label lbl_Status;
    }
}

