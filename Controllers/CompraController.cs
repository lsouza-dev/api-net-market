using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Market.Context;
using Market.Models.DTO.Compra;
using Market.Models.DTO.Erros;
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
            var (compra,mensagem) = _service.Criar(clienteId);
            if(compra == null) return BadRequest(new ErroDTO(mensagem));
            return Created($"api/compras/{compra.Id}",new CompraExibicaoDTO(compra));
        }

        [HttpGet("{id}")]
        public ActionResult ObterCompraPorId(int id){
            var compra = _service.BuscarPorId(id);
            if(compra == null) return NotFound();
            return Ok(new CompraExibicaoDTO(compra));
        }

        [HttpGet]
        public ActionResult Listar(){
            return Ok(_service.Listar());
        }

        [HttpGet("pendentes")]
        public ActionResult ListarPendentes(){
            return Ok(_service.ListarPendentes());
        }

        [HttpGet("pagas")]
        public ActionResult ListarPagas(){
            return Ok(_service.ListarPagas());
        }

        [HttpGet("cliente/{idCliente}")]
        public ActionResult ListarPorClienteId(int idCliente){
            var (compras,sucesso,mensagem) = _service.ListarPorClienteId(idCliente);
            if(!sucesso) return NotFound(new ErroDTO(mensagem));
            return Ok(compras);

        }

        [HttpPost("{id}/finalizar")]
        public ActionResult Finalizar(int id,decimal valor){
            var (resultado,mensagem) = _service.Finalizar(id,valor);
            if(resultado == false) return BadRequest(new {Erro = mensagem});
            return Ok(new ErroDTO(mensagem));
        }

        [HttpPost("{id}/adicionar-produto/{produtoId}/quantidade/{quantidade}")]
        public ActionResult AdicionarProduto(int id,int produtoId,int quantidade){
            var  (sucesso,mensagem,compra) = _service.AdicionarCompraProduto(id,produtoId,quantidade);
            if(!sucesso) return BadRequest(new ErroDTO(mensagem));
            return Ok(new CompraExibicaoDTO(compra));
        }
        
    }
}