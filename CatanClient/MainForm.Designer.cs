namespace CatanClient
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Papyrus", 36F);
            this.label1.Location = new System.Drawing.Point(142, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 114);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // tbServerIPAddress
            // 
            this.tbServerIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbServerIPAddress.Font = new System.Drawing.Font("OCR A Extended", 32F);
            this.tbServerIPAddress.Location = new System.Drawing.Point(550, 270);
            this.tbServerIPAddress.Name = "tbServerIPAddress";
            this.tbServerIPAddress.Size = new System.Drawing.Size(486, 67);
            this.tbServerIPAddress.TabIndex = 1;
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPassword.Font = new System.Drawing.Font("OCR A Extended", 32F);
            this.tbPassword.Location = new System.Drawing.Point(550, 413);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(486, 67);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Papyrus", 36F);
            this.label2.Location = new System.Drawing.Point(142, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(347, 114);
            this.label2.TabIndex = 2;
            this.label2.Text = "Passwort:";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConnect.Font = new System.Drawing.Font("Papyrus", 36F, System.Drawing.FontStyle.Bold);
            this.btnConnect.Location = new System.Drawing.Point(332, 535);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(511, 99);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Verbinden";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Agency FB", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(318, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(525, 170);
            this.label3.TabIndex = 5;
            this.label3.Text = "Catan FB4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(1209, 643);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbServerIPAddress);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
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
    }
}

