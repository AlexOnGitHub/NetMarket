using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CarritoCompraController : BaseApiController
    {
        private readonly ICarritoCompraRepository _carritoCompraRepository;

        public CarritoCompraController(ICarritoCompraRepository carritoCompraRepository)
        {
            _carritoCompraRepository = carritoCompraRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CarritoCompra>> GetCarritoById(string id)
        {
            var carrito = await _carritoCompraRepository.GetCarritoCompraAsync(id);

            return Ok(carrito ?? new CarritoCompra(id));
        }

        [HttpPost]
        public async Task<ActionResult<CarritoCompra>> UpdateCarritoCompra(CarritoCompra carritoParam)
        {
            var carritoUpdated = await _carritoCompraRepository.UpdateCarritoCompraAsync(carritoParam);

            return Ok(carritoUpdated);
        }

        [HttpDelete]
        public async Task DeleteCarritoCompra(string id)
        {
            await _carritoCompraRepository.DeleteCarritoCompraAsync(id);
        }
    }
}
