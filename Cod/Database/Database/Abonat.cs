/*************************************************************************
*                                                                       *
*  File:        Abonat.cs                                               *
*  Copyright:   (c) 2025, B. Andreea                                    *
*                                                                       *
*  Description: Prezinta atributele si metodele clasei Abonat           *
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
    public class Abonat
    {
        private int _idAbonat;
        private string _nume;
        private string _prenume;
        private string _adresa;
        private string _telefon;
        private string _email;
        private string _status;  // blocat (nu poate imprumuta nimic) / cu restrictii (nu imprumuta acasa) / fara restrictii (cat ii permite limita)
        private int _limitaCarti;
        private int _zileIntarziate;

        public int IdAbonat { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public string Adresa { get; set; }

        public string Status { get; set; }

        public int LimitaCarti { get; set; }

        public int ZileIntarziate { get; set; }

        public Abonat(int idAbonat, string nume, string prenume, string adresa, string telefon, string email, int limitaCarti = 5, string status = "fara restrictii", int zileIntarziate = 0)
        {
            IdAbonat = idAbonat;
            Nume = nume;
            Prenume = prenume;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
            Status = status;
            LimitaCarti = limitaCarti;
        }

        public Abonat(string nume, string prenume, string adresa, string telefon, string email, int limitaCarti = 5, string status = "fara restrictii", int zileIntarziate = 0)
        {
            Nume = nume;
            Prenume = prenume;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
            Status = status;
            LimitaCarti = limitaCarti;
        }

        public Abonat(Abonat other) 
        {
            IdAbonat = other.IdAbonat;
            Nume = other.Nume;
            Prenume = other.Prenume;
            Adresa = other.Adresa;
            Telefon = other.Telefon;
            Email = other.Email;
            LimitaCarti = other.LimitaCarti;
            Status = other.Status;
        }

        public override string ToString()
        {
            return $"ID: {IdAbonat} Nume: {Nume} Prenume: {Prenume}, Adresa: {Adresa}, Telefon: {Telefon}, Email: {Email}, LimitaCarti: {LimitaCarti} , Status: {Status}, Zile Intarziate: {ZileIntarziate}";
        }
    }
}
