/*************************************************************************
*                                                                       *
*  File:        Database.cs                                             *
*  Copyright:   (c) 2025, B. Andreea                                    *
*                                                                       *
*  Description:Clasa Utilizator care reprezinta angajatul din biblioteca*
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
    public class Utilizator
    {
        private string _nume;
        private string _hashParola;
        private string _rol;

        /// <summary>
        /// getter/setter pentru _nume
        /// </summary>
        public string Nume { get; set; }
        /// <summary>
        /// getter/setter pentru _hashParola
        /// </summary>
        public string HashParola { get; set;  }
        /// <summary>
        /// getter/setter pentru _rol
        /// </summary>
        public string Rol { get; set; }

        /// <summary>
        /// constructorul clasei
        /// </summary>
        /// <param name="nume"></param>
        /// <param name="hashParola"></param>
        /// <param name="rol"></param>
        public Utilizator(string nume, string hashParola, string rol)
        {
            Nume = nume;
            HashParola = Cryptography.Encrypt(hashParola, "secret");
            Rol = rol;
         
        }
    }

}
