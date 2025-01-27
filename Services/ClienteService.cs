using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Market.Context;
using Market.Models;
using Market.Models.DTO.Cliente;

namespace Market.Services
{
    public class ClienteService
    {
        private readonly MarketContext _context;
        public ClienteService(MarketContext context){
            _context = context;
        }

        public Cliente Criar(ClienteCriacaoDTO dto)
        {
            var cliente = new Cliente(dto);
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public List<ClienteExibicaoDTO> Listar(){
            var clientes = _context.Clientes.ToList();
            var dtos = clientes.Select(c => new ClienteExibicaoDTO(c)).ToList();
            return dtos;
        }

        public Cliente BuscarPorId(int id){
            return _context.Clientes.Where(c => c.Id == id).FirstOrDefault();
        }

        public Cliente Editar(int id,ClienteEdicaoDTO dto){
            var clienteDb = BuscarPorId(id);
            if(clienteDb == null) return null;
            clienteDb.Editar(dto);
            _context.Clientes.Update(clienteDb);
            _context.SaveChanges();
            return clienteDb;
        }

        public bool Depositar(int id, decimal valor)
        {
             var clienteDb = BuscarPorId(id);
            if(clienteDb == null) return false;
            clienteDb.Depositar(valor);
            _context.Update(clienteDb);
            _context.SaveChanges();
            return true;
        }
    }
}