namespace UserInterface
{
    partial class MainView
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
            this.autentificareLabel = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelRol = new System.Windows.Forms.Label();
            this.radioButtonBibliotecar = new System.Windows.Forms.RadioButton();
            this.radioButtonAdministrator = new System.Windows.Forms.RadioButton();
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.groupBoxLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // autentificareLabel
            // 
            this.autentificareLabel.AutoSize = true;
            this.autentificareLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.autentificareLabel.Location = new System.Drawing.Point(210, 20);
            this.autentificareLabel.Name = "autentificareLabel";
            this.autentificareLabel.Size = new System.Drawing.Size(269, 54);
            this.autentificareLabel.TabIndex = 0;
            this.autentificareLabel.Text = "Autentificare";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonLogin.Location = new System.Drawing.Point(290, 290);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(120, 40);
            this.buttonLogin.TabIndex = 2;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(170, 30);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(200, 30);
            this.textBoxUsername.TabIndex = 1;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(170, 65);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(200, 30);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(30, 33);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(133, 23);
            this.labelUsername.TabIndex = 0;
            this.labelUsername.Text = "Nume utilizator:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(30, 68);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(61, 23);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Parola:";
            // 
            // labelRol
            // 
            this.labelRol.AutoSize = true;
            this.labelRol.Location = new System.Drawing.Point(30, 105);
            this.labelRol.Name = "labelRol";
            this.labelRol.Size = new System.Drawing.Size(38, 23);
            this.labelRol.TabIndex = 4;
            this.labelRol.Text = "Rol:";
            // 
            // radioButtonBibliotecar
            // 
            this.radioButtonBibliotecar.AutoSize = true;
            this.radioButtonBibliotecar.Location = new System.Drawing.Point(170, 103);
            this.radioButtonBibliotecar.Name = "radioButtonBibliotecar";
            this.radioButtonBibliotecar.Size = new System.Drawing.Size(111, 27);
            this.radioButtonBibliotecar.TabIndex = 5;
            this.radioButtonBibliotecar.TabStop = true;
            this.radioButtonBibliotecar.Text = "Bibliotecar";
            this.radioButtonBibliotecar.UseVisualStyleBackColor = true;
            // 
            // radioButtonAdministrator
            // 
            this.radioButtonAdministrator.AutoSize = true;
            this.radioButtonAdministrator.Location = new System.Drawing.Point(170, 136);
            this.radioButtonAdministrator.Name = "radioButtonAdministrator";
            this.radioButtonAdministrator.Size = new System.Drawing.Size(135, 27);
            this.radioButtonAdministrator.TabIndex = 6;
            this.radioButtonAdministrator.TabStop = true;
            this.radioButtonAdministrator.Text = "Administrator";
            this.radioButtonAdministrator.UseVisualStyleBackColor = true;
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.Controls.Add(this.labelUsername);
            this.groupBoxLogin.Controls.Add(this.textBoxUsername);
            this.groupBoxLogin.Controls.Add(this.labelPassword);
            this.groupBoxLogin.Controls.Add(this.textBoxPassword);
            this.groupBoxLogin.Controls.Add(this.labelRol);
            this.groupBoxLogin.Controls.Add(this.radioButtonBibliotecar);
            this.groupBoxLogin.Controls.Add(this.radioButtonAdministrator);
            this.groupBoxLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupBoxLogin.Location = new System.Drawing.Point(150, 90);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(400, 170);
            this.groupBoxLogin.TabIndex = 1;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "Date autentificare";
            // 
            // Biblioteca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 360);
            this.Controls.Add(this.autentificareLabel);
            this.Controls.Add(this.groupBoxLogin);
            this.Controls.Add(this.buttonLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Biblioteca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bibliotecă - Autentificare";
            this.groupBoxLogin.ResumeLayout(false);
            this.groupBoxLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Declare group box
        private System.Windows.Forms.GroupBox groupBoxLogin;


        #endregion
        private System.Windows.Forms.Label autentificareLabel;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelRol;
        private System.Windows.Forms.RadioButton radioButtonBibliotecar;
        private System.Windows.Forms.RadioButton radioButtonAdministrator;
    }
}

