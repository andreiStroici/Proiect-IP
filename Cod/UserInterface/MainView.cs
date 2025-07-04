﻿/*************************************************************************
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
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    /// <summary>
    /// /// Interacțiunea principală a utilizatorului
    /// </summary>
    public partial class MainView : Form
    {
        private readonly ConnectionToClientBackend _connectionToClientBackend;
        /// <summary>
        /// Constructorul pentru MainView
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            _connectionToClientBackend = new ConnectionToClientBackend(true);
        }

        /// <summary>
        /// Acest eveniment se declanșează atunci când utilizatorul face clic pe butonul de autentificare
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            // Verificare autentificare
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string rol;
            textBoxUsername.Clear();
            textBoxPassword.Clear();

            if (radioButtonAdministrator.Checked)
            {
                rol = "administrator";
                radioButtonAdministrator.Checked = false;
            }
            else if (radioButtonBibliotecar.Checked)
            {
                rol = "bibliotecar";
                radioButtonBibliotecar.Checked = false;
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
                Console.WriteLine("response:" + response);

                if (response == "Login successful")
                {
                    if (rol == "bibliotecar")
                    {

                        // Deschideți fereastra corespunzătoare pentru bibliotecar
                        Form formBibliotecar = new BibliotecarView(this);
                        formBibliotecar.Show();

                        formBibliotecar.BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Autentificare reușită ca Bibliotecar!");
                        });

                        formBibliotecar.FormClosed += (s, args) => this.Close();
                        this.Hide();
                    }
                    else if (rol == "administrator")
                    {
                        // Deschideți fereastra corespunzătoare pentru administrator
                        Form formAdmin = new AdminView(this);
                        formAdmin.Show();

                        formAdmin.BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Autentificare reușită ca Administrator!");
                        });

                        formAdmin.FormClosed += (s, args) => this.Close();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show($"Autentificare eșuată: {response}");
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
