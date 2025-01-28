using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Market.Context;
using Market.Models;
using Market.Models.DTO.Compra;
using Microsoft.EntityFrameworkCore;

namespace Market.Services
{
    public class CompraService
    {
        private readonly MarketContext _context;
        private ClienteService _clienteService;
        private ProdutoService _produtoService;



        public CompraService(MarketContext context, ProdutoService produtoService, ClienteService clienteService)
        {
            _context = context;
            _produtoService = produtoService;
            _clienteService = clienteService;
        }

        public (Compra, string) Criar(int clienteId)
        {
            var cliente = _clienteService.BuscarPorId(clienteId);
            if (cliente == null) return (null, "Cliente não encontrado.");
            var compraNãoFinalizada = _context.Compras.Where(c => c.ClienteId == cliente.Id && c.Status == StatusCompra.PENDENTE).FirstOrDefault();
            if (compraNãoFinalizada != null) return (null, "O usuário tem uma compra em andamento. Não será possível iniciar outra compra enquanto não finalizar a compra pendente.");
            var compra = new Compra(cliente);
            _context.Compras.Add(compra);
            _context.SaveChanges();
            return (compra, "Compra criada com sucesso!");
        }

        public Compra BuscarPorId(int id)
        {
            return _context.Compras.Where(c => c.Id == id).Include(c => c.Cliente).Include(c => c.ComprasProdutos).FirstOrDefault();
        }

        public List<CompraExibicaoDTO> Listar()
        {
            var compras = _context.Compras.Include(c => c.Cliente).Include(c => c.ComprasProdutos).ToList();
            var dtos = compras.Select(c => new CompraExibicaoDTO(c)).ToList();
            return dtos;
        }

        public (bool, string) Finalizar(int id, decimal valor)
        {
            var compra = BuscarPorId(id);
            if (compra == null) return (false, "Não existe uma compra com o ID informado.");
            var cliente = _clienteService.BuscarPorId(compra.ClienteId);
            var compraFinalizada = compra.Finalizar(cliente.Saldo);
            if (!compraFinalizada) return (false, $"Seu saldo é insuficiente para finalizar a compra. Saldo: {cliente.Saldo}");
            cliente.Saldo -= compra.ValorTotal;
            _context.Clientes.Update(cliente);
            _context.Compras.Update(compra);
            _context.SaveChanges();
            return (true, "Compra finalizada com sucesso.");
        }


        public (bool, string, Compra) AdicionarCompraProduto(int compraId, int produtoId, int quantidade)
        {
            var compra = BuscarPorId(compraId);
            if (compra == null || compra.Status == StatusCompra.PAGO)
                return (false, $"Compra com o Id {compraId} não foi encontrada ou já está paga.", null);

            var produto = _produtoService.BuscarPorId(produtoId);
            if (produto == null)
                return (false, $"Produto com o Id {produtoId} não foi encontrado.", null);

            var (produtoAdicionado, mensagemVenda) = produto.Vender(quantidade);
            if (!produtoAdicionado)
                return (false, mensagemVenda, null);

            // Cria uma nova entrada na tabela CompraProduto
            var compraProduto = new CompraProduto(compra, produto, quantidade);
            _context.CompraProdutos.Add(compraProduto);
            produto.ComprasProdutos.Add(compraProduto);
            compra.ComprasProdutos.Add(compraProduto);
            _context.Compras.Update(compra);
            _context.Produtos.Update(produto);
            _context.SaveChanges();
            // Agora que a Compra e seus Produtos estão salvos, calcula o valor total
            compra.ValorTotal = CalcularValorTotalCompra(compra);

            // Atualiza o valor total e salva novamente, se necessário
            _context.Compras.Update(compra);
            _context.SaveChanges();

            return (true, $"Produto adicionado com sucesso à compra com Id {compra.Id} iniciada por {compra.Cliente.Nome}", compra);
        }



        private decimal CalcularValorTotalCompra(Compra compra)
        {
            var valorTotal = 0m;

            // Garanta que os produtos relacionados estão carregados
            foreach (var compraProduto in compra.ComprasProdutos)
            {
                if (compraProduto.Produto == null)
                {
                    // Carrega o Produto associado se ele estiver nulo
                    compraProduto.Produto = _context.Produtos.FirstOrDefault(p => p.Id == compraProduto.ProdutoId);
                }

                if (compraProduto.Produto != null)
                {
                    valorTotal += compraProduto.Produto.Preco * compraProduto.Quantidade;
                }
            }

            return valorTotal;
        }




        public List<CompraExibicaoDTO> ListarPendentes()
        {
            var compras = _context.Compras.Include(c => c.Cliente).Include(c => c.ComprasProdutos).Where(c => c.Status == StatusCompra.PENDENTE).ToList();
            var dtos = compras.Select(c => new CompraExibicaoDTO(c)).ToList();
            return dtos;
        }

        public List<CompraExibicaoDTO> ListarPagas()
        {
            var compras = _context.Compras.Include(c => c.Cliente).Include(c => c.ComprasProdutos).Where(c => c.Status == StatusCompra.PAGO).ToList();
            var dtos = compras.Select(c => new CompraExibicaoDTO(c)).ToList();
            return dtos;
        }

        public (List<CompraExibicaoDTO>, bool, string) ListarPorClienteId(int idCliente)
        {
            var cliente = _clienteService.BuscarPorId(idCliente);
            if (cliente == null) return (null, false, "Cliente não encontrado.");
            var compras = _context.Compras.Include(c => c.Cliente).Include(c => c.ComprasProdutos).Where(c => c.ClienteId == idCliente).ToList();
            var dtos = compras.Select(c => new CompraExibicaoDTO(c)).ToList();
            return (dtos, true, "Busca realizada com sucesso.");
        }
    }
}