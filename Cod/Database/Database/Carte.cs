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

        public int IdCarte { get; set; }
        public string Isbn {  get; set; }
        public string Titlu { get; set; }
        public string Autor { get; set; }
        public string Gen { get; set; }
        public string Editura { get; set; }

        public string StatusDisponibilitate { get; set; }

        public Carte(int idCarte, string isbn, string titlu, string autor, string gen, string editura, string statusDisponibilitate = "disponibila")
        {
            IdCarte = idCarte;
            Isbn = isbn;
            Titlu = titlu;
            Autor = autor;
            Gen = gen;
            Editura = editura;
            StatusDisponibilitate = statusDisponibilitate;
        }

        public Carte(string isbn, string titlu, string autor, string gen, string editura, string statusDisponibilitate = "disponibila")
        {
            Isbn = isbn;
            Titlu = titlu;
            Autor = autor;
            Gen = gen;
            Editura = editura;
            StatusDisponibilitate = statusDisponibilitate;
        }

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

        public override string ToString()
        {
            return $"ID: {IdCarte} ISBN: { Isbn} Titlu: {Titlu}, Autor: {Autor}, Gen: {Gen}, Editura: {Editura}, Status: {StatusDisponibilitate}";
        }
    }
}
