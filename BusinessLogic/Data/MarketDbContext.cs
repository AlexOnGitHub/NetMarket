﻿using Core.Entities;
using Core.Entities.OrdenCompra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    
    //MarketDbContext:
    //La creación de esta clase es para definir la instancia de mis objetos EntityFramework al interior de mi proyecto.
        
    public class MarketDbContext : DbContext
    {

        public MarketDbContext(DbContextOptions<MarketDbContext> options): base(options) { } //Creación de un contructor, el cual esta esperando la cadena de conexión
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<OrdenCompras>  OrdenCompras { get; set; }
        public DbSet<OrdenItem> OrdenItems { get; set; }
        public DbSet<TipoEnvio> TipoEnvios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
