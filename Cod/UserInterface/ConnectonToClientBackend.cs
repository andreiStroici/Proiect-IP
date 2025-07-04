﻿/*************************************************************************
 *                                                                       *
 *  File:        ConnectonToClientBackend.cs                             *
 *  Copyright:   (c) 2025, S. Andrei                                     *
 *                                                                       *
 *  Description: Conexiunea cu Backend-ul.                               *
 *                                                                       *
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
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace UserInterface
{
    /// <summary>
    /// This class will be used to create a proxy user for the system.
    /// </summary>
    public class ConnectionToClientBackend
    {
        /// <summary>
        /// this attribute will keep the content of the proxy user
        /// </summary>
        private string _content;

        /// <summary>
        /// this attribute will keep the operation of the proxy user
        /// </summary>
        private string _operation;

        /// <summary>
        /// TCP client for the proxy user
        /// </summary>
        private TcpClient _client;

        /// <summary>
        /// stream for the proxy user
        /// </summary>
        private NetworkStream _stream;

        /// <summary>
        /// constructor for the ProxyUser class
        /// </summary>
        public ConnectionToClientBackend(bool delay)
        {
            // conect to the client backend
            try
            {
                if (delay)
                {
                    System.Threading.Thread.Sleep(26000); // wait for the client backend to start
                }
                _client = new TcpClient("127.0.0.1", 8081); // IP-ul și portul serverului
                _stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la conectarea la server: " + ex.Message);
            }
        }

        /// <summary>
        /// this method will send the request to the client backend
        /// </summary>
        public void SendRequest(string operation, string content)
        {
            try
            {
                this._content = content;
                this._operation = operation;
                // send the request to the client backend
                byte[] data = Encoding.UTF8.GetBytes(_operation + "|" + _content);
                _stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eroare la trimiterea cererii: " + ex.Message);
            }
        }

        /// <summary>
        /// receive a message from backedn
        /// </summary>
        public string ReceiveResponse()
        {
            using (var reader = new StreamReader(_stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true))
            {
                return reader.ReadLine(); // blocking until \n
            }
        }

        /// <summary>
        /// close the connection with backend
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                _stream.Close();
                _client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eroare la închiderea conexiunii: " + ex.Message);
            }
        }
    }
}
