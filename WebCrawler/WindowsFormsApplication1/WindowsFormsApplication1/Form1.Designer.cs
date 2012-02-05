namespace WindowsFormsApplication1
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
            this.txtSearchString = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnNavForward = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnNavBack = new System.Windows.Forms.Button();
            this.btnRecordCommit = new System.Windows.Forms.Button();
            this.btnRecordBack = new System.Windows.Forms.Button();
            this.btnCancelCommit = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnDeleteToday = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSearchString
            // 
            this.txtSearchString.Location = new System.Drawing.Point(932, 16);
            this.txtSearchString.Name = "txtSearchString";
            this.txtSearchString.Size = new System.Drawing.Size(190, 20);
            this.txtSearchString.TabIndex = 1;
            this.txtSearchString.Text = "Obama";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1125, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(50, 40);
            this.btnSearch.TabIndex = 32;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 51);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1163, 603);
            this.webBrowser1.TabIndex = 37;
            this.webBrowser1.Url = new System.Uri("http://www.google.com", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // btnNavForward
            // 
            this.btnNavForward.Location = new System.Drawing.Point(59, 3);
            this.btnNavForward.Name = "btnNavForward";
            this.btnNavForward.Size = new System.Drawing.Size(50, 40);
            this.btnNavForward.TabIndex = 31;
            this.btnNavForward.Text = "--->";
            this.btnNavForward.UseVisualStyleBackColor = true;
            this.btnNavForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(820, 5);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(50, 40);
            this.btnGo.TabIndex = 34;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // textBoxURL
            // 
            this.textBoxURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxURL.Location = new System.Drawing.Point(115, 12);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(699, 22);
            this.textBoxURL.TabIndex = 28;
            this.textBoxURL.WordWrap = false;
            this.textBoxURL.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(876, 5);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(50, 40);
            this.btnAbout.TabIndex = 35;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnNavBack
            // 
            this.btnNavBack.Location = new System.Drawing.Point(3, 3);
            this.btnNavBack.Name = "btnNavBack";
            this.btnNavBack.Size = new System.Drawing.Size(50, 40);
            this.btnNavBack.TabIndex = 30;
            this.btnNavBack.Text = "<---";
            this.btnNavBack.UseVisualStyleBackColor = true;
            this.btnNavBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRecordCommit
            // 
            this.btnRecordCommit.Location = new System.Drawing.Point(907, 660);
            this.btnRecordCommit.Name = "btnRecordCommit";
            this.btnRecordCommit.Size = new System.Drawing.Size(168, 40);
            this.btnRecordCommit.TabIndex = 41;
            this.btnRecordCommit.Text = "Commit Record:\r\nX of Y";
            this.btnRecordCommit.UseVisualStyleBackColor = true;
            this.btnRecordCommit.Click += new System.EventHandler(this.btnRecordCommit_Click);
            // 
            // btnRecordBack
            // 
            this.btnRecordBack.Location = new System.Drawing.Point(851, 660);
            this.btnRecordBack.Name = "btnRecordBack";
            this.btnRecordBack.Size = new System.Drawing.Size(50, 40);
            this.btnRecordBack.TabIndex = 42;
            this.btnRecordBack.Text = "<---";
            this.btnRecordBack.UseVisualStyleBackColor = true;
            this.btnRecordBack.Click += new System.EventHandler(this.btnRecordBack_Click);
            // 
            // btnCancelCommit
            // 
            this.btnCancelCommit.Location = new System.Drawing.Point(1082, 661);
            this.btnCancelCommit.Name = "btnCancelCommit";
            this.btnCancelCommit.Size = new System.Drawing.Size(75, 39);
            this.btnCancelCommit.TabIndex = 43;
            this.btnCancelCommit.Text = "Don\'t Commit";
            this.btnCancelCommit.UseVisualStyleBackColor = true;
            this.btnCancelCommit.Click += new System.EventHandler(this.btnCancelCommit_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblInfo.Location = new System.Drawing.Point(13, 661);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 13);
            this.lblInfo.TabIndex = 44;
            // 
            // btnDeleteToday
            // 
            this.btnDeleteToday.Location = new System.Drawing.Point(1054, 768);
            this.btnDeleteToday.Name = "btnDeleteToday";
            this.btnDeleteToday.Size = new System.Drawing.Size(118, 22);
            this.btnDeleteToday.TabIndex = 45;
            this.btnDeleteToday.Text = "DeleteTodaysEntries";
            this.btnDeleteToday.UseVisualStyleBackColor = true;
            this.btnDeleteToday.Click += new System.EventHandler(this.btnDeleteToday_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1184, 802);
            this.Controls.Add(this.btnDeleteToday);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnCancelCommit);
            this.Controls.Add(this.btnRecordBack);
            this.Controls.Add(this.btnRecordCommit);
            this.Controls.Add(this.btnNavBack);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnNavForward);
            this.Controls.Add(this.txtSearchString);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchString;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnNavForward;
        private System.Windows.Forms.Button btnGo;
        public System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnNavBack;
        private System.Windows.Forms.Button btnRecordCommit;
        private System.Windows.Forms.Button btnRecordBack;
        private System.Windows.Forms.Button btnCancelCommit;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnDeleteToday;
    }
}

