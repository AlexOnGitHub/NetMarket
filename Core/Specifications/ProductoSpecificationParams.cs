using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductoSpecificationParams
    {
        public int? Marca {  get; set; }

        public int? Categoria { get; set; }

        public string Sort {  get; set; }

        public int PageIndex { get; set; } = 1;

        private const int MaxPageSize = 50;

        private int _pageSize = 3;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value>MaxPageSize) ? MaxPageSize : value; //La logica indica que si el valor que ingresa el cliente referente a la cantidad de datos que quiere visualizar es mayor al maximo establecido devolvera solo esta cantidad de datos.
        }

        public string Search {  get; set; }
    }
}
