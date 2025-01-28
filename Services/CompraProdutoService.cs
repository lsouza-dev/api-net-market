using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Market.Context;
using Market.Models;
using Market.Models.DTO.CompraProduto;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Market.Services
{
    public class CompraProdutoService
    {
        private readonly MarketContext _context;
        private ClienteService _clienteService;
        private CompraService _compraService;

        public CompraProdutoService(MarketContext context, ClienteService clienteService, CompraService compraService)
        {
            _context = context;
            _clienteService = clienteService;
            _compraService = compraService;
        }
        public List<CompraProdutoExibicaoDTO> Listar()
        {
            var comprasProdutos = _context.CompraProdutos.ToList();
            var dtos = comprasProdutos.Select(cp => new CompraProdutoExibicaoDTO(cp)).ToList();
            return dtos;
        }

        public List<CompraProdutoExibicaoDTO> ListarPorProdutoId(int produtoId)
        {
            var comprasProdutos = _context.CompraProdutos.Where(cp => cp.ProdutoId == produtoId).ToList();
            var dtos = comprasProdutos.Select(cp => new CompraProdutoExibicaoDTO(cp)).ToList();
            return dtos;
        }

        public async Task<(List<CompraProdutoExibicaoDTO>, string)> ListarPorClienteIdAsync(int clienteId)
        {
            // Busca o cliente pelo ID
            var cliente =  _clienteService.BuscarPorId(clienteId);
            if (cliente == null)
                return (null, "Cliente não encontrado.");

            // Busca as compras do cliente, incluindo os CompraProdutos e Produto (se necessário)
            var compras = await _context.Compras
                .Include(c => c.ComprasProdutos) // Inclui os produtos comprados
                .ThenInclude(cp => cp.Produto)  // Inclui os detalhes dos produtos, se necessário
                .Where(c => c.ClienteId == clienteId && c.ComprasProdutos.Any()) // Filtra somente as compras com produtos
                .ToListAsync();

            if (!compras.Any())
                return (null, "Nenhuma compra com produtos encontrada para este cliente.");

            // Mapeia os CompraProdutos para os DTOs
            var dtos = compras
                .SelectMany(c => c.ComprasProdutos) // Obtém todos os CompraProdutos das compras
                .Select(cp => new CompraProdutoExibicaoDTO(cp)) // Cria o DTO para cada CompraProduto
                .ToList();

            return (dtos, "Busca realizada com sucesso!");
        }
    }
}