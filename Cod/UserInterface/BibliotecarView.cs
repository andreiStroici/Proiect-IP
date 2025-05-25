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
        private void buttonBibliotecarLogin_Click(object sender, EventArgs e)
        {

            string numarTelefon = textBoxAutentificareTelefon.Text;
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

            // TODO: IMPLEMENTEAZĂ ACEST REQUEST: CAUTĂ ABONAT DUPĂ NUMĂR DE TELEFON (RETURNEAZĂ UN ABONAT)
            _connectionToClientBackend.SendRequest("loginAbonat", $"{numarTelefon}\n");
            string response = _connectionToClientBackend.ReceiveResponse();

            if (response != "Abonat: Login Successful")
            {
                MessageBox.Show("Autentificare eșuată! Nu există niciun abonat cu acest număr de telefon.");
                return;
            }

            // tot aici fac o interogare pe statusul abonatului, dacă e blocat afișez messageBox și păstrez câmpurile blocate
            string status = ""; // status = abonat.status
            if(status == "blocat")
            {
                MessageBox.Show("Abonatul este blocat! Nu poate efectua împrumuturi.");
                return;
            }

            if(status == "restrictionat")
            {
                MessageBox.Show("Abonatul are restricții! Nu poate împrumuta cărți acasă.");
                radioButtonÎmprumutAcasă.Enabled = false;
            }

            // Activează interfața pentru gestiunea serviciilor de împrumut și retur
            groupBoxServicii.Enabled = true;
            panelServiciuReturnare.Enabled = true;

            // tot aici vom trimite către comboboxul de returnare cărți toate acele împrumuturi nefinalizate (cărți nereturnate)
            // TODO: AM NEVOIE DE LISTA DE CĂRȚI NERETURNATE ÎN FUNCȚIE DE ID-UL CLIENTULUI
            _connectionToClientBackend.SendRequest("getImprumuturi", $"{this._abonatId}\n");

            response = "";
            if(response != "Successul")
            {
                MessageBox.Show("Nu am găsit nicio carte împrumutată!");
                return;
            }
 

            comboBoxReturnare.Items.Clear();
            // comboBoxReturnare.Items.Add("");
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de înregistrare a unui abonat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAngajatRegister_Click(object sender, EventArgs e)
        {
            string name = textBoxAbonatNume.Text;
            string password = textBoxAbonatPrenume.Text;  
            string adresa = textBoxAbonatAdresa.Text;
            string telefon = textBoxAbonatTelefon.Text;
            string email = textBoxAbonatEmail.Text;

            string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string patternTelefon = @"^07\d{8}$";
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

            _connectionToClientBackend.SendRequest("register", $"{name}|{password}|{adresa}|{telefon}|{email}\n");
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

            if(string.IsNullOrEmpty(titlu) && string.IsNullOrEmpty(autor))
            {
                MessageBox.Show("Vă rugăm să introduceți un titlu sau un autor!");
                return;
            }

            // TODO: IMPLEMENTEAZĂ ACEST REQUEST: CAUTĂ CĂRȚILE DISPONIBILE ÎN FUNCȚIE DE TITLU SAU AUTOR (RETURNEAZĂ O LISTĂ)
            _connectionToClientBackend.SendRequest("searchBooks", $"{titlu}|{autor}\n");

            string response = ""; // response = _connectionToClientBackend.ReceiveResponse();

            if (response != "Successul")
            {
                MessageBox.Show("Nu am găsit nicio carte disponibilă!");
                return;
            }

            int count = 0; // reținem numărul de cărți găsite
            comboBoxSugestii.Items.Clear();
            //comboBoxSugestii.Items.Add("Căutare după titlu");

            if (count == 0)
            {
                MessageBox.Show("Nu am găsit nicio carte disponibilă!");
                return;
            }

            // Activează interfața pentru căutarea cărților disponibile (se efectuează căutări parțiale)
            labelImprumutSugestii.Enabled = true;
            comboBoxSugestii.Enabled = true;
            buttonImprumutValidare.Enabled = true;

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

            string carte = comboBoxSugestii.SelectedItem.ToString();
            string idCarte = carte.Split('|')[0];
            string locatie = "";

            if(!radioButtonImprumutSalaLectura.Checked && !radioButtonÎmprumutAcasă.Checked)
            {
                MessageBox.Show("Vă rugăm să selectați o opțiune de împrumut!");
                return;
            }
            if (radioButtonImprumutSalaLectura.Checked)
            {
                locatie = "sala de lectura";
            }
            else
            {
                locatie = "acasa";
            }

            // TODO: TRIMITE ÎMPRUMUTUL CĂTRE SERVER ȘI INDISPONIBILIZEAZĂ CARTEA ÎMPRUMUTATĂ
            _connectionToClientBackend.SendRequest("imprumut", $"{this._abonatId}|{idCarte}|{locatie}\n");
            string response = "";
            if(response == "Successful")
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
            comboBoxSugestii.Items.Clear();
            radioButtonImprumutSalaLectura.Checked = false;
            radioButtonÎmprumutAcasă.Checked = false;
            panelServiciuReturnare.Enabled = true;
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
            string idCarte = carte.Split('|')[0];
            string dataCurenta = DateTime.Now.ToString("yyyy-MM-dd");

            // TODO: TRIMITE RETURUL CĂTRE SERVER CU DATA CURENTĂ CA DATĂ DE RESTITUIRE
            _connectionToClientBackend.SendRequest("returnare", $"{this._abonatId}|{idCarte}|{dataCurenta}\n");

            string response = "";
            if (response == "Successful")
            {
                MessageBox.Show("Cartea a fost returnată cu succes!");
            }
            else
            {
                MessageBox.Show("Returnarea nu a fost efectuată!");
                return;
            }

            comboBoxReturnare.Items.Clear();
        }
    }
}
