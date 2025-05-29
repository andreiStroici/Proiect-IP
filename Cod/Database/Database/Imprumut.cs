/*************************************************************************
*                                                                       *
*  File:        Imprumut.cs                                             *
*  Copyright:   (c) 2025, B. Andreea                                    *
*                                                                       *
*  Description: Prezinta atributele si metodele clasei Imprumut         *
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
    public class Imprumut
    {
        private int _idImprumut;
        private DateTime _dataImprumut;
        private DateTime _dataRestituire;
        private Carte _carte;
        private Abonat _abonat;

        /// <summary>
        /// getter/setter pentru _idImprumut
        /// </summary>
        public int IdImprumut {get; set;}
        /// <summary>
        /// getter/setter pentru _dataImprumut
        /// </summary>
        public DateTime DataImprumut { get; set; }
        /// <summary>
        /// getter/setter pentru _dataRestituire
        /// </summary>
        public DateTime? DataRestituire { get; set; }
        /// <summary>
        /// getter/setter pentru _carte
        /// </summary>
        public Carte Carte { get; set; }
        /// <summary>
        /// getter/setter pentru _abonat
        /// </summary>
        public Abonat Abonat { get; set; }

        /// <summary>
        /// constructorul clasei
        /// </summary>
        /// <param name="idImprumut"></param>
        /// <param name="dataImprumut"></param>
        /// <param name="carte"></param>
        /// <param name="abonat"></param>
        /// <param name="dataRestituire"></param>
        public Imprumut(int idImprumut, DateTime dataImprumut, Carte carte, Abonat abonat, DateTime? dataRestituire = null)
        {
            IdImprumut = idImprumut;
            DataImprumut = dataImprumut;
            DataRestituire = dataRestituire;
            Carte = new Carte(carte);
            Abonat = new Abonat(abonat);
        }

        /// <summary>
        /// functie pentru afisare obiectelor instantiate
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID: {IdImprumut} DataImprumut: {DataImprumut} IdCarte: {Carte.IdCarte}, IdAbonat: {Abonat.IdAbonat}";
        }
    }
}
