﻿using AutoMapper;
using Core.Entities.OrdenCompra;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Authorize]
    public class OrdenCompraController : BaseApiController
    {
        private readonly IOrdenCompraService _ordenCompraService;
        private readonly IMapper _mapper;

        public OrdenCompraController(IOrdenCompraService ordenCompraService, IMapper mapper)
        {
            _ordenCompraService = ordenCompraService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrdenCompraResponseDto>> AddOrdenCompra(OrdenCompraDto ordenCompraDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var direccion = _mapper.Map<DireccionDto, Direccion>(ordenCompraDto.DireccionEnvio);
            var ordenCompra = await _ordenCompraService.AddOrdenCompraAsync(email, ordenCompraDto.TipoEnvio, ordenCompraDto.CarritoCompraId, direccion);

            if (ordenCompra == null) return BadRequest(new CodeErrorResponse(400, "Errores creando la orden de compra."));

            return Ok(_mapper.Map<OrdenCompras, OrdenCompraResponseDto>(ordenCompra));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrdenCompraResponseDto>>> GetOrdenCompras()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var ordenCompras = await _ordenCompraService.GetOrdenComprasByUserEmailAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrdenCompras>, IReadOnlyList<OrdenCompraResponseDto>>(ordenCompras));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCompraResponseDto>> GetOrdenComprasById(int id)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var ordenCompra = await _ordenCompraService.GetOrdenComprasByIdAsync(id, email);

            if(ordenCompra == null) return NotFound(new CodeErrorResponse(404, "No se encontro la orden de compra."));

            //return Ok(ordenCompra);
            return _mapper.Map<OrdenCompras, OrdenCompraResponseDto>(ordenCompra);
        }

        [HttpGet("tipoEnvio")]
        public async Task<ActionResult<IReadOnlyList<TipoEnvio>>> GetTipoEnvios()
        {
            return Ok(await _ordenCompraService.GetTipoEnviosAsync());
        }
    }
}
