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

namespace UserInterface
{
    /// <summary>
    /// Interfața pentru bibliotecar
    /// </summary>    
    public partial class BibliotecarView : Form
    {
        private ConnectionToClientBackend _connectionToClientBackend;
        private Form mainView;
        /// <summary>
        /// Constructorul pentru BibliotecarView
        /// </summary>        
        public BibliotecarView(Form mainView)
        {
            this.mainView = mainView;
            InitializeComponent();
            _connectionToClientBackend = new ConnectionToClientBackend();
        }
        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de autentificare a unui abonat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBibliotecarLogin_Click(object sender, EventArgs e)
        {
            // Activează interfața pentru gestiunea serviciilor de împrumut și retur
            groupBoxServicii.Enabled = true;
            panelServiciuReturnare.Enabled = true;
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de înregistrare a unui abonat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAngajatRegister_Click(object sender, EventArgs e)
        {
            // Activează interfața pentru gestiunea serviciilor de împrumut și retur
            groupBoxServicii.Enabled = true;
            string name = textBoxAbonatNume.Text;
            string password = textBoxAbonatPrenume.Text;  
            string adresa = textBoxAbonatAdresa.Text;
            string telefon = textBoxAbonatTelefon.Text;
            string email = textBoxAbonatEmail.Text;
            _connectionToClientBackend.SendRequest("register", $"{name}|{password}|{adresa}|{telefon}|{email}");
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de căutare parțială
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImprumutCautare_Click(object sender, EventArgs e)
        {
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
            mainView.Show();
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
            panelServiciuReturnare.Enabled = true;
            throw new NotImplementedException("Implementează butonul de validare al împrumutului!");
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de validare a returului
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void buttonRetur_Click(object sender, EventArgs e)
        {
            // Validare retur
            throw new NotImplementedException("Implementează butonul de validare al returului!");
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de căutare a cărților returnate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReturnareCăutare_Click(object sender, EventArgs e)
        {
            // folosind id-ul clientului, returnează în comboBoxReturnare cărțile împrumutate
            labelReturnareCarti.Enabled = true;
            comboBoxReturnare.Enabled = true;
            buttonReturnareValidare.Enabled = true;
        }
    }
}
