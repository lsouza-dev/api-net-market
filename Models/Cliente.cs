using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Market.Models.DTO.Cliente;

namespace Market.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(11)]
        public string Cpf { get; set; }
        [Required]
        [MaxLength(80)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Idade { get; set; }
        public decimal Saldo {get;set;}

        public ICollection<Compra> Compras { get; set; }

        public Cliente(){}
        public Cliente(ClienteCriacaoDTO dto)
        {
            this.Nome = dto.Nome;
            this.Cpf = dto.Cpf;
            this.Email = dto.Email;
            this.Idade = dto.Idade;
            this.Saldo = dto.Saldo;
        }

        internal void Editar(ClienteEdicaoDTO dto)
        {
            if(dto.Nome != null) this.Nome = dto.Nome;
            if(dto.Cpf != null) this.Cpf = dto.Cpf;
            if(dto.Email != null) this.Email = dto.Email;
            if(dto.Idade != default(int)) this.Idade = dto.Idade;
        }

        public bool Depositar(decimal valor){
            if(valor <= 0) return false;
            this.Saldo += valor;
            return true;
        }

        public void Pagar(Models.Compra compra){

        }
    }


}