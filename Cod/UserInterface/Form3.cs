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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void buttonGestiuneCautare_Click(object sender, EventArgs e)
        {
            this.labelGestiuneDate.Enabled = true;
            this.textBoxGestiuneDate.Enabled = true;
            this.buttonGestiuneValidare.Enabled = true;
            this.radioButtonGestiuneA.Enabled = true;
            this.radioButtonGestiuneB.Enabled = true;
            this.radioButtonGestiuneE.Enabled = true;

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
