using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Context;

namespace Market.Services
{
    public class CompraProdutoService
    {
        private readonly MarketContext _context;

        public CompraProdutoService(MarketContext context){
            _context = context;
        }

        
    }
}