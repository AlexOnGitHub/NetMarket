﻿using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class CarritoCompraRepository : ICarritoCompraRepository
    {
        private readonly IDatabase _database;

        public CarritoCompraRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteCarritoCompraAsync(string carritoId)
        {
            return await _database.KeyDeleteAsync(carritoId);
        }

        public async Task<CarritoCompra> GetCarritoCompraAsync(string carritoId)
        {
            var data = await _database.StringGetAsync(carritoId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CarritoCompra>(data);
        }

        public async Task<CarritoCompra> UpdateCarritoCompraAsync(CarritoCompra carritoCompra)
        {
            var result = await _database.StringSetAsync(carritoCompra.Id, JsonSerializer.Serialize(carritoCompra), TimeSpan.FromDays(30));

            if(!result) return null;

            return await GetCarritoCompraAsync(carritoCompra.Id);
        }
    }
}
