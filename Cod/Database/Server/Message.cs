/*************************************************************************
 *                                                                       *
 *  File:        Message.cs                                               *
 *  Copyright:   (c) 2025, S. Andrei                                     *
 *                                                                       *
 *  Description: Represents a message structure for communication between*
 *                                               the server and clients..*
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
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Represents a message structure for communication between the server and clients.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Getter/Setter
        /// </summary>
        public string operation { get; set; }
        /// <summary>
        /// Getter/Setter
        /// </summary>
        public List<Dictionary<string, string>> data { get; set; }

    }
}
