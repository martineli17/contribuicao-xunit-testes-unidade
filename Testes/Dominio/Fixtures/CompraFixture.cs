using Bogus;
using Dominio.Entidades;
using System.Collections.Generic;

namespace Testes.Dominio.Fixtures
{
    public class CompraFixture
    {
        public Compra CriarCompra() => new Compra();

        public CompraProduto CriarCompraProduto(string nomeProduto = null, double quantidade = 0)
        {
            var faker = new Faker();
            var produto = new Produto()
                .DefinirPreco(faker.Random.Decimal())
                .DefinirNome(nomeProduto ?? faker.Random.String(20));
            return new CompraProduto()
                .DefinirQuantidade(quantidade == 0 ? faker.Random.Double() : quantidade)
                .DefinirProduto(produto);
        }

        public List<CompraProduto> CriarListaProdutos(params CompraProduto[] compraProdutos)
        {
            var listaProdutos = new List<CompraProduto>();
            listaProdutos.AddRange(compraProdutos);
            return listaProdutos;
        }
    }
}
