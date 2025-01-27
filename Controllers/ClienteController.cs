using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Market.Context;
using Market.Models.DTO.Cliente;
using Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly MarketContext _context;
        private ClienteService _service;
            
        public ClienteController(MarketContext context,ClienteService service){
            _context = context;
            _service = service;
        }

        [HttpPost]
        public ActionResult Criar(ClienteCriacaoDTO dto){
            var cliente = _service.Criar(dto);
            return Created($"api/clientes/{cliente.Id}",new ClienteExibicaoDTO(cliente));
        }

        [HttpGet]
        public ActionResult Listar(){
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public ActionResult BuscarPorId(int id){
            var cliente = _service.BuscarPorId(id);
            if(cliente == null) return NotFound();
            return Ok(new ClienteExibicaoDTO(cliente));
        }

        [HttpPut("{id}")]
        public ActionResult Editar(int id,ClienteEdicaoDTO dto){
            var cliente = _service.Editar(id,dto);
            if(cliente == null) return NotFound();
            return Ok(new ClienteExibicaoDTO(cliente));
        }

        [HttpPut("{id}/depositar")]
        public ActionResult Depositar(int id, decimal valor){
            var depositoBemSucedido = _service.Depositar(id,valor);
            if(!depositoBemSucedido) return BadRequest("Houve um erro ao fazer o depósito");
            return Ok(new {Message= "Depósito bem sucedido!"});
        }
    }
}