/*************************************************************************
 *                                                                       *
 *  File:        AdminView.cs                                            *
 *  Copyright:   (c) 2025, A. Denisa                                     *
 *                                                                       *
 *  Description: Interfața admin                                         *
 *               Gestionează cărțile, angajații și abonații              *
 *                                                                       *
 *                                                                       *
 *  This code and information is provided "as is" without warranty of    *
 *  any kind, either expressed or implied, including but not limited     *
 *  to the implied warranties of merchantability or fitness for a        *
 *  particular purpose. You are free to use this source code in your     *
 *  applications as long as the original copyright notice is included.   *
 *                                                                       *      
 ************************************************************************ */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    /// <summary>
    /// Interfața pentru admin
    /// </summary>
    public partial class AdminView : Form
    {
        private Form _mainView;
        private ConnectionToClientBackend _connectionToClientBackend;
        private int _abonatId;
        /// <summary>
        /// Constructorul pentru AdminView
        /// </summary>
        public AdminView(Form mainView)
        {
            InitializeComponent();
            _connectionToClientBackend = new ConnectionToClientBackend(false);
            this._mainView = mainView;
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul face clic pe butonul de delogare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelogare_Click(object sender, EventArgs e)
        {
            // Delogare și revenire la pagina principală
            this._mainView.Show();
            this.Hide();
            this.Controls.Clear();
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul face clic pe butonul de căutare a abonaților
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonGestiuneAbonați_CheckedChanged(object sender, EventArgs e)
        {
            // Activăm interfața pentru gestiunea abonaților după numărul de telefon
            if (radioButtonGestiuneAbonați.Checked)
            {
                labelGestiuneAbonați.Enabled = true;
                textBoxGestiuneTelefon.Enabled = true;
                labelGestiuneAbonațiTelefon.Enabled = true;
                buttonGestiuneCautare.Enabled = true;
                labelGestiuneDate.Enabled = true;
                textBoxGestiuneDate.Enabled = true;

                labelAfișareAbonațiProbleme.Enabled = false;
                buttonAfișareAbonațiCăutare.Enabled = false;
                comboBoxAbonațiProbleme.Enabled = false;
            }

        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul face clic pe butonul de căutare a abonaților problematici
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonAfișareAbonați_CheckedChanged(object sender, EventArgs e)
        {
            // Activăm interfața pentru listarea abonaților problematici
            if (radioButtonAfișareAbonați.Checked)
            {
                labelAfișareAbonațiProbleme.Enabled = true;
                buttonAfișareAbonațiCăutare.Enabled = true;
                comboBoxAbonațiProbleme.Enabled = true;
                labelAfișareAbonați.Enabled = true;

                labelGestiuneAbonați.Enabled = false;
                textBoxGestiuneTelefon.Enabled = false;
                labelGestiuneAbonațiTelefon.Enabled = false;
                buttonGestiuneCautare.Enabled = false;
                labelGestiuneDate.Enabled = false;
                textBoxGestiuneDate.Enabled = false;
            }
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul face clic pe butonul de căutare a abonaților
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGestiuneCautare_Click(object sender, EventArgs e)
        {
            string numarTelefon = textBoxGestiuneTelefon.Text;
            string pattern = @"^07\d{8}$";

            if (string.IsNullOrEmpty(numarTelefon))
            {
                MessageBox.Show("Vă rugăm să introduceți un număr de telefon!");
                return;
            }
            if (!Regex.IsMatch(numarTelefon, pattern))
            {
                MessageBox.Show("Nu este acceptat acest format de număr de telefon!");
                return;
            }

            // TODO: IMPLEMENTEAZĂ ACEST REQUEST: CAUTĂ ABONAT DUPĂ NUMĂR DE TELEFON, RETURNEAZĂ ABONAT
            _connectionToClientBackend.SendRequest("loginAbonat", $"{numarTelefon}");
            this._abonatId = 0; // = id;    
            string response = _connectionToClientBackend.ReceiveResponse();

            if (response != "Abonat: Login Successful")
            {
                MessageBox.Show("Autentificare eșuată! Nu există niciun abonat cu acest număr de telefon.");
                return;
            }


            this.textBoxGestiuneDate.Enabled = true;
            this.buttonGestiuneValidare.Enabled = true;
            this.radioButtonGestiuneA.Enabled = true;
            this.radioButtonGestiuneB.Enabled = true;
            this.radioButtonGestiuneE.Enabled = true;

            this.comboBoxAbonațiProbleme.TabIndex = 0;
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul face clic pe butonul de căutare a abonaților cu probleme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAfișareAbonațiCăutare_Click(object sender, EventArgs e)
        {

            // TODO: IMPLEMENTEAZĂ ACEST REQUEST: CAUTĂ ABONAT DUPĂ ÎNTÂRZIEREA ÎMPRUMUTULUI
            _connectionToClientBackend.SendRequest("getAbonatiProbleme", $"");
            string response = _connectionToClientBackend.ReceiveResponse();

            if (response != "Successful")
            {
                MessageBox.Show("Nu există niciun abonat cu probleme.");
                return;
            }

            comboBoxAbonațiProbleme.Items.Clear();
            // adaugare

            if(comboBoxAbonațiProbleme.Items.Count == 0)
            {
                MessageBox.Show("Nu există niciun abonat cu probleme.");
                return;
            }

            // intoarce nume, prenume, și împrumutul cu întârzierea cea mai mare 
            this.buttonGestiuneValidare.Enabled = true;
            this.radioButtonGestiuneA.Enabled = true;
            this.radioButtonGestiuneB.Enabled = true;
            this.radioButtonGestiuneE.Enabled = true;

        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul face clic pe butonul de validare a modificării statusului unui abonat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGestiuneValidare_Click(object sender, EventArgs e)
        {
            
            if (radioButtonAfișareAbonați.Checked)
            {
                // Extragem obiectul de tip abonat din lista de abonați problematici
                string abonat = comboBoxAbonațiProbleme.SelectedItem.ToString();
                this._abonatId = int.Parse(abonat.Split(' ')[0]); // presupunem că id-ul este primul element din string
            }
            else if (radioButtonGestiuneAbonați.Checked)
            {
                // Extragem obiectul de tip abonat din cautarea dupa numar de telefon - implementat deja
               
            }

            // avem un obiect abonat rezultat
            if(radioButtonGestiuneA.Checked)
            {
                // aplicare restricții
                // TODO: IMPLEMENTEAZĂ ACEST REQUEST: APLICA RESTRICȚII (SCHIMBĂ STATUS ÎN "restrictionat")
                _connectionToClientBackend.SendRequest("applyRestrictions", $"{_abonatId}");

                string response = _connectionToClientBackend.ReceiveResponse();
                if (response == "Successful")
                {
                    MessageBox.Show("Restricții aplicate cu succes!");
                }
                else
                {
                    MessageBox.Show("Eroare la aplicarea restricțiilor: " + response);
                    return;
                }
            }
            if (radioButtonGestiuneB.Checked)
            {
                // eliminare restricții
                // TODO: IMPLEMENTEAZĂ ACEST REQUEST: ELIMINARE RESTRICȚII (SCHIMBĂ STATUS ÎN "fara restrictii")
                _connectionToClientBackend.SendRequest("removeRestrictions", $"{_abonatId}");
                string response = _connectionToClientBackend.ReceiveResponse();
                if (response == "Successful")
                {
                    MessageBox.Show("Restricții eliminate cu succes!");
                }
                else
                {
                    MessageBox.Show("Eroare la eliminarea restricțiilor: " + response);
                    return;
                }
            }
            if (radioButtonGestiuneE.Checked)
            {
                // blocare abonat
                // TODO: IMPLEMENTEAZĂ ACEST REQUEST: BLOCARE ABONAT (SCHIMBĂ STATUS ÎN "blocat")
                _connectionToClientBackend.SendRequest("blockAbonat", $"{_abonatId}");
                string response = _connectionToClientBackend.ReceiveResponse();
                if (response == "Successful")
                {
                    MessageBox.Show("Abonat blocat cu succes!");
                }
                else
                {
                    MessageBox.Show("Eroare la blocarea abonatului: " + response);
                    return;
                }
            }
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul înregistrează un nou angajat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void buttonAngajatRegister_Click(object sender, EventArgs e)
        {
            string username = textBoxAngajatUsername.Text;
            string password = textBoxAngajatParola.Text;
           
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Introduceti un nume de utilizator și o parolă valide.");
                return;
            }

            if(!radioButtonAngajatAdministrator.Checked && !radioButtonAngajatBibliotecar.Checked)
            {
                MessageBox.Show("Selectați un rol pentru angajat.");
                return;
            }

            string rol = radioButtonAngajatAdministrator.Checked ? "Administrator" : "Bibliotecar";

            // TODO: Trimiteți datele angajatului către server pentru a fi înregistrate cu verificare dacă există deja un angajat cu acel nume de utilizator
            _connectionToClientBackend.SendRequest("registerEmployee", $"{username}|{password}|{rol}");


            string response = "";
            if (response == "Successful")
            {
                MessageBox.Show("Angajat înregistrat cu succes!");
            }
            else
            {
                MessageBox.Show("Eroare la înregistrarea angajatului: " + response);
            }
            textBoxAngajatUsername.Clear();
            textBoxAngajatParola.Clear();
            radioButtonAngajatAdministrator.Checked = false;
            radioButtonAngajatBibliotecar.Checked = false;
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul adaugă o carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void buttonAdaugaCarte_Click(object sender, EventArgs e)
        {
            string ISBN = textBoxAddCarteISBN.Text;
            string titlu = textBoxAddCarteTitlu.Text;
            string autor = textBoxAddCarteAutor.Text;
            string editura = textBoxAddCarteEditura.Text;
            string gen = textBoxAddCarteGen.Text;
            if (string.IsNullOrEmpty(ISBN) || string.IsNullOrEmpty(titlu) || string.IsNullOrEmpty(autor) || string.IsNullOrEmpty(editura) || string.IsNullOrEmpty(gen))
            {
                MessageBox.Show("Introduceti toate datele cărții.");
                return;
            }

            // TODO: Trimiteți datele cărții către server pentru a fi adăugate în baza de date, cu verificare dacă există deja o carte cu acel ISBN
            _connectionToClientBackend.SendRequest("addBook", $"{ISBN}|{titlu}|{autor}|{editura}|{gen}");
            
            string response = "";
            if (response == "Successful")
            {
                MessageBox.Show("Carte adăugată cu succes!");
            }
            else
            {
                MessageBox.Show("Eroare la adăugarea cărții: " + response);
                return;
            }
            textBoxAddCarteISBN.Clear();
            textBoxAddCarteTitlu.Clear();
            textBoxAddCarteAutor.Clear();
            textBoxAddCarteEditura.Clear();
            textBoxAddCarteEditura.Clear();
        }


        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul caută cărți de șters după ISBN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonȘtergereCarteCăutare_Click(object sender, EventArgs e)
        {
            comboBoxStergereCarti.Items.Clear();
            string ISBN = textBoxDeleteIDCarte.Text;
            if (string.IsNullOrEmpty(ISBN))
            {
                MessageBox.Show("Introduceti un ISBN valid.");
                return;
            }

            // TODO: Trimiteți cererea de căutare a cărților cu ISBN-ul introdus către server și obțineți lista de cărți pentru a le adăuga în comboBoxStergereCarti
            _connectionToClientBackend.SendRequest("searchBook", $"{ISBN}");
            // listează toate cărțile cu ISBN-ul introdus

            string response = "";
            if (response == "Successful")
            {
                // Adăugați cărțile găsite în comboBoxStergereCarti

                if(comboBoxStergereCarti.Items.Count == 0)
                {
                    MessageBox.Show("Nu s-au găsit cărți cu acest ISBN.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Eroare la căutarea cărților: " + response);
                return;
            }
            buttonDeleteCarte.Enabled = true;
            comboBoxStergereCarti.Enabled = true;
        }


        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul validează ștergerea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void buttonDeleteCarte_Click(object sender, EventArgs e)
        {
            if(comboBoxStergereCarti.SelectedItem == null)
            {
                MessageBox.Show("Selectați o carte din listă.");
                return;
            }

            string carte = comboBoxStergereCarti.SelectedItem.ToString();
            string carteId = carte.Split(' ')[0]; // presupunem că id-ul este primul element din string

            // TODO: Trimiteți cererea de ștergere a cărții cu id-ul selectat către server
            _connectionToClientBackend.SendRequest("deleteBook", $"{carteId}");
            string response = "";
            if(response == "Successful")
            {
                MessageBox.Show("Carte ștearsă cu succes!");
            }
            else
            {
                MessageBox.Show("Eroare la ștergerea cărții: " + response);
                return;
            }
        }
    }
}
