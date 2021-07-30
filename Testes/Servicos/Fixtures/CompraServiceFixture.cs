using Bogus;
using Dominio.Entidades;
using Moq.AutoMock;
using Servico;
using System.Collections.Generic;

namespace Testes.Servicos.Fixtures
{
    public class CompraServiceFixture
    {
        public AutoMocker Mocker;
        public CompraService Service;
        public CompraServiceFixture()
        {
            Mocker = new AutoMocker();
            Service = InstanciarService();
        }

        public CompraService InstanciarService() => Mocker.CreateInstance<CompraService>();

        public ICollection<CompraProduto> CriarCompraProdutoCollection(int quantidade = 1)
        {
            var fakerCompraProduto = new Faker<CompraProduto>();
            var compraProdutos = new List<CompraProduto>();
            fakerCompraProduto.RuleFor(x => x.Quantidade, f => f.Random.Double());
            fakerCompraProduto.RuleFor(x => x.Produto, CriarProduto());
            return fakerCompraProduto.Generate(quantidade);
        }

        public Produto CriarProduto()
        {
            var fakerProduto = new Faker<Produto>();
            var compraProdutos = new List<Produto>();
            fakerProduto.RuleFor(x => x.Preco, f => f.Random.Decimal());
            fakerProduto.RuleFor(x => x.Nome, f => f.Random.String());
            return fakerProduto;
        }
    }
}
