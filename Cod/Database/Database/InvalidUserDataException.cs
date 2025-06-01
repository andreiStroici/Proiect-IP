/*************************************************************************
*                                                                       *
*  File:        InvalidUserDataException.cs                             *
*  Copyright:   (c) 2025, B. Andreea                                    *
*                                                                       *
*  Description:  Clasa de excepție apelată când                         *
*                datele utilizatorului sunt invalide                    *
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
    /// Excepție aruncată atunci când datele furnizate pentru autentificare ale unui utilizator sunt invalide
    /// </summary>
    public class InvalidUserDataException : Exception
    {
        /// <summary>
        /// Inițializează o nouă instanță a clasei <c>InvalidUserDataException</c> fără mesaj.
        /// </summary>
        public InvalidUserDataException() { }

        /// <summary>
        /// Inițializează o nouă instanță a clasei <c>InvalidUserDataException</c> cu un mesaj specific care descrie motivul erorii.
        /// </summary>
        /// <param name="message">Mesajul descriptiv al excepției.</param>
        public InvalidUserDataException(string message) : base(message) { }

    }

}