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
        private readonly Form _mainView;
        private readonly ConnectionToClientBackend _connectionToClientBackend;
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
        private void ButtonDelogare_Click(object sender, EventArgs e)
        {
            // Delogare și revenire la pagina principală
            _connectionToClientBackend.SendRequest("logout", "\n");
            
            this._mainView.Show();
            this.Hide();
            this.Controls.Clear();
        }
        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul face clic pe butonul de căutare a abonaților
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonGestiuneAbonați_CheckedChanged(object sender, EventArgs e)
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
        private void RadioButtonAfișareAbonați_CheckedChanged(object sender, EventArgs e)
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
        private void ButtonGestiuneCautare_Click(object sender, EventArgs e)
        {
            textBoxGestiuneDate.Clear();
            string numarTelefon = textBoxGestiuneTelefon.Text;
            string pattern = @"^(\+407|07)[0-9]{8}$";

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

            _connectionToClientBackend.SendRequest("loginSubscriber", $"{numarTelefon}\n");
            string r = _connectionToClientBackend.ReceiveResponse();
            //MessageBox.Show(r);
            string[] response = r.Split('|');

            if (response[0] != "Subscriber Login successful")
            {
                MessageBox.Show("Autentificare eșuată! Nu există niciun abonat cu acest număr de telefon.");
                return;
            }

            textBoxGestiuneTelefon.Clear();

            _abonatId = int.Parse(response[1]);
            string status = response[2];
            string nume = response[3];
            string prenume = response[4];
            string limitaCarti = response[8];

            this.textBoxGestiuneDate.Enabled = true;

            this.textBoxGestiuneDate.Text = $"ID: {this._abonatId}\t" +
                                $"Nume: {nume}\t" +
                                $"Prenume: {prenume}\t" +
                                $"Status: {status}\t" +
                                $"Limită număr de cărți împrumutate simultan: {limitaCarti}";
            
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
        private void ButtonAfișareAbonațiCăutare_Click(object sender, EventArgs e)
        {
            _connectionToClientBackend.SendRequest("searchSubscribers", $"\n");
            string []response = _connectionToClientBackend.ReceiveResponse().Split('|');

            if (response[0] == "No subscribers found with restrictions or blocked.")
            {
                MessageBox.Show("Nu există niciun abonat cu probleme.");
                return;
            }

            comboBoxAbonațiProbleme.Items.Clear();

            foreach (string item in response)
            {
                string[] abonat = item.Split('~');
                if (abonat.Length < 7)
                {
                    continue; // dacă nu avem toate informațiile despre abonat, trecem la următorul
                }
                string idAbonat = abonat[0];
                string numeAbonat = abonat[1];
                string prenumeAbonat = abonat[2];
                string telefonAbonat = abonat[4];
                string emailAbonat = abonat[5];
                string statusAbonat = abonat[6];
                string limitaCartiAbonat = abonat[7];
                string zileIntarziere = abonat[8];
                comboBoxAbonațiProbleme.Items.Add($"{idAbonat}, {numeAbonat}, {prenumeAbonat}, {telefonAbonat}, {emailAbonat}, {statusAbonat}, {limitaCartiAbonat}, {zileIntarziere}");
            }
            
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
        private void ButtonGestiuneValidare_Click(object sender, EventArgs e)
        {
            if (radioButtonAfișareAbonați.Checked)
            {
                // Extragem obiectul de tip abonat din lista de abonați problematici
                string []abonat = comboBoxAbonațiProbleme.SelectedItem.ToString().Split(',');
                this._abonatId = int.Parse(abonat[0].Trim());
            }
            else if (radioButtonGestiuneAbonați.Checked)
            {
                string []abonat = textBoxGestiuneDate.Text.Split('\t');
                this._abonatId = int.Parse(abonat[0].Split(':')[1].Trim());
            }

            string status = "";

            if(radioButtonGestiuneA.Checked)
            {
                // blocare abonat
                status = "cu restrictii";
            }
            if (radioButtonGestiuneB.Checked)
            {
                
                status = "blocat";
            }
            if (radioButtonGestiuneE.Checked)
            {   
                // eliminare restricții
                status = "fara restrictii";
            }

            if (string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Selectați o opțiune de modificare a statusului abonatului.");
                return;
            }

            _connectionToClientBackend.SendRequest("updateStatus", $"{this._abonatId}|{status}\n");
            string response = _connectionToClientBackend.ReceiveResponse();
            if (response == "Status updated successful.")
            {
                MessageBox.Show("Statusul abonatului a fost actualizat cu succes!");
            }
            else
            {
                MessageBox.Show("Nu s-a reușit actualizarea statusului abonatului!");
                return;
            }
            // Resetăm câmpurile
            textBoxGestiuneDate.Clear();
            textBoxGestiuneTelefon.Clear();
            comboBoxAbonațiProbleme.Items.Clear();
            comboBoxAbonațiProbleme.SelectedItem = null;
            radioButtonGestiuneA.Checked = false;
            radioButtonGestiuneB.Checked = false;
            radioButtonGestiuneE.Checked = false;
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul înregistrează un nou angajat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ButtonAngajatRegister_Click(object sender, EventArgs e)
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

            string rol = radioButtonAngajatAdministrator.Checked ? "administrator" : "bibliotecar";

            _connectionToClientBackend.SendRequest("registerEmployee", $"{username}|{password}|{rol}\n");

            string response = _connectionToClientBackend.ReceiveResponse();
            if (response == "Employee Register successful.")
            {
                MessageBox.Show("Angajat înregistrat cu succes!");
                textBoxAngajatUsername.Clear();
                textBoxAngajatParola.Clear();
                radioButtonAngajatAdministrator.Checked = false;
                radioButtonAngajatBibliotecar.Checked = false;
            }
            else
            {
                MessageBox.Show("Încercare de înregistrare angajat eșuată.");
                return;
            }
     }

        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul adaugă o carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ButtonAdaugaCarte_Click(object sender, EventArgs e)
        {
            string ISBN = textBoxAddCarteISBN.Text;
            string titlu = textBoxAddCarteTitlu.Text;
            string autor = textBoxAddCarteAutor.Text;
            string editura = textBoxAddCarteEditura.Text;
            string gen = textBoxAddCarteGen.Text;
            string pattern = "^97[89]\\d{10}$";
            if (string.IsNullOrEmpty(ISBN) || string.IsNullOrEmpty(titlu) || string.IsNullOrEmpty(autor) || string.IsNullOrEmpty(editura) || string.IsNullOrEmpty(gen))
            {
                MessageBox.Show("Introduceti toate datele cărții.");
                return;
            }

            if(!Regex.IsMatch(ISBN, pattern))
            {
                MessageBox.Show("Nu este acceptat acest format de ISBN!");
                return;
            }
            _connectionToClientBackend.SendRequest("addBook", $"{ISBN}|{titlu}|{autor}|{editura}|{gen}\n");
            
            string response = _connectionToClientBackend.ReceiveResponse();
            if (response == "Book added successful.")
            {
                MessageBox.Show("Carte adăugată cu succes!");
            }
            else
            {
                MessageBox.Show("Nu s-a reușit adăgarea cărții!");
                return;
            }
            textBoxAddCarteISBN.Clear();
            textBoxAddCarteTitlu.Clear();
            textBoxAddCarteAutor.Clear();
            textBoxAddCarteEditura.Clear();
            textBoxAddCarteGen.Clear();
        }
        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul caută cărți de șters după ISBN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonȘtergereCarteCăutare_Click(object sender, EventArgs e)
        {
            comboBoxStergereCarti.Items.Clear();
            string ISBN = textBoxDeleteIDCarte.Text;

            string pattern = "^97[89]\\d{10}$";
            if (string.IsNullOrEmpty(ISBN))
            {
                MessageBox.Show("Introduceti un ISBN valid.");
                return;
            }

            if(!Regex.IsMatch(ISBN, pattern))
            {
                MessageBox.Show("Nu este acceptat acest format de ISBN!");
                return;
            }

            _connectionToClientBackend.SendRequest("searchBook", $"{ISBN}\n");

            string []response = _connectionToClientBackend.ReceiveResponse().Split('|');

            if (response[0] == "No books found.")
            {
                MessageBox.Show("Nu s-au găsit cărți cu acest ISBN.");
                return;
            }

            foreach (var r in response)
            {
                string[] book = r.Split('~');
                if (book.Length < 4)
                {
                    continue; // dacă nu avem toate informațiile despre carte, trecem la următoarea
                }
                string idCarte = book[0];
                string titluCarte = book[1];
                string autorCarte = book[2];
                string edituraCarte = book[3];
                comboBoxStergereCarti.Items.Add($"{titluCarte}, {autorCarte}, {edituraCarte}, {idCarte}"); // adăugăm cartea în comboBox
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
        private void ButtonDeleteCarte_Click(object sender, EventArgs e)
        {
            if(comboBoxStergereCarti.SelectedItem == null)
            {
                MessageBox.Show("Selectați o carte din listă.");
                return;
            }

            string []carte = comboBoxStergereCarti.SelectedItem.ToString().Split(',');
            int carteId = int.Parse(carte[3].Trim());

            _connectionToClientBackend.SendRequest("deleteBook", $"{carteId}\n");
            string response = _connectionToClientBackend.ReceiveResponse();
            if (response == "Book deleted successful.")
            {
                MessageBox.Show("Carte ștearsă cu succes!");
            }
            else
            {
                MessageBox.Show("Nu s-a reușit ștergerea cărții selectate!");
                return;
            }
            comboBoxStergereCarti.SelectedItem = null;
            comboBoxStergereCarti.Items.Clear();
        }
        /// <summary>
        /// Acest eveniment se declanșează atunci când administratorul șterge un angajat (bibliotecar)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAngajatDelete_Click(object sender, EventArgs e)
        {
            string username = textBoxAngajatStergereUsername.Text;
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Introduceti un nume de utilizator valid.");
                return;
            }

            _connectionToClientBackend.SendRequest("deleteEmployee", $"{username}\n");
            string response = _connectionToClientBackend.ReceiveResponse();
            if (response == "Librarian deleted successful.")
            {
                MessageBox.Show("Angajat șters cu succes!");
            }
            else
            {
                MessageBox.Show("Nu s-a reușit ștergerea angajatului!");
                return;
            }
        }
    }
}
