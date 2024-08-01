using Core.Entities;
using Core.Entities.OrdenCompra;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class MarketDbContextData
    {
        public static async Task CargarDataAsync(MarketDbContext context, ILoggerFactory loggerFactory)
        {
			try
			{
				if (!context.Marca.Any())
				{
					var marcaData = File.ReadAllText("../BusinessLogic/Data/CargarData/marca.json"); //Lectura del archivo json
					var marcas = JsonSerializer.Deserialize<List<Marca>>(marcaData); //Serializarlos en un formato del tipo listas "Marca"

					foreach (var marca in marcas)
					{
						context.Marca.Add(marca);
					}

					await context.SaveChangesAsync(); //Confirmación de la transacción
				}

                if (!context.Categoria.Any())
                {
                    var categoriaData = File.ReadAllText("../BusinessLogic/Data/CargarData/categoria.json"); //Lectura del archivo json
                    var categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriaData); //Serializarlos en un formato del tipo listas "Categoria"

                    foreach (var categoria in categorias)
                    {
                        context.Categoria.Add(categoria);
                    }

                    await context.SaveChangesAsync(); //Confirmación de la transacción
                }

                if (!context.Producto.Any())
                {
                    var productoData = File.ReadAllText("../BusinessLogic/Data/CargarData/producto.json"); //Lectura del archivo json
                    var productos = JsonSerializer.Deserialize<List<Producto>>(productoData); //Serializarlos en un formato del tipo listas "Producto"

                    foreach (var producto in productos)
                    {
                        context.Producto.Add(producto);
                    }

                    await context.SaveChangesAsync(); //Confirmación de la transacción
                }

                if (!context.TipoEnvios.Any())
                {
                    var tipoEnvioData = File.ReadAllText("../BusinessLogic/Data/CargarData/tipoenvio.json"); //Lectura del archivo json
                    var tipoEnvios = JsonSerializer.Deserialize<List<TipoEnvio>>(tipoEnvioData); //Serializarlos en un formato del tipo listas "TipoEnvios"

                    foreach (var tipoenvio in tipoEnvios)
                    {
                        context.TipoEnvios.Add(tipoenvio);
                    }

                    await context.SaveChangesAsync(); //Confirmación de la transacción
                }
            }
			catch (Exception e)
			{
                var logger = loggerFactory.CreateLogger<MarketDbContextData>();
                logger.LogError(e.Message);
				throw;
			}
        }
    }
}
