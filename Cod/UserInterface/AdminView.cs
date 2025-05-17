

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
        public AdminView()
        {
            InitializeComponent();
        }

        private void buttonGestiuneCautare_Click(object sender, EventArgs e)
        {
            this.textBoxGestiuneDate.Enabled = true;
            this.buttonGestiuneValidare.Enabled = true;
            this.radioButtonGestiuneA.Enabled = true;
            this.radioButtonGestiuneB.Enabled = true;
            this.radioButtonGestiuneE.Enabled = true;
        }

        private void buttonDelogare_Click(object sender, EventArgs e)
        { 
            Form form1 = new MainView();
            form1.Show();
            this.Hide();
            this.Controls.Clear();
        }

        private void radioButtonGestiuneAbonați_CheckedChanged(object sender, EventArgs e)
        {
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

        private void buttonAfișareAbonațiCăutare_Click(object sender, EventArgs e)
        {
            this.buttonGestiuneValidare.Enabled = true;
            this.radioButtonGestiuneA.Enabled = true;
            this.radioButtonGestiuneB.Enabled = true;
            this.radioButtonGestiuneE.Enabled = true;
        }

        private void radioButtonAfișareAbonați_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonAfișareAbonați.Checked)
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
    }
}
