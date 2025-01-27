using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models.DTO.Cliente
{
    public record ClienteCriacaoDTO
    {
        public string Nome { get; set; } 
        public string Cpf { get; set; } 
        public string Email { get; set; } 
        public int Idade { get; set; }
        public decimal Saldo { get; set; }
    }
}