/*************************************************************************
 *                                                                       *
 *  File:        MainView.cs                                             *
 *  Copyright:   (c) 2025, A. Denisa                                     *
 *                                                                       *
 *  Description: Prima și principala pagină de interfață cu utilizatorul.*
 *               Aplicație de gestionare a unei biblioteci               *
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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    /// <summary>
    public partial class MainView : Form
    {
        private ConnectionToClientBackend _connectionToClientBackend;
        /// <summary>
        public MainView()
        {
            InitializeComponent();
            _connectionToClientBackend = new ConnectionToClientBackend(true);
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

            if (rol != string.Empty)
            {
                _connectionToClientBackend.SendRequest("login", $"{rol}|{username}|{password}\n");
                string response = _connectionToClientBackend.ReceiveResponse();
                if (response == "Login successful")
                {
                    if (rol == "Bibliotecar")
                    {
                        //MessageBox.Show("Autentificare reușită ca Bibliotecar!");
                        // Deschideți fereastra corespunzătoare pentru bibliotecar
                        Form form2 = new BibliotecarView();
                        form2.Show();

                        form2.BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Autentificare reușită ca Bibliotecar!");
                        });

                        this.Hide();

                        form2.FormClosed += (s, args) => this.Close();

                    }
                    else if (rol == "Administrator")
                    {
                        //MessageBox.Show("Autentificare reușită ca Administrator!");
                        // Deschideți fereastra corespunzătoare pentru administrator
                        Form form3 = new AdminView();
                        form3.Show();

                        form3.BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Autentificare reușită ca Administrator!");
                        });

                        form3.FormClosed += (s, args) => this.Close();

                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show($"Autentificare eșuată.{response}");
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să selectați un rol.");
                return;
            }
        }
    }
}
