/*************************************************************************
*                                                                       *
*  File:        Carte.cs                                                *
*  Copyright:   (c) 2025, B. Andreea                                    *
*                                                                       *
*  Description: Prezinta atributele si metodele clasei Carte            *
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
    public class Carte
    {
        private int _idCarte;
        private string _isbn;
        private string _titlu;
        private string _autor;
        private string _gen;
        private string _editura;
        private string _statusDisponibilitate;

        /// <summary>
        /// getter/setter _idCarte
        /// </summary>
        public int IdCarte { get; set; }
        /// <summary>
        /// getter/setter _isbn
        /// </summary>
        public string Isbn {  get; set; }
        /// <summary>
        /// getter/setter _titlu
        /// </summary>
        public string Titlu { get; set; }
        /// <summary>
        /// getter/setter _autor
        /// </summary>
        public string Autor { get; set; }
        /// <summary>
        /// getter/setter _gen
        /// </summary>
        public string Gen { get; set; }
        /// <summary>
        /// getter/setter _editura
        /// </summary>
        public string Editura { get; set; }
        /// <summary>
        /// getter/setter _statusDisponibilitate
        /// </summary>

        public string StatusDisponibilitate { get; set; }

        /// <summary>
        /// constructorul clasei
        /// </summary>
        /// <param name="idCarte"></param>
        /// <param name="isbn"></param>
        /// <param name="titlu"></param>
        /// <param name="autor"></param>
        /// <param name="gen"></param>
        /// <param name="editura"></param>
        /// <param name="statusDisponibilitate"></param>
        public Carte(int idCarte, string isbn, string titlu, string autor, string gen, string editura, string statusDisponibilitate = "disponibil")
        {
            IdCarte = idCarte;
            Isbn = isbn;
            Titlu = titlu;
            Autor = autor;
            Gen = gen;
            Editura = editura;
            StatusDisponibilitate = statusDisponibilitate;
        }

        /// <summary>
        /// constructorul clasei
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="titlu"></param>
        /// <param name="autor"></param>
        /// <param name="gen"></param>
        /// <param name="editura"></param>
        /// <param name="statusDisponibilitate"></param>
        public Carte(string isbn, string titlu, string autor, string gen, string editura, string statusDisponibilitate = "disponibil")
        {
            Isbn = isbn;
            Titlu = titlu;
            Autor = autor;
            Gen = gen;
            Editura = editura;
            StatusDisponibilitate = statusDisponibilitate;
        }

        /// <summary>
        /// constructorul de copiere a clasei
        /// </summary>
        /// <param name="other"></param>
        public Carte(Carte other)
        {
            IdCarte = other.IdCarte;
            Isbn = other.Isbn;
            Titlu = other.Titlu;
            Autor = other.Autor;
            Gen = other.Gen;
            Editura = other.Editura;
            StatusDisponibilitate = other.StatusDisponibilitate;
        }

        /// <summary>
        /// functie pentru afisarea obiectelor instantiate
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID: {IdCarte} ISBN: { Isbn} Titlu: {Titlu}, Autor: {Autor}, Gen: {Gen}, Editura: {Editura}, Status: {StatusDisponibilitate}";
        }
    }
}
