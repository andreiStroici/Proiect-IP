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
    public partial class Biblioteca : Form
    {
        public Biblioteca()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Verificare autentificare
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string rol;

            if (radioButtonAdministrator.Checked)
            {
                rol = "Administrator";
            }
            else if (radioButtonBibliotecar.Checked)
            {
                rol = "Bibliotecar";
            }
            else
            {
                rol = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vă rugăm să introduceți corect datele de autentificare.");
                return;
            }

            if (rol == "Bibliotecar")
            {
                //MessageBox.Show("Autentificare reușită ca Bibliotecar!");
                // Deschideți fereastra corespunzătoare pentru bibliotecar
                Form form2 = new Form2();
                form2.Show();

                form2.BeginInvoke((MethodInvoker)delegate {
                    MessageBox.Show("Autentificare reușită ca Bibliotecar!");
                });

                this.Hide();

                form2.FormClosed += (s, args) => this.Close();

            }
            else if (rol == "Administrator")
            {
                //MessageBox.Show("Autentificare reușită ca Administrator!");
                // Deschideți fereastra corespunzătoare pentru administrator
                Form form3 = new Form3();
                form3.Show();

                form3.BeginInvoke((MethodInvoker)delegate {
                    MessageBox.Show("Autentificare reușită ca Administrator!");
                });

                form3.FormClosed += (s, args) => this.Close();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Vă rugăm să selectați un rol.");
                return;
            }
        }
    }
}
