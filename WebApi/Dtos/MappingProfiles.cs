﻿using AutoMapper;
using Core.Entities;
using Core.Entities.OrdenCompra;

namespace WebApi.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Producto, ProductoDto>()
                .ForMember( p => p.CategoriaNombre, x => x.MapFrom(a => a.Categoria.Nombre))
                .ForMember(p => p.MarcaNombre, x => x.MapFrom(a => a.Marca.Nombre));

            CreateMap<Core.Entities.Direccion, DireccionDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            CreateMap<DireccionDto, Core.Entities.OrdenCompra.Direccion>();

            CreateMap<OrdenCompras, OrdenCompraResponseDto>()
                .ForMember(o => o.TipoEnvio, x => x.MapFrom(y => y.TipoEnvio.Nombre))//Aquí se indica que la propiedad tipo de envio que es un string va a setearse desde el valor tipo envio en su propiedad nombre.
                .ForMember(o => o.TipoEnvioPrecio, x => x.MapFrom(y => y.TipoEnvio.Precio));

            CreateMap<OrdenItem, OrdenItemResponseDto>()
                .ForMember(o => o.ProductoId, x => x.MapFrom(y => y.ItemOrdenado.ProductoItemId))//Setear el id del producto
                .ForMember(o => o.ProductoNombre, x => x.MapFrom(y => y.ItemOrdenado.ProductoNombre))//Setear el Nombre del producto
                .ForMember(o => o.ProductoImagen, x => x.MapFrom(y => y.ItemOrdenado.ImagenUrl));//Setear imagen del producto
        }
    }
}
