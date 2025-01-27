using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Market.Context;
using Market.Models.DTO.Compra;
using Market.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("api/compras")]
    public class CompraController : ControllerBase
    {
        private readonly MarketContext _context;
        private CompraService _service;

        public CompraController(MarketContext context, CompraService service){
            _service = service;
            _context = context;
        }

        
        [HttpPost("{clienteId}")]
        public ActionResult Criar(int clienteId){
            var compra = _service.Criar(clienteId);
            if(compra == null) return NotFound();
            return Ok(new CompraExibicaoDTO(compra));
        }

        [HttpGet("{id}")]
        public ActionResult ObterCompraPorId(int id){
            var compra = _service.ObterPorId(id);
            if(compra == null) return NotFound();
            return Ok(new CompraExibicaoDTO(compra));
        }

        [HttpGet]
        public ActionResult Listar(){
            return Ok(_service.Listar());
        }

        [HttpPost("{id}/finalizar")]
        public ActionResult Finalizar(int id,decimal valor){
            var (resultado,mensagem) = _service.Finalizar(id,valor);
            if(resultado == false) return BadRequest(new {Erro = mensagem});
            return Ok(new {Mensagem = mensagem});
        }
        
    }
}