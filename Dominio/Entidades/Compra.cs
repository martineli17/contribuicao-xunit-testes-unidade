using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Entidades
{
    public class Compra
    {
        public DateTime Data { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string Comprador { get; private set; }
        public ICollection<CompraProduto> Produtos { get; private set; }
        public Compra()
        {
            Data = DateTime.Now;
            Produtos = new List<CompraProduto>();
        }

        public Compra DefinirComprador(string nome)
        {
            Comprador = nome;
            return this;
        }

        public Compra DefinirProdutos(ICollection<CompraProduto> produtos)
        {
            Produtos = produtos;
            CalcularValorTotal();
            return this;
        }

        public Compra AdicionarProduto(CompraProduto produto)
        {
            if (Produtos.Any(x => x.Produto.Nome == produto.Produto.Nome))
                Produtos.First(x => x.Produto.Nome == produto.Produto.Nome).AumentarQuantidade(produto.Quantidade);
            else
                Produtos.Add(produto);
            CalcularValorTotal();
            return this;
        }

        public Compra RemoverProduto(CompraProduto produto)
        {
            Produtos.Remove(produto);
            CalcularValorTotal();
            return this;
        }

        public decimal CalcularValorTotal()
        {
            ValorTotal = Produtos.Sum(x => x.CalcularValorTotal());
            return ValorTotal;
        }
    }
}
