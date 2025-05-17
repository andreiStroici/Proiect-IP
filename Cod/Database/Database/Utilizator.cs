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
        private string rol;

        public string Nume { get; set; }
        public string HashParola { get; set;  }
        public string Rol { get; set; }

        public Utilizator(string nume, string hashParola, string rol)
        {
            Nume = nume;
            HashParola = hashParola;
            Rol = rol;
         
        }
    }

}
