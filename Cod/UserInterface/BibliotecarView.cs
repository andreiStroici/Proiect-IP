/*************************************************************************
 *                                                                       *
 *  File:        BibliotecarView.cs                                      *
 *  Copyright:   (c) 2025, A. Denisa                                     *
 *                                                                       *
 *  Description: Interfața bibliotecar                                   *
 *               Gestionează abonați, împrumuturi și retururi de cărți   *
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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Win32;

namespace UserInterface
{
    /// <summary>
    /// Interfața pentru bibliotecar
    /// </summary>    
    public partial class BibliotecarView : Form
    {
        private ConnectionToClientBackend _connectionToClientBackend;
        private Form _mainView;
        private int _abonatId;
        private string _abonatStatus;
        /// <summary>
        /// Constructorul pentru BibliotecarView
        /// </summary>        
        public BibliotecarView(Form mainView)
        {
            this._mainView = mainView;
            InitializeComponent();
            _connectionToClientBackend = new ConnectionToClientBackend(false);
        }
        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de autentificare a unui abonat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAbonatLogin_Click(object sender, EventArgs e)
        {
            panelServiciuReturnare.Enabled = false;
            groupBoxServicii.Enabled = false;
            textBoxImprumutAutor.Clear();
            textBoxImprumutTitlu.Clear();
            comboBoxSugestii.Items.Clear();
            comboBoxReturnare.Items.Clear();

            string numarTelefon = textBoxAutentificareTelefon.Text;
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
            MessageBox.Show(r);
            string[] response = r.Split('|');

            if (response[0] != "Subscriber Login successful")
            {
                MessageBox.Show("Autentificare eșuată! Nu există niciun abonat cu acest număr de telefon.");
                return;
            }

            textBoxAutentificareTelefon.Clear();

            _abonatId = int.Parse(response[1]);

            // tot aici fac o interogare pe statusul abonatului, dacă e blocat afișez messageBox și păstrez câmpurile blocate
            if (response[2] == "blocat")
            {
                MessageBox.Show("Abonatul este blocat! Nu poate efectua împrumuturi.");
                panelServiciuReturnare.Enabled = true;
                return;
            }

            else if (response[2] == "cu restrictii")
            {
                MessageBox.Show("Abonatul are restricții! Nu poate împrumuta cărți acasă.");
                _abonatStatus = "restrictionat";
            }

            else
            {
                _abonatStatus = "fara restrictii";
            }


            // Activează interfața pentru gestiunea serviciilor de împrumut și retur
            groupBoxServicii.Enabled = true;
            panelServiciuReturnare.Enabled = true;
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de înregistrare a unui abonat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAbonatRegister_Click(object sender, EventArgs e)
        {
            string name = textBoxAbonatNume.Text;
            string password = textBoxAbonatPrenume.Text;
            string adresa = textBoxAbonatAdresa.Text;
            string telefon = textBoxAbonatTelefon.Text;
            string email = textBoxAbonatEmail.Text;

            string patternEmail = @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            string patternTelefon = @"^(\+407|07)[0-9]{8}$";
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(adresa) ||
                string.IsNullOrEmpty(telefon) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vă rugăm să completați toate câmpurile!");
                return;
            }

            if (!Regex.IsMatch(telefon, patternTelefon))
            {
                MessageBox.Show("Nu este acceptat acest format de număr de telefon!");
                return;
            }

            if (!Regex.IsMatch(email, patternEmail))
            {
                MessageBox.Show("Nu este acceptat acest format de email!");
                return;
            }

            _connectionToClientBackend.SendRequest("registerSubscriber", $"{name}|{password}|{adresa}|{telefon}|{email}\n");
            string response = _connectionToClientBackend.ReceiveResponse();
            if (response == "Subscriber Register successful")
            {
                MessageBox.Show("Înregistrarea a fost efectuată cu succes!");
                textBoxAbonatNume.Clear();
                textBoxAbonatPrenume.Clear();
                textBoxAbonatAdresa.Clear();
                textBoxAbonatTelefon.Clear();
                textBoxAbonatEmail.Clear();
            }
            else
            {
                MessageBox.Show("Înregistrarea a eșuat! Vă rugăm să încercați din nou.");
            }
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de căutare parțială
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImprumutCautare_Click(object sender, EventArgs e)
        {
            string titlu = textBoxImprumutTitlu.Text;
            string autor = textBoxImprumutAutor.Text;
            comboBoxSugestii.Items.Clear();
            if (string.IsNullOrEmpty(titlu) && string.IsNullOrEmpty(autor))
            {
                MessageBox.Show("Vă rugăm să introduceți un titlu sau un autor!");
                return;
            }

            _connectionToClientBackend.SendRequest("searchBooks", $"{titlu}|{autor}\n");

            string []response = _connectionToClientBackend.ReceiveResponse().Split('|');

            // 1~Moarte pe Nil~Agatha Cristie
            if (response[0] == "No books found.")
            {
                MessageBox.Show("Nu am găsit nicio carte disponibilă!");
                return;
            }

            if(_abonatStatus != "restrictionat" && _abonatStatus != "blocat")
            {
                radioButtonÎmprumutAcasă.Enabled = true;
            }
            else
            {
                radioButtonÎmprumutAcasă.Enabled = false;
            }

                foreach (var r in response)
                {
                    string[] book = r.Split('~');
                    if (book.Length < 3)
                    {
                        continue; // dacă nu avem toate informațiile despre carte, trecem la următoarea
                    }
                    string idCarte = book[0];
                    string titluCarte = book[1];
                    string autorCarte = book[2];
                    comboBoxSugestii.Items.Add($"{titluCarte}, {autorCarte}, {idCarte}"); // adăugăm cartea în comboBox
                }

            // Activează interfața pentru căutarea cărților disponibile (se efectuează căutări parțiale)
            labelImprumutSugestii.Enabled = true;
            comboBoxSugestii.Enabled = true;
            buttonImprumutValidare.Enabled = true;
            radioButtonImprumutSalaLectura.Enabled = true;

        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de delogare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelogare_Click(object sender, EventArgs e)
        {
            // Delogare și revenire la pagina principală
            _connectionToClientBackend.SendRequest("logout", "\n");

            this._mainView.Show();
            this.Hide();
            this.Controls.Clear();
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de validare a împrumutului
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void buttonImprumutValidare_Click(object sender, EventArgs e)
        {
            if (comboBoxSugestii.SelectedItem == null)
            {
                MessageBox.Show("Vă rugăm să selectați o carte din sugestii!");
                return;
            }

            string []selectedBook = comboBoxSugestii.SelectedItem.ToString().Split(',');
            int idBook = int.Parse(selectedBook[2].Trim());
            string selectedLocation = "";

            if(!radioButtonImprumutSalaLectura.Checked && !radioButtonÎmprumutAcasă.Checked)
            {
                MessageBox.Show("Vă rugăm să selectați o opțiune de împrumut!");
                return;
            }
            if (radioButtonImprumutSalaLectura.Checked)
            {
                selectedLocation = "sala de lectura";
            }
            else
            {
                selectedLocation = "acasa";
            }

            _connectionToClientBackend.SendRequest("insertLoan", $"{this._abonatId}|{idBook}|{selectedLocation}\n");
            string response = _connectionToClientBackend.ReceiveResponse();
            if (response == "Inserted Loan successful.")
            {
                MessageBox.Show("Împrumutul a fost efectuat cu succes!");
            }
            else
            {
                MessageBox.Show("Împrumutul nu a fost efectuat!");
                return;
            }

            textBoxImprumutTitlu.Clear();
            textBoxImprumutAutor.Clear();
            comboBoxSugestii.SelectedItem = null;
            comboBoxSugestii.Items.Clear();
            radioButtonImprumutSalaLectura.Checked = false;
            radioButtonÎmprumutAcasă.Checked = false;
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de validare a returului
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void buttonRetur_Click(object sender, EventArgs e)
        {
            if(comboBoxReturnare.Items.Count == 0)
            {
                MessageBox.Show("Nu există cărți de returnat!");
                return;
            }

            if(comboBoxReturnare.SelectedItem == null)
            {
                MessageBox.Show("Vă rugăm să selectați o carte din lista de returnat!");
                return;
            }

            string carte = comboBoxReturnare.SelectedItem.ToString();
            string idCarte = carte.Split(',')[2].Trim();

            _connectionToClientBackend.SendRequest("returnBook", $"{this._abonatId}|{idCarte}\n");

            string response = _connectionToClientBackend.ReceiveResponse();
            if (response == "Return successful.")
            {
                MessageBox.Show("Cartea a fost returnată cu succes!");
            }
            else
            {
                MessageBox.Show("Returnarea nu a fost efectuată!");
                return;
            }

            comboBoxReturnare.SelectedItem = null;
            comboBoxReturnare.Items.Clear();
        }

        private void buttonReturCautare_Click(object sender, EventArgs e)
        {
            // vom trimite către comboboxul de returnare cărți toate acele împrumuturi nefinalizate (cărți nereturnate)
            comboBoxReturnare.SelectedItem = null;
            comboBoxReturnare.Items.Clear();
            _connectionToClientBackend.SendRequest("getLoans", $"{this._abonatId}\n");

            string response = _connectionToClientBackend.ReceiveResponse();
            if (response == "No loans found.")
            {
                MessageBox.Show("Nu există cărți de returnat!");
                return;
            }

            string[] loans = response.Split('|');
            foreach (string s in loans)
            {
                string[] book = s.Split('~');
                if (book.Length < 3)
                {
                    continue; // dacă nu avem toate informațiile despre carte, trecem la următoarea
                }
                string idCarte = book[0];
                string titluCarte = book[1];
                string autorCarte = book[2];
                comboBoxReturnare.Items.Add($"{titluCarte}, {autorCarte}, {idCarte}"); // adăugăm cartea în comboBox
            }
        }
    }
}
