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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    public partial class AdminView : Form
    {
        private ConnectionToClientBackend _connectionToClientBackend;
        /// <summary>
        public AdminView()
        {
            InitializeComponent();
            _connectionToClientBackend = new ConnectionToClientBackend(false);
        }

        private void buttonDelogare_Click(object sender, EventArgs e)
        {
            // Delogare și revenire la pagina principală
            Form form1 = new MainView();
            form1.Show();
            this.Hide();
            this.Controls.Clear();
        }

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

        private void radioButtonAfișareAbonați_CheckedChanged(object sender, EventArgs e)
        {
            // Activăm interfața pentru listarea abonaților problematici
            if (radioButtonAfișareAbonați.Checked)
            {
                labelAfișareAbonațiProbleme.Enabled = true;
                buttonAfișareAbonațiCăutare.Enabled = true;
                comboBoxAbonațiProbleme.Enabled = true;

                labelGestiuneAbonați.Enabled = false;
                textBoxGestiuneTelefon.Enabled = false;
                labelGestiuneAbonațiTelefon.Enabled = false;
                buttonGestiuneCautare.Enabled = false;
                labelGestiuneDate.Enabled = false;
                textBoxGestiuneDate.Enabled = false;
            }
        }

        private void buttonGestiuneCautare_Click(object sender, EventArgs e)
        {
            this.textBoxGestiuneDate.Enabled = true;
            this.buttonGestiuneValidare.Enabled = true;
            this.radioButtonGestiuneA.Enabled = true;
            this.radioButtonGestiuneB.Enabled = true;
            this.radioButtonGestiuneE.Enabled = true;
        }

        private void buttonAfișareAbonațiCăutare_Click(object sender, EventArgs e)
        {
            this.buttonGestiuneValidare.Enabled = true;
            this.radioButtonGestiuneA.Enabled = true;
            this.radioButtonGestiuneB.Enabled = true;
            this.radioButtonGestiuneE.Enabled = true;
        }

        private void buttonGestiuneValidare_Click(object sender, EventArgs e)
        {
            if(radioButtonAfișareAbonați.Checked)
            {
                // Extragem obiectul de tip abonat din lista de abonați problematici
            }
            else if (radioButtonGestiuneAbonați.Checked)
            {
                // Extragem obiectul de tip abonat din cautarea dupa numar de telefon
            }

            // avem un obiect abonat rezultat
            if(radioButtonGestiuneA.Checked)
            {
                // aplicare restricții
            }
            if(radioButtonGestiuneB.Checked)
            {
                // eliminare restricții
            }
            if (radioButtonGestiuneE.Checked)
            {
                // blocare abonat
            }
        }

        private void buttonAngajatRegister_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Implementează butonul de înregistrare al angajatului!");
        }

        private void buttonAdaugaCarte_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Implementează butonul de adăugare a cărții!");
        }

        private void buttonDeleteCarte_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Implementează butonul de ștergere a cărții!");
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Implementează butonul de actualizare a cărții!");
        }

        private void textBoxDeleteIDCarte_Enter(object sender, EventArgs e)
        {
            // acest eveniment se apelează când o celulă nu mai este controlul selectat activ din fereastră
            // listează toate cărțile cu ISBN-ul introdus
            buttonDeleteCarte.Enabled = true;
            comboBoxStergereCarti.Enabled = true;
        }
    }
}
