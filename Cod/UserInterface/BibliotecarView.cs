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
    public partial class BibliotecarView : Form
    {
        public BibliotecarView()
        {
            InitializeComponent();
        }

        private void buttonBibliotecarLogin_Click(object sender, EventArgs e)
        {
            groupBoxServicii.Enabled = true;
        }

        private void buttonAngajatRegister_Click(object sender, EventArgs e)
        {
            groupBoxServicii.Enabled = true;
        }

        private void buttonImprumutCautare_Click(object sender, EventArgs e)
        {
            labelImprumutSugestii.Enabled = true;
            comboBoxSugestii.Enabled = true;
            buttonImprumutValidare.Enabled = true;

        }

        private void buttonDelogare_Click(object sender, EventArgs e)
        {
            Form mainView = new MainView();
            mainView.Show();
            this.Hide();
            this.Controls.Clear();
        }

        private void buttonImprumutValidare_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Implementează butonul de validare al împrumutului!");
        }

        private void buttonRetur_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Implementează butonul de validare al returului!");
        }
    }
}
