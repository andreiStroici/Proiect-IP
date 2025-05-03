namespace UserInterface
{
    partial class Biblioteca
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
            this.SuspendLayout();
            // 
            // autentificareLabel
            // 
            this.autentificareLabel.AllowDrop = true;
            this.autentificareLabel.AutoSize = true;
            this.autentificareLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autentificareLabel.Location = new System.Drawing.Point(274, 69);
            this.autentificareLabel.Name = "autentificareLabel";
            this.autentificareLabel.Size = new System.Drawing.Size(198, 38);
            this.autentificareLabel.TabIndex = 0;
            this.autentificareLabel.Text = "Autentificare";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(302, 302);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(120, 33);
            this.buttonLogin.TabIndex = 1;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(368, 159);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(165, 22);
            this.textBoxUsername.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(368, 199);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(165, 22);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(209, 159);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(122, 20);
            this.labelUsername.TabIndex = 4;
            this.labelUsername.Text = "Nume utilizator";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.Location = new System.Drawing.Point(235, 199);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(57, 20);
            this.labelPassword.TabIndex = 5;
            this.labelPassword.Text = "Parola";
            // 
            // labelRol
            // 
            this.labelRol.AutoSize = true;
            this.labelRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRol.Location = new System.Drawing.Point(244, 242);
            this.labelRol.Name = "labelRol";
            this.labelRol.Size = new System.Drawing.Size(34, 20);
            this.labelRol.TabIndex = 6;
            this.labelRol.Text = "Rol";
            // 
            // radioButtonBibliotecar
            // 
            this.radioButtonBibliotecar.AutoSize = true;
            this.radioButtonBibliotecar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonBibliotecar.Location = new System.Drawing.Point(348, 242);
            this.radioButtonBibliotecar.Name = "radioButtonBibliotecar";
            this.radioButtonBibliotecar.Size = new System.Drawing.Size(98, 22);
            this.radioButtonBibliotecar.TabIndex = 7;
            this.radioButtonBibliotecar.TabStop = true;
            this.radioButtonBibliotecar.Text = "Bibliotecar";
            this.radioButtonBibliotecar.UseVisualStyleBackColor = true;
            // 
            // radioButtonAdministrator
            // 
            this.radioButtonAdministrator.AutoSize = true;
            this.radioButtonAdministrator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAdministrator.Location = new System.Drawing.Point(461, 242);
            this.radioButtonAdministrator.Name = "radioButtonAdministrator";
            this.radioButtonAdministrator.Size = new System.Drawing.Size(116, 22);
            this.radioButtonAdministrator.TabIndex = 8;
            this.radioButtonAdministrator.TabStop = true;
            this.radioButtonAdministrator.Text = "Administrator";
            this.radioButtonAdministrator.UseVisualStyleBackColor = true;
            // 
            // Biblioteca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 384);
            this.Controls.Add(this.radioButtonAdministrator);
            this.Controls.Add(this.radioButtonBibliotecar);
            this.Controls.Add(this.labelRol);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.autentificareLabel);
            this.Name = "Biblioteca";
            this.Text = "Bibliotecă";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void InitializeComponentsBibliotecar()
        {
            this.Controls.Clear();
            this.labelAngajatInregistrare = new System.Windows.Forms.Label();
            this.labelInregistrareTelefon = new System.Windows.Forms.Label();
            this.labelInregistrareEmail = new System.Windows.Forms.Label();
            this.labelInregistrareAdresa = new System.Windows.Forms.Label();
            this.labelAngajatParola = new System.Windows.Forms.Label();
            this.labelAngajatUsername = new System.Windows.Forms.Label();
            this.buttonAngajatRegister = new System.Windows.Forms.Button();
            this.buttonBibliotecarLogin = new System.Windows.Forms.Button();
            this.labelAutentificareTelefon = new System.Windows.Forms.Label();
            this.labelAutentificare = new System.Windows.Forms.Label();
            this.textBoxAngajatUsername = new System.Windows.Forms.TextBox();
            this.textBoxAngajatParola = new System.Windows.Forms.TextBox();
            this.textBoxInregistrareAdresa = new System.Windows.Forms.TextBox();
            this.textBoxInregistrareTelefon = new System.Windows.Forms.TextBox();
            this.textBoxInregistrareEmail = new System.Windows.Forms.TextBox();
            this.textBoxAutentificareTelefon = new System.Windows.Forms.TextBox();
            this.buttonRetur = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.labelReturIDImprumut = new System.Windows.Forms.Label();
            this.labelRetur = new System.Windows.Forms.Label();
            this.buttonImprumut = new System.Windows.Forms.Button();
            this.textBoxImprumutPerioada = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelImprumutPerioada = new System.Windows.Forms.Label();
            this.labelImprumutAutor = new System.Windows.Forms.Label();
            this.labelImprumutTitlu = new System.Windows.Forms.Label();
            this.labelImprumut = new System.Windows.Forms.Label();
            this.groupBoxAbonat = new System.Windows.Forms.GroupBox();
            this.groupBoxServicii = new System.Windows.Forms.GroupBox();
            this.groupBoxAbonat.SuspendLayout();
            this.groupBoxServicii.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInregistrare
            // 
            this.labelAngajatInregistrare.AutoSize = true;
            this.labelAngajatInregistrare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAngajatInregistrare.Location = new System.Drawing.Point(122, 291);
            this.labelAngajatInregistrare.Name = "labelInregistrare";
            this.labelAngajatInregistrare.Size = new System.Drawing.Size(220, 25);
            this.labelAngajatInregistrare.TabIndex = 1;
            this.labelAngajatInregistrare.Text = "Înregistrare client nou";
            // 
            // labelInregistrareTelefon
            // 
            this.labelInregistrareTelefon.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelInregistrareTelefon.AutoSize = true;
            this.labelInregistrareTelefon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInregistrareTelefon.Location = new System.Drawing.Point(57, 442);
            this.labelInregistrareTelefon.Name = "labelInregistrareTelefon";
            this.labelInregistrareTelefon.Size = new System.Drawing.Size(137, 20);
            this.labelInregistrareTelefon.TabIndex = 3;
            this.labelInregistrareTelefon.Text = "Număr de telefon";
            // 
            // labelInregistrareEmail
            // 
            this.labelInregistrareEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelInregistrareEmail.AutoSize = true;
            this.labelInregistrareEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInregistrareEmail.Location = new System.Drawing.Point(57, 477);
            this.labelInregistrareEmail.Name = "labelInregistrareEmail";
            this.labelInregistrareEmail.Size = new System.Drawing.Size(51, 20);
            this.labelInregistrareEmail.TabIndex = 4;
            this.labelInregistrareEmail.Text = "Email";
            // 
            // labelInregistrareAdresa
            // 
            this.labelInregistrareAdresa.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelInregistrareAdresa.AutoSize = true;
            this.labelInregistrareAdresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInregistrareAdresa.Location = new System.Drawing.Point(57, 412);
            this.labelInregistrareAdresa.Name = "labelInregistrareAdresa";
            this.labelInregistrareAdresa.Size = new System.Drawing.Size(62, 20);
            this.labelInregistrareAdresa.TabIndex = 5;
            this.labelInregistrareAdresa.Text = "Adresă";
            // 
            // labelInregistrarePrenume
            // 
            this.labelAngajatParola.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAngajatParola.AutoSize = true;
            this.labelAngajatParola.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAngajatParola.Location = new System.Drawing.Point(57, 379);
            this.labelAngajatParola.Name = "labelInregistrarePrenume";
            this.labelAngajatParola.Size = new System.Drawing.Size(76, 20);
            this.labelAngajatParola.TabIndex = 7;
            this.labelAngajatParola.Text = "Prenume";
            // 
            // labelInregistrareNume
            // 
            this.labelAngajatUsername.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAngajatUsername.AutoSize = true;
            this.labelAngajatUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAngajatUsername.Location = new System.Drawing.Point(57, 346);
            this.labelAngajatUsername.Name = "labelInregistrareNume";
            this.labelAngajatUsername.Size = new System.Drawing.Size(53, 20);
            this.labelAngajatUsername.TabIndex = 8;
            this.labelAngajatUsername.Text = "Nume";
            // 
            // buttonRegister
            // 
            this.buttonAngajatRegister.Location = new System.Drawing.Point(162, 514);
            this.buttonAngajatRegister.Name = "buttonRegister";
            this.buttonAngajatRegister.Size = new System.Drawing.Size(101, 38);
            this.buttonAngajatRegister.TabIndex = 16;
            this.buttonAngajatRegister.Text = "Register";
            this.buttonAngajatRegister.UseVisualStyleBackColor = true;
            // 
            // buttonBibliotecarLogin
            // 
            this.buttonBibliotecarLogin.Location = new System.Drawing.Point(150, 175);
            this.buttonBibliotecarLogin.Name = "buttonBibliotecarLogin";
            this.buttonBibliotecarLogin.Size = new System.Drawing.Size(101, 33);
            this.buttonBibliotecarLogin.TabIndex = 15;
            this.buttonBibliotecarLogin.Text = "Login";
            this.buttonBibliotecarLogin.UseVisualStyleBackColor = true;
            // 
            // labelAutentificareTelefon
            // 
            this.labelAutentificareTelefon.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAutentificareTelefon.AutoSize = true;
            this.labelAutentificareTelefon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAutentificareTelefon.Location = new System.Drawing.Point(43, 132);
            this.labelAutentificareTelefon.Name = "labelAutentificareTelefon";
            this.labelAutentificareTelefon.Size = new System.Drawing.Size(137, 20);
            this.labelAutentificareTelefon.TabIndex = 2;
            this.labelAutentificareTelefon.Text = "Număr de telefon";
            // 
            // labelAutentificare
            // 
            this.labelAutentificare.AutoSize = true;
            this.labelAutentificare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAutentificare.Location = new System.Drawing.Point(145, 78);
            this.labelAutentificare.Name = "labelAutentificare";
            this.labelAutentificare.Size = new System.Drawing.Size(133, 25);
            this.labelAutentificare.TabIndex = 0;
            this.labelAutentificare.Text = "Autentificare";
            // 
            // textBoxInregistrareNume
            // 
            this.textBoxAngajatUsername.Location = new System.Drawing.Point(225, 342);
            this.textBoxAngajatUsername.Name = "textBoxInregistrareNume";
            this.textBoxAngajatUsername.Size = new System.Drawing.Size(165, 22);
            this.textBoxAngajatUsername.TabIndex = 10;
            // 
            // textBoxInregistrarePrenume
            // 
            this.textBoxAngajatParola.Location = new System.Drawing.Point(225, 372);
            this.textBoxAngajatParola.Name = "textBoxInregistrarePrenume";
            this.textBoxAngajatParola.Size = new System.Drawing.Size(165, 22);
            this.textBoxAngajatParola.TabIndex = 11;
            // 
            // textBoxInregistrareAdresa
            // 
            this.textBoxInregistrareAdresa.Location = new System.Drawing.Point(225, 410);
            this.textBoxInregistrareAdresa.Name = "textBoxInregistrareAdresa";
            this.textBoxInregistrareAdresa.Size = new System.Drawing.Size(165, 22);
            this.textBoxInregistrareAdresa.TabIndex = 12;
            // 
            // textBoxInregistrareTelefon
            // 
            this.textBoxInregistrareTelefon.Location = new System.Drawing.Point(225, 440);
            this.textBoxInregistrareTelefon.Name = "textBoxInregistrareTelefon";
            this.textBoxInregistrareTelefon.Size = new System.Drawing.Size(165, 22);
            this.textBoxInregistrareTelefon.TabIndex = 13;
            // 
            // textBoxInregistrareEmail
            // 
            this.textBoxInregistrareEmail.Location = new System.Drawing.Point(225, 475);
            this.textBoxInregistrareEmail.Name = "textBoxInregistrareEmail";
            this.textBoxInregistrareEmail.Size = new System.Drawing.Size(165, 22);
            this.textBoxInregistrareEmail.TabIndex = 14;
            // 
            // textBoxAutentificareTelefon
            // 
            this.textBoxAutentificareTelefon.Location = new System.Drawing.Point(213, 128);
            this.textBoxAutentificareTelefon.Name = "textBoxAutentificareTelefon";
            this.textBoxAutentificareTelefon.Size = new System.Drawing.Size(165, 22);
            this.textBoxAutentificareTelefon.TabIndex = 9;
            // 
            // buttonRetur
            // 
            this.buttonRetur.Location = new System.Drawing.Point(178, 494);
            this.buttonRetur.Name = "buttonRetur";
            this.buttonRetur.Size = new System.Drawing.Size(124, 37);
            this.buttonRetur.TabIndex = 52;
            this.buttonRetur.Text = "Validare";
            this.buttonRetur.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(201, 426);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(215, 22);
            this.textBox3.TabIndex = 51;
            // 
            // labelReturIDImprumut
            // 
            this.labelReturIDImprumut.AutoSize = true;
            this.labelReturIDImprumut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReturIDImprumut.Location = new System.Drawing.Point(44, 428);
            this.labelReturIDImprumut.Name = "labelReturIDImprumut";
            this.labelReturIDImprumut.Size = new System.Drawing.Size(72, 20);
            this.labelReturIDImprumut.TabIndex = 50;
            this.labelReturIDImprumut.Text = "ID Carte";
            // 
            // labelRetur
            // 
            this.labelRetur.AutoSize = true;
            this.labelRetur.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRetur.Location = new System.Drawing.Point(134, 357);
            this.labelRetur.Name = "labelRetur";
            this.labelRetur.Size = new System.Drawing.Size(160, 25);
            this.labelRetur.TabIndex = 49;
            this.labelRetur.Text = "Returnare carte";
            // 
            // buttonImprumut
            // 
            this.buttonImprumut.Location = new System.Drawing.Point(178, 273);
            this.buttonImprumut.Name = "buttonImprumut";
            this.buttonImprumut.Size = new System.Drawing.Size(124, 34);
            this.buttonImprumut.TabIndex = 48;
            this.buttonImprumut.Text = "Validare";
            // 
            // textBoxImprumutPerioada
            // 
            this.textBoxImprumutPerioada.Location = new System.Drawing.Point(286, 213);
            this.textBoxImprumutPerioada.Name = "textBoxImprumutPerioada";
            this.textBoxImprumutPerioada.Size = new System.Drawing.Size(130, 22);
            this.textBoxImprumutPerioada.TabIndex = 47;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(179, 164);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(237, 22);
            this.textBox2.TabIndex = 45;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(178, 119);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(238, 22);
            this.textBox1.TabIndex = 44;
            // 
            // labelImprumutPerioada
            // 
            this.labelImprumutPerioada.AutoSize = true;
            this.labelImprumutPerioada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImprumutPerioada.Location = new System.Drawing.Point(44, 217);
            this.labelImprumutPerioada.Name = "labelImprumutPerioada";
            this.labelImprumutPerioada.Size = new System.Drawing.Size(217, 20);
            this.labelImprumutPerioada.TabIndex = 46;
            this.labelImprumutPerioada.Text = "Perioadă de împrumut (zile)";
            // 
            // labelImprumutAutor
            // 
            this.labelImprumutAutor.AutoSize = true;
            this.labelImprumutAutor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImprumutAutor.Location = new System.Drawing.Point(44, 165);
            this.labelImprumutAutor.Name = "labelImprumutAutor";
            this.labelImprumutAutor.Size = new System.Drawing.Size(49, 20);
            this.labelImprumutAutor.TabIndex = 43;
            this.labelImprumutAutor.Text = "Autor";
            // 
            // labelImprumutTitlu
            // 
            this.labelImprumutTitlu.AutoSize = true;
            this.labelImprumutTitlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImprumutTitlu.Location = new System.Drawing.Point(44, 121);
            this.labelImprumutTitlu.Name = "labelImprumutTitlu";
            this.labelImprumutTitlu.Size = new System.Drawing.Size(41, 20);
            this.labelImprumutTitlu.TabIndex = 42;
            this.labelImprumutTitlu.Text = "Titlu";
            // 
            // labelImprumut
            // 
            this.labelImprumut.AutoSize = true;
            this.labelImprumut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImprumut.Location = new System.Drawing.Point(134, 60);
            this.labelImprumut.Name = "labelImprumut";
            this.labelImprumut.Size = new System.Drawing.Size(186, 25);
            this.labelImprumut.TabIndex = 41;
            this.labelImprumut.Text = "Împrumutare carte";
            // 
            // groupBoxAbonat
            // 
            this.groupBoxAbonat.Controls.Add(this.groupBoxServicii);
            this.groupBoxAbonat.Controls.Add(this.labelAutentificare);
            this.groupBoxAbonat.Controls.Add(this.textBoxAngajatParola);
            this.groupBoxAbonat.Controls.Add(this.textBoxAngajatUsername);
            this.groupBoxAbonat.Controls.Add(this.textBoxInregistrareAdresa);
            this.groupBoxAbonat.Controls.Add(this.labelAutentificareTelefon);
            this.groupBoxAbonat.Controls.Add(this.textBoxInregistrareTelefon);
            this.groupBoxAbonat.Controls.Add(this.buttonBibliotecarLogin);
            this.groupBoxAbonat.Controls.Add(this.textBoxInregistrareEmail);
            this.groupBoxAbonat.Controls.Add(this.buttonAngajatRegister);
            this.groupBoxAbonat.Controls.Add(this.textBoxAutentificareTelefon);
            this.groupBoxAbonat.Controls.Add(this.labelAngajatUsername);
            this.groupBoxAbonat.Controls.Add(this.labelAngajatParola);
            this.groupBoxAbonat.Controls.Add(this.labelInregistrareAdresa);
            this.groupBoxAbonat.Controls.Add(this.labelInregistrareEmail);
            this.groupBoxAbonat.Controls.Add(this.labelAngajatInregistrare);
            this.groupBoxAbonat.Controls.Add(this.labelInregistrareTelefon);
            this.groupBoxAbonat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAbonat.Location = new System.Drawing.Point(0, 0);
            this.groupBoxAbonat.Name = "groupBoxAbonat";
            this.groupBoxAbonat.Size = new System.Drawing.Size(940, 579);
            this.groupBoxAbonat.TabIndex = 53;
            this.groupBoxAbonat.TabStop = false;
            // 
            // groupBoxServicii
            // 
            this.groupBoxServicii.Controls.Add(this.labelImprumut);
            this.groupBoxServicii.Controls.Add(this.labelImprumutTitlu);
            this.groupBoxServicii.Controls.Add(this.buttonRetur);
            this.groupBoxServicii.Controls.Add(this.labelImprumutAutor);
            this.groupBoxServicii.Controls.Add(this.textBox3);
            this.groupBoxServicii.Controls.Add(this.labelImprumutPerioada);
            this.groupBoxServicii.Controls.Add(this.labelReturIDImprumut);
            this.groupBoxServicii.Controls.Add(this.textBox1);
            this.groupBoxServicii.Controls.Add(this.labelRetur);
            this.groupBoxServicii.Controls.Add(this.textBox2);
            this.groupBoxServicii.Controls.Add(this.buttonImprumut);
            this.groupBoxServicii.Controls.Add(this.textBoxImprumutPerioada);
            this.groupBoxServicii.Enabled = false;
            this.groupBoxServicii.Location = new System.Drawing.Point(441, 0);
            this.groupBoxServicii.Name = "groupBoxServicii";
            this.groupBoxServicii.Size = new System.Drawing.Size(496, 576);
            this.groupBoxServicii.TabIndex = 54;
            this.groupBoxServicii.TabStop = false;
            // 
            // Biblioteca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 579);
            this.Controls.Add(this.groupBoxAbonat);
            this.Name = "Bibliotecar";
            this.Text = "Bibliotecar";
            this.groupBoxAbonat.ResumeLayout(false);
            this.groupBoxAbonat.PerformLayout();
            this.groupBoxServicii.ResumeLayout(false);
            this.groupBoxServicii.PerformLayout();
            this.ResumeLayout(false);
        }

        public void InitializeComponentsAdministrator()
        {
            this.Controls.Clear();
            this.labelAngajatInregistrare = new System.Windows.Forms.Label();
            this.labelAngajatParola = new System.Windows.Forms.Label();
            this.labelAngajatUsername = new System.Windows.Forms.Label();
            this.buttonAngajatRegister = new System.Windows.Forms.Button();
            this.textBoxAngajatUsername = new System.Windows.Forms.TextBox();
            this.textBoxAngajatParola = new System.Windows.Forms.TextBox();
            this.groupBoxAbonatii = new System.Windows.Forms.GroupBox();
            this.tabControlAdministrator = new System.Windows.Forms.TabControl();
            this.tabPageAdminAngajati = new System.Windows.Forms.TabPage();
            this.radioButtonAngajatBibliotecar = new System.Windows.Forms.RadioButton();
            this.radioButtonAngajatAdministrator = new System.Windows.Forms.RadioButton();
            this.labelAngajatRol = new System.Windows.Forms.Label();
            this.tabPageAdminCarti = new System.Windows.Forms.TabPage();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.textBoxIDCarte = new System.Windows.Forms.TextBox();
            this.labelUpdateIDCarte = new System.Windows.Forms.Label();
            this.labelUpdateCarte = new System.Windows.Forms.Label();
            this.buttonDeleteCarte = new System.Windows.Forms.Button();
            this.textBoxDeleteIDCarte = new System.Windows.Forms.TextBox();
            this.labelDeleteIDCarte = new System.Windows.Forms.Label();
            this.labelDelete = new System.Windows.Forms.Label();
            this.labelAddCarteGen = new System.Windows.Forms.Label();
            this.textBoxAddCarteGen = new System.Windows.Forms.TextBox();
            this.labelAddCarteEditura = new System.Windows.Forms.Label();
            this.labelAddCarteAutor = new System.Windows.Forms.Label();
            this.textBoxAddCarteEditura = new System.Windows.Forms.TextBox();
            this.textBoxAddCarteAutor = new System.Windows.Forms.TextBox();
            this.buttonAdaugaCarte = new System.Windows.Forms.Button();
            this.labelAddCarte = new System.Windows.Forms.Label();
            this.labelAddCarteTitlu = new System.Windows.Forms.Label();
            this.labelAddCarteISBN = new System.Windows.Forms.Label();
            this.textBoxAddCarteTitlu = new System.Windows.Forms.TextBox();
            this.textBoxAddCarteISBN = new System.Windows.Forms.TextBox();
            this.tabPageAdminAbonati = new System.Windows.Forms.TabPage();
            this.groupBoxAbonatii.SuspendLayout();
            this.tabControlAdministrator.SuspendLayout();
            this.tabPageAdminAngajati.SuspendLayout();
            this.tabPageAdminCarti.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAngajatInregistrare
            // 
            this.labelAngajatInregistrare.AutoSize = true;
            this.labelAngajatInregistrare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAngajatInregistrare.Location = new System.Drawing.Point(270, 85);
            this.labelAngajatInregistrare.Name = "labelAngajatInregistrare";
            this.labelAngajatInregistrare.Size = new System.Drawing.Size(240, 25);
            this.labelAngajatInregistrare.TabIndex = 1;
            this.labelAngajatInregistrare.Text = "Înregistrare angajat nou";
            // 
            // labelAngajatParola
            // 
            this.labelAngajatParola.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAngajatParola.AutoSize = true;
            this.labelAngajatParola.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAngajatParola.Location = new System.Drawing.Point(228, 171);
            this.labelAngajatParola.Name = "labelAngajatParola";
            this.labelAngajatParola.Size = new System.Drawing.Size(57, 20);
            this.labelAngajatParola.TabIndex = 7;
            this.labelAngajatParola.Text = "Parola";
            // 
            // labelAngajatUsername
            // 
            this.labelAngajatUsername.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAngajatUsername.AutoSize = true;
            this.labelAngajatUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAngajatUsername.Location = new System.Drawing.Point(228, 138);
            this.labelAngajatUsername.Name = "labelAngajatUsername";
            this.labelAngajatUsername.Size = new System.Drawing.Size(86, 20);
            this.labelAngajatUsername.TabIndex = 8;
            this.labelAngajatUsername.Text = "Username";
            // 
            // buttonAngajatRegister
            // 
            this.buttonAngajatRegister.Location = new System.Drawing.Point(336, 242);
            this.buttonAngajatRegister.Name = "buttonAngajatRegister";
            this.buttonAngajatRegister.Size = new System.Drawing.Size(101, 38);
            this.buttonAngajatRegister.TabIndex = 16;
            this.buttonAngajatRegister.Text = "Register";
            this.buttonAngajatRegister.UseVisualStyleBackColor = true;
            // 
            // textBoxAngajatUsername
            // 
            this.textBoxAngajatUsername.Location = new System.Drawing.Point(396, 134);
            this.textBoxAngajatUsername.Name = "textBoxAngajatUsername";
            this.textBoxAngajatUsername.Size = new System.Drawing.Size(165, 22);
            this.textBoxAngajatUsername.TabIndex = 10;
            // 
            // textBoxAngajatParola
            // 
            this.textBoxAngajatParola.Location = new System.Drawing.Point(396, 164);
            this.textBoxAngajatParola.Name = "textBoxAngajatParola";
            this.textBoxAngajatParola.Size = new System.Drawing.Size(165, 22);
            this.textBoxAngajatParola.TabIndex = 11;
            // 
            // groupBoxAbonatii
            // 
            this.groupBoxAbonatii.Controls.Add(this.tabControlAdministrator);
            this.groupBoxAbonatii.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAbonatii.Location = new System.Drawing.Point(0, 0);
            this.groupBoxAbonatii.Name = "groupBoxAbonatii";
            this.groupBoxAbonatii.Size = new System.Drawing.Size(841, 430);
            this.groupBoxAbonatii.TabIndex = 53;
            this.groupBoxAbonatii.TabStop = false;
            // 
            // tabControlAdministrator
            // 
            this.tabControlAdministrator.Controls.Add(this.tabPageAdminAngajati);
            this.tabControlAdministrator.Controls.Add(this.tabPageAdminCarti);
            this.tabControlAdministrator.Controls.Add(this.tabPageAdminAbonati);
            this.tabControlAdministrator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdministrator.Location = new System.Drawing.Point(3, 18);
            this.tabControlAdministrator.Name = "tabControlAdministrator";
            this.tabControlAdministrator.SelectedIndex = 0;
            this.tabControlAdministrator.Size = new System.Drawing.Size(835, 409);
            this.tabControlAdministrator.TabIndex = 58;
            // 
            // tabPageAdminAngajati
            // 
            this.tabPageAdminAngajati.Controls.Add(this.radioButtonAngajatBibliotecar);
            this.tabPageAdminAngajati.Controls.Add(this.labelAngajatInregistrare);
            this.tabPageAdminAngajati.Controls.Add(this.radioButtonAngajatAdministrator);
            this.tabPageAdminAngajati.Controls.Add(this.labelAngajatParola);
            this.tabPageAdminAngajati.Controls.Add(this.labelAngajatRol);
            this.tabPageAdminAngajati.Controls.Add(this.labelAngajatUsername);
            this.tabPageAdminAngajati.Controls.Add(this.textBoxAngajatParola);
            this.tabPageAdminAngajati.Controls.Add(this.buttonAngajatRegister);
            this.tabPageAdminAngajati.Controls.Add(this.textBoxAngajatUsername);
            this.tabPageAdminAngajati.Location = new System.Drawing.Point(4, 25);
            this.tabPageAdminAngajati.Name = "tabPageAdminAngajati";
            this.tabPageAdminAngajati.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdminAngajati.Size = new System.Drawing.Size(827, 380);
            this.tabPageAdminAngajati.TabIndex = 0;
            this.tabPageAdminAngajati.Text = "Angajați";
            this.tabPageAdminAngajati.UseVisualStyleBackColor = true;
            // 
            // radioButtonAngajatBibliotecar
            // 
            this.radioButtonAngajatBibliotecar.AutoSize = true;
            this.radioButtonAngajatBibliotecar.Location = new System.Drawing.Point(458, 203);
            this.radioButtonAngajatBibliotecar.Name = "radioButtonAngajatBibliotecar";
            this.radioButtonAngajatBibliotecar.Size = new System.Drawing.Size(92, 20);
            this.radioButtonAngajatBibliotecar.TabIndex = 57;
            this.radioButtonAngajatBibliotecar.TabStop = true;
            this.radioButtonAngajatBibliotecar.Text = "Bibliotecar";
            this.radioButtonAngajatBibliotecar.UseVisualStyleBackColor = true;
            // 
            // radioButtonAngajatAdministrator
            // 
            this.radioButtonAngajatAdministrator.AutoSize = true;
            this.radioButtonAngajatAdministrator.Location = new System.Drawing.Point(336, 203);
            this.radioButtonAngajatAdministrator.Name = "radioButtonAngajatAdministrator";
            this.radioButtonAngajatAdministrator.Size = new System.Drawing.Size(106, 20);
            this.radioButtonAngajatAdministrator.TabIndex = 56;
            this.radioButtonAngajatAdministrator.TabStop = true;
            this.radioButtonAngajatAdministrator.Text = "Administrator";
            this.radioButtonAngajatAdministrator.UseVisualStyleBackColor = true;
            // 
            // labelAngajatRol
            // 
            this.labelAngajatRol.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAngajatRol.AutoSize = true;
            this.labelAngajatRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAngajatRol.Location = new System.Drawing.Point(228, 202);
            this.labelAngajatRol.Name = "labelAngajatRol";
            this.labelAngajatRol.Size = new System.Drawing.Size(34, 20);
            this.labelAngajatRol.TabIndex = 55;
            this.labelAngajatRol.Text = "Rol";
            // 
            // tabPageAdminCarti
            // 
            this.tabPageAdminCarti.Controls.Add(this.buttonUpdate);
            this.tabPageAdminCarti.Controls.Add(this.textBoxIDCarte);
            this.tabPageAdminCarti.Controls.Add(this.labelUpdateIDCarte);
            this.tabPageAdminCarti.Controls.Add(this.labelUpdateCarte);
            this.tabPageAdminCarti.Controls.Add(this.buttonDeleteCarte);
            this.tabPageAdminCarti.Controls.Add(this.textBoxDeleteIDCarte);
            this.tabPageAdminCarti.Controls.Add(this.labelDeleteIDCarte);
            this.tabPageAdminCarti.Controls.Add(this.labelDelete);
            this.tabPageAdminCarti.Controls.Add(this.labelAddCarteGen);
            this.tabPageAdminCarti.Controls.Add(this.textBoxAddCarteGen);
            this.tabPageAdminCarti.Controls.Add(this.labelAddCarteEditura);
            this.tabPageAdminCarti.Controls.Add(this.labelAddCarteAutor);
            this.tabPageAdminCarti.Controls.Add(this.textBoxAddCarteEditura);
            this.tabPageAdminCarti.Controls.Add(this.textBoxAddCarteAutor);
            this.tabPageAdminCarti.Controls.Add(this.buttonAdaugaCarte);
            this.tabPageAdminCarti.Controls.Add(this.labelAddCarte);
            this.tabPageAdminCarti.Controls.Add(this.labelAddCarteTitlu);
            this.tabPageAdminCarti.Controls.Add(this.labelAddCarteISBN);
            this.tabPageAdminCarti.Controls.Add(this.textBoxAddCarteTitlu);
            this.tabPageAdminCarti.Controls.Add(this.textBoxAddCarteISBN);
            this.tabPageAdminCarti.Location = new System.Drawing.Point(4, 25);
            this.tabPageAdminCarti.Name = "tabPageAdminCarti";
            this.tabPageAdminCarti.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdminCarti.Size = new System.Drawing.Size(827, 380);
            this.tabPageAdminCarti.TabIndex = 1;
            this.tabPageAdminCarti.Text = "Cărți";
            this.tabPageAdminCarti.UseVisualStyleBackColor = true;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(563, 278);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(101, 38);
            this.buttonUpdate.TabIndex = 32;
            this.buttonUpdate.Text = "Modifică";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // textBoxIDCarte
            // 
            this.textBoxIDCarte.Location = new System.Drawing.Point(596, 241);
            this.textBoxIDCarte.Name = "textBoxIDCarte";
            this.textBoxIDCarte.Size = new System.Drawing.Size(196, 22);
            this.textBoxIDCarte.TabIndex = 31;
            // 
            // labelUpdateIDCarte
            // 
            this.labelUpdateIDCarte.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelUpdateIDCarte.AutoSize = true;
            this.labelUpdateIDCarte.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpdateIDCarte.Location = new System.Drawing.Point(474, 241);
            this.labelUpdateIDCarte.Name = "labelUpdateIDCarte";
            this.labelUpdateIDCarte.Size = new System.Drawing.Size(72, 20);
            this.labelUpdateIDCarte.TabIndex = 30;
            this.labelUpdateIDCarte.Text = "ID Carte";
            // 
            // labelUpdateCarte
            // 
            this.labelUpdateCarte.AutoSize = true;
            this.labelUpdateCarte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpdateCarte.Location = new System.Drawing.Point(529, 189);
            this.labelUpdateCarte.Name = "labelUpdateCarte";
            this.labelUpdateCarte.Size = new System.Drawing.Size(211, 25);
            this.labelUpdateCarte.TabIndex = 29;
            this.labelUpdateCarte.Text = "Modifică status carte";
            // 
            // buttonDeleteCarte
            // 
            this.buttonDeleteCarte.Location = new System.Drawing.Point(563, 105);
            this.buttonDeleteCarte.Name = "buttonDeleteCarte";
            this.buttonDeleteCarte.Size = new System.Drawing.Size(101, 38);
            this.buttonDeleteCarte.TabIndex = 28;
            this.buttonDeleteCarte.Text = "Șterge";
            this.buttonDeleteCarte.UseVisualStyleBackColor = true;
            // 
            // textBoxDeleteIDCarte
            // 
            this.textBoxDeleteIDCarte.Location = new System.Drawing.Point(596, 68);
            this.textBoxDeleteIDCarte.Name = "textBoxDeleteIDCarte";
            this.textBoxDeleteIDCarte.Size = new System.Drawing.Size(196, 22);
            this.textBoxDeleteIDCarte.TabIndex = 27;
            // 
            // labelDeleteIDCarte
            // 
            this.labelDeleteIDCarte.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelDeleteIDCarte.AutoSize = true;
            this.labelDeleteIDCarte.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDeleteIDCarte.Location = new System.Drawing.Point(474, 68);
            this.labelDeleteIDCarte.Name = "labelDeleteIDCarte";
            this.labelDeleteIDCarte.Size = new System.Drawing.Size(72, 20);
            this.labelDeleteIDCarte.TabIndex = 26;
            this.labelDeleteIDCarte.Text = "ID Carte";
            // 
            // labelDelete
            // 
            this.labelDelete.AutoSize = true;
            this.labelDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelete.Location = new System.Drawing.Point(549, 18);
            this.labelDelete.Name = "labelDelete";
            this.labelDelete.Size = new System.Drawing.Size(149, 25);
            this.labelDelete.TabIndex = 25;
            this.labelDelete.Text = "Ștergere carte";
            // 
            // labelAddCarteGen
            // 
            this.labelAddCarteGen.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAddCarteGen.AutoSize = true;
            this.labelAddCarteGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddCarteGen.Location = new System.Drawing.Point(15, 217);
            this.labelAddCarteGen.Name = "labelAddCarteGen";
            this.labelAddCarteGen.Size = new System.Drawing.Size(40, 20);
            this.labelAddCarteGen.TabIndex = 23;
            this.labelAddCarteGen.Text = "Gen";
            // 
            // textBoxAddCarteGen
            // 
            this.textBoxAddCarteGen.Location = new System.Drawing.Point(183, 217);
            this.textBoxAddCarteGen.Name = "textBoxAddCarteGen";
            this.textBoxAddCarteGen.Size = new System.Drawing.Size(196, 22);
            this.textBoxAddCarteGen.TabIndex = 24;
            // 
            // labelAddCarteEditura
            // 
            this.labelAddCarteEditura.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAddCarteEditura.AutoSize = true;
            this.labelAddCarteEditura.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddCarteEditura.Location = new System.Drawing.Point(15, 178);
            this.labelAddCarteEditura.Name = "labelAddCarteEditura";
            this.labelAddCarteEditura.Size = new System.Drawing.Size(62, 20);
            this.labelAddCarteEditura.TabIndex = 18;
            this.labelAddCarteEditura.Text = "Editură";
            // 
            // labelAddCarteAutor
            // 
            this.labelAddCarteAutor.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAddCarteAutor.AutoSize = true;
            this.labelAddCarteAutor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddCarteAutor.Location = new System.Drawing.Point(15, 141);
            this.labelAddCarteAutor.Name = "labelAddCarteAutor";
            this.labelAddCarteAutor.Size = new System.Drawing.Size(49, 20);
            this.labelAddCarteAutor.TabIndex = 19;
            this.labelAddCarteAutor.Text = "Autor";
            // 
            // textBoxAddCarteEditura
            // 
            this.textBoxAddCarteEditura.Location = new System.Drawing.Point(183, 178);
            this.textBoxAddCarteEditura.Name = "textBoxAddCarteEditura";
            this.textBoxAddCarteEditura.Size = new System.Drawing.Size(196, 22);
            this.textBoxAddCarteEditura.TabIndex = 21;
            // 
            // textBoxAddCarteAutor
            // 
            this.textBoxAddCarteAutor.Location = new System.Drawing.Point(183, 141);
            this.textBoxAddCarteAutor.Name = "textBoxAddCarteAutor";
            this.textBoxAddCarteAutor.Size = new System.Drawing.Size(196, 22);
            this.textBoxAddCarteAutor.TabIndex = 20;
            // 
            // buttonAdaugaCarte
            // 
            this.buttonAdaugaCarte.Location = new System.Drawing.Point(116, 254);
            this.buttonAdaugaCarte.Name = "buttonAdaugaCarte";
            this.buttonAdaugaCarte.Size = new System.Drawing.Size(101, 38);
            this.buttonAdaugaCarte.TabIndex = 17;
            this.buttonAdaugaCarte.Text = "Adaugă";
            this.buttonAdaugaCarte.UseVisualStyleBackColor = true;
            // 
            // labelAddCarte
            // 
            this.labelAddCarte.AutoSize = true;
            this.labelAddCarte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddCarte.Location = new System.Drawing.Point(111, 18);
            this.labelAddCarte.Name = "labelAddCarte";
            this.labelAddCarte.Size = new System.Drawing.Size(160, 25);
            this.labelAddCarte.TabIndex = 12;
            this.labelAddCarte.Text = "Adăugare carte";
            // 
            // labelAddCarteTitlu
            // 
            this.labelAddCarteTitlu.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAddCarteTitlu.AutoSize = true;
            this.labelAddCarteTitlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddCarteTitlu.Location = new System.Drawing.Point(15, 105);
            this.labelAddCarteTitlu.Name = "labelAddCarteTitlu";
            this.labelAddCarteTitlu.Size = new System.Drawing.Size(41, 20);
            this.labelAddCarteTitlu.TabIndex = 13;
            this.labelAddCarteTitlu.Text = "Titlu";
            // 
            // labelAddCarteISBN
            // 
            this.labelAddCarteISBN.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.labelAddCarteISBN.AutoSize = true;
            this.labelAddCarteISBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddCarteISBN.Location = new System.Drawing.Point(15, 72);
            this.labelAddCarteISBN.Name = "labelAddCarteISBN";
            this.labelAddCarteISBN.Size = new System.Drawing.Size(48, 20);
            this.labelAddCarteISBN.TabIndex = 14;
            this.labelAddCarteISBN.Text = "ISBN";
            // 
            // textBoxAddCarteTitlu
            // 
            this.textBoxAddCarteTitlu.Location = new System.Drawing.Point(183, 105);
            this.textBoxAddCarteTitlu.Name = "textBoxAddCarteTitlu";
            this.textBoxAddCarteTitlu.Size = new System.Drawing.Size(196, 22);
            this.textBoxAddCarteTitlu.TabIndex = 16;
            // 
            // textBoxAddCarteISBN
            // 
            this.textBoxAddCarteISBN.Location = new System.Drawing.Point(183, 68);
            this.textBoxAddCarteISBN.Name = "textBoxAddCarteISBN";
            this.textBoxAddCarteISBN.Size = new System.Drawing.Size(196, 22);
            this.textBoxAddCarteISBN.TabIndex = 15;
            // 
            // tabPageAdminAbonati
            // 
            this.tabPageAdminAbonati.Location = new System.Drawing.Point(4, 25);
            this.tabPageAdminAbonati.Name = "tabPageAdminAbonati";
            this.tabPageAdminAbonati.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdminAbonati.Size = new System.Drawing.Size(827, 380);
            this.tabPageAdminAbonati.TabIndex = 2;
            this.tabPageAdminAbonati.Text = "Abonați";
            this.tabPageAdminAbonati.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 430);
            this.Controls.Add(this.groupBoxAbonatii);
            this.Name = "Form1";
            this.Text = "Administrator";
            this.groupBoxAbonatii.ResumeLayout(false);
            this.tabControlAdministrator.ResumeLayout(false);
            this.tabPageAdminAngajati.ResumeLayout(false);
            this.tabPageAdminAngajati.PerformLayout();
            this.tabPageAdminCarti.ResumeLayout(false);
            this.tabPageAdminCarti.PerformLayout();
            this.ResumeLayout(false);

        }

        // Principal
        private System.Windows.Forms.Label autentificareLabel;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelRol;
        private System.Windows.Forms.RadioButton radioButtonBibliotecar;
        private System.Windows.Forms.RadioButton radioButtonAdministrator;


        #endregion
        // Bibliotecar
        private System.Windows.Forms.Label labelInregistrare;
        private System.Windows.Forms.Label labelInregistrareTelefon;
        private System.Windows.Forms.Label labelInregistrareEmail;
        private System.Windows.Forms.Label labelInregistrareAdresa;
        private System.Windows.Forms.Label labelInregistrarePrenume;
        private System.Windows.Forms.Label labelInregistrareNume;
        private System.Windows.Forms.Button buttonBibliotecarRegister;
        private System.Windows.Forms.Button buttonBibliotecarLogin;
        private System.Windows.Forms.Label labelAutentificareTelefon;
        private System.Windows.Forms.Label labelAutentificare;
        private System.Windows.Forms.TextBox textBoxInregistrareNume;
        private System.Windows.Forms.TextBox textBoxInregistrarePrenume;
        private System.Windows.Forms.TextBox textBoxInregistrareAdresa;
        private System.Windows.Forms.TextBox textBoxInregistrareTelefon;
        private System.Windows.Forms.TextBox textBoxInregistrareEmail;
        private System.Windows.Forms.TextBox textBoxAutentificareTelefon;
        private System.Windows.Forms.Button buttonRetur;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label labelReturIDImprumut;
        private System.Windows.Forms.Label labelRetur;
        private System.Windows.Forms.Button buttonImprumut;
        private System.Windows.Forms.TextBox textBoxImprumutPerioada;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelImprumutPerioada;
        private System.Windows.Forms.Label labelImprumutAutor;
        private System.Windows.Forms.Label labelImprumutTitlu;
        private System.Windows.Forms.Label labelImprumut;
        private System.Windows.Forms.GroupBox groupBoxAbonat;
        private System.Windows.Forms.GroupBox groupBoxServicii;

        // Administrator
        private System.Windows.Forms.Label labelAngajatInregistrare;
        private System.Windows.Forms.Label labelAngajatParola;
        private System.Windows.Forms.Label labelAngajatUsername;
        private System.Windows.Forms.Button buttonAngajatRegister;
        private System.Windows.Forms.TextBox textBoxAngajatUsername;
        private System.Windows.Forms.TextBox textBoxAngajatParola;
        private System.Windows.Forms.GroupBox groupBoxAbonatii;
        private System.Windows.Forms.RadioButton radioButtonAngajatBibliotecar;
        private System.Windows.Forms.RadioButton radioButtonAngajatAdministrator;
        private System.Windows.Forms.Label labelAngajatRol;


        private System.Windows.Forms.TabControl tabControlAdministrator;
        private System.Windows.Forms.TabPage tabPageAdminAngajati;
        private System.Windows.Forms.TabPage tabPageAdminCarti;
        private System.Windows.Forms.Label labelAddCarteGen;
        private System.Windows.Forms.TextBox textBoxAddCarteGen;
        private System.Windows.Forms.Label labelAddCarteEditura;
        private System.Windows.Forms.Label labelAddCarteAutor;
        private System.Windows.Forms.TextBox textBoxAddCarteEditura;
        private System.Windows.Forms.TextBox textBoxAddCarteAutor;
        private System.Windows.Forms.Button buttonAdaugaCarte;
        private System.Windows.Forms.Label labelAddCarte;
        private System.Windows.Forms.Label labelAddCarteTitlu;
        private System.Windows.Forms.Label labelAddCarteISBN;
        private System.Windows.Forms.TextBox textBoxAddCarteTitlu;
        private System.Windows.Forms.TextBox textBoxAddCarteISBN;
        private System.Windows.Forms.Button buttonDeleteCarte;
        private System.Windows.Forms.TextBox textBoxDeleteIDCarte;
        private System.Windows.Forms.Label labelDeleteIDCarte;
        private System.Windows.Forms.Label labelDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.TextBox textBoxIDCarte;
        private System.Windows.Forms.Label labelUpdateIDCarte;
        private System.Windows.Forms.Label labelUpdateCarte;
        private System.Windows.Forms.TabPage tabPageAdminAbonati;

    }
}

