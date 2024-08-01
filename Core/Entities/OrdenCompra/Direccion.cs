using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class Direccion
    {
        public Direccion() { }

        public Direccion(string calle, string ciudad, string departamento, string cp, string pais)
        {
            Calle = calle;
            Ciudad = ciudad;
            Departamento = departamento;
            CP = cp;
            Pais = pais;
        }

        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public string CP { get; set; }
        public string Pais { get; set; }
    }
}
