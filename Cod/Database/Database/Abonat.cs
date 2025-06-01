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
    /// <summary>
    /// Clasa care incapsuleaza proprietatile unei abonat
    /// </summary>
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

        /// <summary>
        /// getter/setter pentru _idAbonat
        /// </summary>
        public int IdAbonat { get; set; }
        /// <summary>
        /// getter/setter pentru _nume
        /// </summary>
        public string Nume { get; set; }
        /// <summary>
        /// getter/setter pentru _prenume
        /// </summary>
        public string Prenume { get; set; }
        /// <summary>
        /// getter/setter pentru _telefon
        /// </summary>
        public string Telefon { get; set; }
        /// <summary>
        /// getter/setter pentru _email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// getter/setter pentru _adresa
        /// </summary>
        public string Adresa { get; set; }
        /// <summary>
        /// getter/setter pentru _status
        /// </summary>

        public string Status { get; set; }
        /// <summary>
        /// getter/setter pentru _limitaCarti
        /// </summary>

        public int LimitaCarti { get; set; }
        /// <summary>
        /// getter/setter pentru _zileIntarziate
        /// </summary>

        public int ZileIntarziate { get; set; }
        /// <summary>
        /// constructorul clasei
        /// </summary>
        /// <param name="idAbonat"></param>
        /// <param name="nume"></param>
        /// <param name="prenume"></param>
        /// <param name="adresa"></param>
        /// <param name="telefon"></param>
        /// <param name="email"></param>
        /// <param name="limitaCarti"></param>
        /// <param name="status"></param>
        public Abonat(int idAbonat, string nume, string prenume, string adresa, string telefon, string email, int limitaCarti = 5, string status = "fara restrictii")
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

        /// <summary>
        /// constructorul clasei
        /// </summary>
        /// <param name="idAbonat"></param>
        /// <param name="nume"></param>
        /// <param name="prenume"></param>
        /// <param name="adresa"></param>
        /// <param name="telefon"></param>
        /// <param name="email"></param>
        /// <param name="zileIntarziate"></param>
        /// <param name="limitaCarti"></param>
        /// <param name="status"></param>
        public Abonat(int idAbonat, string nume, string prenume, string adresa, string telefon, string email, int zileIntarziate, int limitaCarti = 5, string status = "fara restrictii")
        {
            IdAbonat = idAbonat;
            Nume = nume;
            Prenume = prenume;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
            Status = status;
            LimitaCarti = limitaCarti;
            ZileIntarziate = zileIntarziate;
        }
        /// <summary>
        /// constructorul clasei
        /// </summary>
        /// <param name="nume"></param>
        /// <param name="prenume"></param>
        /// <param name="adresa"></param>
        /// <param name="telefon"></param>
        /// <param name="email"></param>
        /// <param name="limitaCarti"></param>
        /// <param name="status"></param>
        public Abonat(string nume, string prenume, string adresa, string telefon, string email, int limitaCarti = 5, string status = "fara restrictii")
        {
            Nume = nume;
            Prenume = prenume;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
            Status = status;
            LimitaCarti = limitaCarti;
        }
        /// <summary>
        /// constructorul de copiere a clasei
        /// </summary>
        /// <param name="other"></param>
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
        /// <summary>
        /// functie pentru afisarea instantelor clasei
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID: {IdAbonat} Nume: {Nume} Prenume: {Prenume}, Adresa: {Adresa}, Telefon: {Telefon}, Email: {Email}, LimitaCarti: {LimitaCarti} , Status: {Status}, Zile Intarziate: {ZileIntarziate}";
        }
    }
}
