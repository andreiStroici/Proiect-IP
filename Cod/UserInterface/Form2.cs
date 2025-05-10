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
    public partial class Form2 : Form
    {
        public Form2()
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
            labelImprumutPerioada.Enabled = true;
            textBoxImprumutPerioada.Enabled = true;

        }

        private void buttonDelogare_Click(object sender, EventArgs e)
        {
            Form form1 = new Biblioteca();
            form1.Show();
            this.Hide();
            this.Controls.Clear();
        }
    }
}
