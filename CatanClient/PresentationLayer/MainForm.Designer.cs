namespace Catan.Client.PresentationLayer
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbServerIPAddress = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNickname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.lblWaitingForClients = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Papyrus", 36F);
            this.label1.Location = new System.Drawing.Point(98, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server-IP:";
            // 
            // tbServerIPAddress
            // 
            this.tbServerIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbServerIPAddress.Font = new System.Drawing.Font("OCR A Extended", 32F);
            this.tbServerIPAddress.Location = new System.Drawing.Point(370, 156);
            this.tbServerIPAddress.Margin = new System.Windows.Forms.Padding(2);
            this.tbServerIPAddress.MaxLength = 14;
            this.tbServerIPAddress.Name = "tbServerIPAddress";
            this.tbServerIPAddress.Size = new System.Drawing.Size(324, 45);
            this.tbServerIPAddress.TabIndex = 1;
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPassword.Font = new System.Drawing.Font("OCR A Extended", 32F);
            this.tbPassword.Location = new System.Drawing.Point(370, 291);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.tbPassword.MaxLength = 10;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(324, 45);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Papyrus", 36F);
            this.label2.Location = new System.Drawing.Point(98, 277);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 76);
            this.label2.TabIndex = 2;
            this.label2.Text = "Passwort:";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConnect.Font = new System.Drawing.Font("Papyrus", 36F, System.Drawing.FontStyle.Bold);
            this.btnConnect.Location = new System.Drawing.Point(223, 488);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(341, 73);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Verbinden";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Agency FB", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(226, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(355, 116);
            this.label3.TabIndex = 5;
            this.label3.Text = "Catan FB4";
            // 
            // tbNickname
            // 
            this.tbNickname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNickname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbNickname.Font = new System.Drawing.Font("OCR A Extended", 32F);
            this.tbNickname.Location = new System.Drawing.Point(370, 354);
            this.tbNickname.Margin = new System.Windows.Forms.Padding(2);
            this.tbNickname.MaxLength = 24;
            this.tbNickname.Name = "tbNickname";
            this.tbNickname.Size = new System.Drawing.Size(324, 45);
            this.tbNickname.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Papyrus", 36F);
            this.label4.Location = new System.Drawing.Point(98, 340);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(248, 76);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nickname:";
            // 
            // pbLoading
            // 
            this.pbLoading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbLoading.Image = global::Catan.Client.Properties.Resources.loading_apple;
            this.pbLoading.Location = new System.Drawing.Point(329, 463);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(156, 120);
            this.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLoading.TabIndex = 7;
            this.pbLoading.TabStop = false;
            this.pbLoading.Visible = false;
            // 
            // lblWaitingForClients
            // 
            this.lblWaitingForClients.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblWaitingForClients.AutoSize = true;
            this.lblWaitingForClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaitingForClients.Location = new System.Drawing.Point(168, 586);
            this.lblWaitingForClients.Name = "lblWaitingForClients";
            this.lblWaitingForClients.Size = new System.Drawing.Size(487, 55);
            this.lblWaitingForClients.TabIndex = 8;
            this.lblWaitingForClients.Text = "Auf Clients warten ...";
            this.lblWaitingForClients.Visible = false;
            // 
            // tbEmail
            // 
            this.tbEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbEmail.Font = new System.Drawing.Font("OCR A Extended", 32F);
            this.tbEmail.Location = new System.Drawing.Point(370, 223);
            this.tbEmail.Margin = new System.Windows.Forms.Padding(2);
            this.tbEmail.MaxLength = 10;
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(324, 45);
            this.tbEmail.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Papyrus", 36F);
            this.label5.Location = new System.Drawing.Point(98, 210);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 76);
            this.label5.TabIndex = 10;
            this.label5.Text = "E-Mail:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(806, 648);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblWaitingForClients);
            this.Controls.Add(this.pbLoading);
            this.Controls.Add(this.tbNickname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbServerIPAddress);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbServerIPAddress;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNickname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.Label lblWaitingForClients;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.Label label5;
    }
}

