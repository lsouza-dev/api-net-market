using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Market.Context
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options) : base(options){}


        public DbSet<Produto> Produtos {get; set;}
        public DbSet<Compra> Compras {get;set;}
        public DbSet<Cliente> Clientes {get; set;}
        public DbSet<CompraProduto> CompraProdutos {get; set;}



        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<Cliente>()
            .HasMany(cli => cli.Compras)
            .WithOne(com => com.Cliente)
            .HasForeignKey(com => com.ClienteId);

            modelBuilder.Entity<CompraProduto>()
            .HasKey(cp => cp.Id);

            modelBuilder.Entity<CompraProduto>()
            .HasOne(cp => cp.Compra)
            .WithMany(c => c.ComprasProdutos)
            .HasForeignKey(cp => cp.CompraId);

            modelBuilder.Entity<CompraProduto>()
            .HasOne(cp => cp.Produto)
            .WithMany(c => c.ComprasProdutos)
            .HasForeignKey(cp => cp.ProdutoId);


            base.OnModelCreating(modelBuilder);
        }
    }
}