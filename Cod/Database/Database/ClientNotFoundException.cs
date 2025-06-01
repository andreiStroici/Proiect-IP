/*************************************************************************
*                                                                       *
*  File:        ClientNotFoundException.cs                              *
*  Copyright:   (c) 2025, B. Andreea                                    *
*                                                                       *
*  Description: Clasa de exceptie apelată cand un client nu a fost găsit*
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

namespace Database
{
    /// <summary>
    /// Excepție aruncată atunci când un client nu este găsit în baza de date.
    /// </summary>
    public class ClientNotFoundException : Exception
    {
        /// <summary>
        /// Inițializează o nouă instanță a clasei <c>ClientNotFoundException</c> fără mesaj.
        /// </summary>
        public ClientNotFoundException() { }

        /// <summary>
        /// Inițializează o nouă instanță a clasei <c>ClientNotFoundException</c> cu un mesaj specific.
        /// </summary>
        ///  /// <param name="message">Mesajul descriptiv al excepției.</param>
        public ClientNotFoundException(string message) : base(message) { }

        
    }
}
