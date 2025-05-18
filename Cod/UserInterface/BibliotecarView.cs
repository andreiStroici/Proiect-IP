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
    public partial class BibliotecarView : Form
    {
        /// <summary>
        public BibliotecarView()
        {
            InitializeComponent();
        }

        private void buttonBibliotecarLogin_Click(object sender, EventArgs e)
        {
            // Activează interfața pentru gestiunea serviciilor de împrumut și retur
            groupBoxServicii.Enabled = true;
            panelServiciuReturnare.Enabled = true;
        }

        private void buttonAngajatRegister_Click(object sender, EventArgs e)
        {
            // Activează interfața pentru gestiunea serviciilor de împrumut și retur
            groupBoxServicii.Enabled = true;
        }

        private void buttonImprumutCautare_Click(object sender, EventArgs e)
        {
            // Activează interfața pentru căutarea cărților disponibile (se efectuează căutări parțiale)
            labelImprumutSugestii.Enabled = true;
            comboBoxSugestii.Enabled = true;
            buttonImprumutValidare.Enabled = true;

        }

        private void buttonDelogare_Click(object sender, EventArgs e)
        {
            // Delogare și revenire la pagina principală
            Form mainView = new MainView();
            mainView.Show();
            this.Hide();
            this.Controls.Clear();
        }

        private void buttonImprumutValidare_Click(object sender, EventArgs e)
        {
            panelServiciuReturnare.Enabled = true;
            throw new NotImplementedException("Implementează butonul de validare al împrumutului!");
        }

        private void buttonRetur_Click(object sender, EventArgs e)
        {
            // Validare retur
            throw new NotImplementedException("Implementează butonul de validare al returului!");
        }

        private void buttonReturnareCăutare_Click(object sender, EventArgs e)
        {
            // folosind id-ul clientului, returnează în comboBoxReturnare cărțile împrumutate
            labelReturnareCarti.Enabled = true;
            comboBoxReturnare.Enabled = true;
            buttonRetur.Enabled = true;
        }
    }
}
