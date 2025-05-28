/*************************************************************************
 *                                                                       *
 *  File:        Program.cs                                              *
 *  Copyright:   (c) 2025, S. Andrei                                     *
 *                                                                       *
 *  Description: Programul principal.                                    *
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
using System.Text;
using System.Threading.Tasks;

namespace ClientBackend
{
    internal class Program
    {
        /// <summary>
        /// Main entry point for the application.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Client Backend is running...");
            // Here you can initialize and start your client backend logic
            // For example, you might want to create an instance of ClientBackend and call AcceptConnection
            ClientBackend clientBackend = new ClientBackend();
            clientBackend.AcceptConnection();
        }
    }
}
