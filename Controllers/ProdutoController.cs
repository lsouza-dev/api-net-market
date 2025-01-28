using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Context;
using Market.Models.DTO.Erros;
using Market.Models.DTO.Produto;
using Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly MarketContext _context;
        private ProdutoService _service;

        public ProdutoController(MarketContext context, ProdutoService service){
            _context = context;
            _service = service;
        }

        
        [HttpGet]
        [Route("listar")]
        public ActionResult Listar(){
            var produtos = _service.Listar();
            return Ok(produtos);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult BuscarPorId(int id){
            var produto = _service.BuscarPorId(id);
            if(produto == null) return NotFound();
            return Ok(new ProdutoExibicaoDTO(produto));
        }

        [HttpPost]
        public ActionResult Cadastrar(ProdutoCriacaoDTO dto){
            var produto = _service.Criar(dto);
            return Created($"api/produtos/{produto.Id}", new ProdutoExibicaoDTO(produto));
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Editar(int id, ProdutoEdicaoDTO edicaoDTO){
            var produto = _service.Editar(id,edicaoDTO);
            return Ok(new ProdutoExibicaoDTO(produto));
        }

        [HttpPut]
        [Route("{id}/repor-quantidade/{quantidade}")]
        public ActionResult Editar(int id, int quantidade){
            var (mensagem,sucesso,produto) = _service.ReporQuantidade(id,quantidade);
            if(!sucesso) return BadRequest(new ErroDTO(mensagem));
            return Ok(new ProdutoExibicaoDTO(produto));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Excluir(int id){
            var excluido = _service.Excluir(id);
            if(!excluido) return NotFound();
            return NoContent();
        }

        
        
    }
}