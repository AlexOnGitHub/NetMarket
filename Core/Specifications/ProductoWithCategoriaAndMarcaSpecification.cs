using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductoWithCategoriaAndMarcaSpecification : BaseSpecification<Producto>
    {

        public ProductoWithCategoriaAndMarcaSpecification(ProductoSpecificationParams productoParams) 
            : base (x => 
                         (string.IsNullOrEmpty(productoParams.Search) || x.Nombre.Contains(productoParams.Search)) &&
                         (!productoParams.Marca.HasValue || x.MarcaId == productoParams.Marca) &&
                         (!productoParams.Categoria.HasValue || x.CategoriaId == productoParams.Categoria)
                    )
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Marca);

            //ApplyPaging(0, 5); 
            ApplyPaging( productoParams.PageSize * (productoParams.PageIndex-1), productoParams.PageSize);

            if (!string.IsNullOrEmpty(productoParams.Sort))
            {
                switch (productoParams.Sort)
                {
                    case "nombreAsc":
                        AddOrdeBy(p => p.Nombre);
                        break;
                    case "nombreDesc":
                        AddOrdeByDescending(p => p.Nombre);
                        break;
                    case "precioAsc":
                        AddOrdeBy(p => p.Precio);
                        break;
                    case "precioDesc":
                        AddOrdeByDescending(p => p.Precio);
                        break;
                    case "descripcionAsc":
                        AddOrdeBy(p => p.Descripcion);
                        break;
                    case "descripcionDesc":
                        AddOrdeByDescending(p => p.Descripcion);
                        break;
                    case "marcaAsc":
                        AddOrdeBy(p => p.Marca);
                        break;
                    case "marcaDesc":
                        AddOrdeByDescending(p => p.Marca);
                        break;
                    case "categoriaAsc":
                        AddOrdeBy(p => p.Categoria);
                        break;
                    case "categoriaDesc":
                        AddOrdeByDescending(p => p.Categoria);
                        break;
                    default:
                        AddOrdeBy(p => p.Nombre);
                        break;
                }
            }
        }

        public ProductoWithCategoriaAndMarcaSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Marca);
        }

    }
}
