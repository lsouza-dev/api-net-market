using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Context;
using Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("api/compras-produtos")]
    public class CompraProdutoController : ControllerBase
    {
        private readonly MarketContext _context;
        private CompraProdutoService _service;

        public CompraProdutoController(MarketContext context, CompraProdutoService service)
        {
            _service = service;
            _context = context;
        }


        [HttpGet]
        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("produto/{idProduto}")]
        public ActionResult ListarPorProduto(int idProduto)
        {
            return Ok(_service.ListarPorProdutoId(idProduto));
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetCompraProdutosPorClienteId(int clienteId)
        {
            var (produtos, mensagem) = await _service.ListarPorClienteIdAsync(clienteId);

            if (produtos == null)
                return NotFound(new { mensagem });

            return Ok(produtos);
        }

    }
}