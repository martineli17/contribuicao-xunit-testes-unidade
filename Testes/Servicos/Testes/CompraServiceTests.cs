using Dominio.Contratos;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Testes.Base;
using Testes.Servicos.Fixtures;
using Xunit;

namespace Testes.Servicos.Testes
{
    public class CompraServiceTests : IClassFixtureBase<CompraServiceFixture>
    {
        private readonly CompraServiceFixture _fixture;
        public CompraServiceTests(CompraServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory(DisplayName = "Realizar compra")]
        [Trait("Categoria", "Compra service")]
        [InlineData("Comprador 01", 1)]
        [InlineData("Comprador 02", 5)]
        [InlineData("Comprador 03", 17)]
        public async Task RealizarCompra_NovaCompra_DeveRetornarACompra(string comprador, int quantidadeProdutos)
        {
            // Arrange
            var compraProdutos = _fixture.CriarCompraProdutoCollection(quantidadeProdutos);

            // Act
            var compra = await _fixture.Service.AddAsync(compraProdutos, comprador);

            // Assert
            compra.Produtos.Should().BeEquivalentTo(compraProdutos, "Produtos da compra gerada deve ser igual aos repassados");
            _fixture.Mocker.GetMock<ICompraRepository>().Verify(x => x.AddAsync(compra), Times.Once, "Executou a adição no repositorio incorretamente");
        }
    }
}
