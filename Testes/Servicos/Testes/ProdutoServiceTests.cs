using Dominio.Contratos;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Testes.Base;
using Testes.Servicos.Fixtures;
using Xunit;

namespace Testes.Servicos.Testes
{
    public class ProdutoServiceTests : IClassFixtureBase<ProtutoServiceFixture>
    {
        ProtutoServiceFixture _fixture;
        public ProdutoServiceTests(ProtutoServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory(DisplayName = "Adicionar produto")]
        [Trait("Categoria", "Produto Service")]
        [InlineData("Produto 01", 199.90)]
        [InlineData("Produto 02", 19.90)]
        [InlineData("Produto 03", 0.90)]
        public async Task AdicionarProduto_NovoProduto_DeveRetornarOProdutoAdicionado(string nome, decimal preco)
        {
            // Act
            var produto = await _fixture.Service.AddAsync(nome, preco);

            // Assert
            produto.Nome.Should().Be(nome);
            produto.Preco.Should().Be(preco);
            _fixture.Mocker.GetMock<IProdutoRepository>().Verify(x => x.AddAsync(produto), Times.Once, "Executou a adição no repositorio incorretamente");

        }

        [Fact(DisplayName = "Remover produto")]
        [Trait("Categoria", "Produto Service")]
        public async Task RemoverProduto_ProdutoExistente_DeveSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _fixture.Mocker.GetMock<IProdutoRepository>().Setup(x => x.RemoveAsync(id)).Returns(() => Task.FromResult(true));

            // Act
            var resultado = await _fixture.Service.RemoveAsync(id);

            // Assert
            resultado.Should().BeTrue("Deve remover com sucesso");
            _fixture.Mocker.GetMock<IProdutoRepository>().Verify(x => x.RemoveAsync(id), Times.Once, "Executou a remoção no repositorio incorretamente");
        }
    }
}
