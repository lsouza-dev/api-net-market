using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        
        public ICollection<Compra> Compras {get; set;}
    }
}