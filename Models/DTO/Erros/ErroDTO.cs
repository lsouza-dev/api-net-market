using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models.DTO.Erros
{
    public record ErroDTO
    {
        public string Mensagem { get; set; }

        public ErroDTO(string mensagem)
        {
            this.Mensagem = mensagem;
        }
    }
}