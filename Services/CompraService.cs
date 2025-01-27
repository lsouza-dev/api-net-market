using System;
using System.Collections.Generic;
using System.Linq;
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


        public CompraService(MarketContext context,ClienteService service){
            _context = context;
            _clienteService = service;
        }

        public Compra Criar(int clienteId){
            var cliente = _clienteService.BuscarPorId(clienteId);
            if(cliente == null) return null;
            var compra = new Compra(cliente);
            _context.Compras.Add(compra);
            _context.SaveChanges();
            return compra;
        }

        public Compra ObterPorId(int id)
        {
            return _context.Compras.Where(c => c.Id == id).Include(c => c.Cliente).FirstOrDefault();
        }

        public List<CompraExibicaoDTO> Listar(){
            var compras = _context.Compras.Include(c => c.Cliente).ToList();
            var dtos = compras.Select(c => new CompraExibicaoDTO(c)).ToList();
            return dtos;
        }

        public (bool, string) Finalizar(int id, decimal valor)
        {
            var compra = ObterPorId(id);
            if(compra == null) return (false,"Não existe uma compra com o ID informado.");
            var cliente = _clienteService.BuscarPorId(compra.ClienteId);
            var compraFinalizada = compra.Finalizar(cliente.Saldo);
            if(!compraFinalizada) return (false,$"Seu saldo é insuficiente para finalizar a compra. Saldo: {cliente.Saldo}");
            cliente.Saldo -= compra.ValorTotal;
            _context.Clientes.Update(cliente);
            _context.Compras.Update(compra);
            _context.SaveChanges();
            return (true,"Compra finalizada com sucesso.");
        }
    }
}