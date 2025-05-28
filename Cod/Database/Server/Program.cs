/*************************************************************************
 *                                                                       *
 *  File:        Program.cs                                              *
 *  Copyright:   (c) 2025, S. Andrei, A. Denisa                          *
 *                                                                       *
 *  Description: Aplicația care face legătura dintre interfață și Server.*
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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Programul principal al aplicației care pornește serverul.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(IPAddress.Any, 12345);
            server.Start();
        }
    }
}
