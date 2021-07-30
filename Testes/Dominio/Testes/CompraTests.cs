using FluentAssertions;
using System;
using System.Linq;
using Testes.Base;
using Testes.Dominio.Fixtures;
using Xunit;

namespace Testes.Dominio.Testes
{
    public class CompraTests : IClassFixtureBase<CompraFixture>
    {
        private CompraFixture _compraFixture;

        public CompraTests(CompraFixture compraFixture)
        {
            _compraFixture = compraFixture;
        }

        [Trait("Categoria", "Compra")]
        [Theory(DisplayName = "Definir comprador")]
        [InlineData("Comprador 01")]
        [InlineData("Comprador 02")]
        [InlineData("Comprador 03")]
        public void DefinirComprador_CompradorValido_DeveAlterarOComprador(string comprador)
        {
            //Arrange
            var compra = _compraFixture.CriarCompra();

            // Act
            compra.DefinirComprador(comprador);

            // Assert
            compra.Comprador.Should().Be(comprador, "Comprador deve ser o mesmo informado por parâmetro");
        }

        [Trait("Categoria", "Compra")]
        [Fact(DisplayName = "Valor total")]
        public void CalcularValorTotal_ProdutosInseridos_DeveCalcularOValorTotalDaListaDeProdutos()
        {
            //Arrange
            var compra = _compraFixture.CriarCompra();
            var produto1 = _compraFixture.CriarCompraProduto();
            var produto2 = _compraFixture.CriarCompraProduto();
            var produto3 = _compraFixture.CriarCompraProduto();
            var listaProdutos = _compraFixture.CriarListaProdutos(produto1);
            compra.DefinirProdutos(listaProdutos);

            // Assert
            compra.ValorTotal.Should().Be(compra.Produtos.Sum(x => x.CalcularValorTotal()), "Valor total da compra deve ser igual a soma dos valores totais de cada item-produto");
        }

        [Trait("Categoria", "Compra")]
        [Fact(DisplayName = "Validar horário da compra")]
        public void ValidarHorarioDaCompra_CompraCriada_DeveRetornarAHorarioAtual()
        {
            //Arrange
            var compra = _compraFixture.CriarCompra();

            // Assert
            compra.Data.Should().NotBeBefore(DateTime.Now.AddSeconds(-30), "Horário da compra deve ser atual");
        }

        [Trait("Categoria", "Compra")]
        [Fact(DisplayName = "Definir lista de produtos")]
        public void DefinirProdutos_ListaDeProdutosValida_DeveDefinirAListagemDeProdutos()
        {
            //Arrange
            var compra = _compraFixture.CriarCompra();
            var produto1 = _compraFixture.CriarCompraProduto();
            var produto2 = _compraFixture.CriarCompraProduto();
            var produto3 = _compraFixture.CriarCompraProduto();
            var listaProdutos = _compraFixture.CriarListaProdutos(produto1, produto2, produto3);

            // Act
            compra.DefinirProdutos(listaProdutos);

            // Assert
            compra.Produtos.Should().HaveCount(listaProdutos.Count(), "Deve haver a mesma quantidade de produtos informada");
            compra.Produtos.Should().Contain(produto1, "Deve haver o Produto 01");
            compra.Produtos.Should().Contain(produto2, "Deve haver o Produto 02");
            compra.Produtos.Should().Contain(produto3, "Deve haver o Produto 03");
        }

        [Trait("Categoria", "Compra")]
        [Fact(DisplayName = "Adicionar novos produtos")]
        public void AdicionarProduto_NovoProduto_DeveInserirNaListaDeProdutos()
        {
            //Arrange
            var compra = _compraFixture.CriarCompra();
            var produto1 = _compraFixture.CriarCompraProduto();
            var produto2 = _compraFixture.CriarCompraProduto();
            var produto3 = _compraFixture.CriarCompraProduto();
            var listaProdutos = _compraFixture.CriarListaProdutos(produto1);
            compra.DefinirProdutos(listaProdutos);

            // Act
            compra.AdicionarProduto(produto2);
            compra.AdicionarProduto(produto3);

            // Assert
            compra.Produtos.Should().HaveCount(3, "Deve haver a mesma quantidade de produtos inseridos");
            compra.Produtos.Should().Contain(produto1, "Deve haver o Produto 01");
            compra.Produtos.Should().Contain(produto2, "Deve haver o Produto 02");
            compra.Produtos.Should().Contain(produto3, "Deve haver o Produto 03");
        }

        [Trait("Categoria", "Compra")]
        [Fact(DisplayName = "Adicionar produtos existentes - Produtos")]
        public void AdicionarProduto_ProdutoExistente_NaoDeveAdicionarProdutosIguais()
        {
            //Arrange
            var compra = _compraFixture.CriarCompra();
            var produto1 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 01");
            var produto2 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 02");
            var produto3 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 01");
            var produto4 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 02");
            var produto5 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 02");
            var listaProdutos = _compraFixture.CriarListaProdutos(produto1, produto2);
            compra.DefinirProdutos(listaProdutos);

            // Act
            compra.AdicionarProduto(produto3);
            compra.AdicionarProduto(produto4);
            compra.AdicionarProduto(produto5);

            // Assert
            compra.Produtos.Should().HaveCount(2, "Deve haver a mesma quantidade de produtos inseridos");
            compra.Produtos.Should().Contain(produto1, "Deve haver o Produto 01");
            compra.Produtos.Should().Contain(produto2, "Deve haver o Produto 02");
            compra.Produtos.Should().NotContain(produto3, "Não deve haver o Produto 03");
            compra.Produtos.Should().NotContain(produto4, "Não deve haver o Produto 04");
            compra.Produtos.Should().NotContain(produto5, "Não deve haver o Produto 05");
        }

        [Trait("Categoria", "Compra")]
        [Fact(DisplayName = "Adicionar produtos existentes - Quantidades")]
        public void AdicionarProduto_ProdutoExistente_AdicionarNaQuantidadeDoProduto()
        {
            //Arrange
            var compra = _compraFixture.CriarCompra();
            var produto1 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 01", quantidade: 10);
            var produto2 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 02", quantidade: 20);
            var produto3 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 01", quantidade: 30);
            var produto4 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 02", quantidade: 40);
            var produto5 = _compraFixture.CriarCompraProduto(nomeProduto: "Produto 02", quantidade: 50);
            var listaProdutos = _compraFixture.CriarListaProdutos(produto1, produto2);
            compra.DefinirProdutos(listaProdutos);

            // Act
            compra.AdicionarProduto(produto3);
            compra.AdicionarProduto(produto4);
            compra.AdicionarProduto(produto5);

            // Assert
            compra.Produtos.First(x => x == produto1).Quantidade.Should().Be(40, "Quantidade deve ser somada");
            compra.Produtos.First(x => x == produto2).Quantidade.Should().Be(110, "Quantidade deve ser somada");
        }

        [Trait("Categoria", "Compra")]
        [Fact(DisplayName = "Remover produtos existentes")]
        public void RemoverProduto_ProdutoExistente_DeveRemoverDaListagemDeProdutos()
        {
            //Arrange
            var compra = _compraFixture.CriarCompra();
            var produto1 = _compraFixture.CriarCompraProduto();
            var produto2 = _compraFixture.CriarCompraProduto();
            var produto3 = _compraFixture.CriarCompraProduto();
            var listaProdutos = _compraFixture.CriarListaProdutos(produto1, produto2, produto3);
            compra.DefinirProdutos(listaProdutos);

            // Act
            compra.RemoverProduto(produto2);
            compra.RemoverProduto(produto3);

            // Assert
            compra.Produtos.Should().HaveCount(1, "Deve haver somente o Produto 01");
            compra.Produtos.Should().Contain(produto1, "Deve haver o Produto 01");
            compra.Produtos.Should().NotContain(produto2, "Não deve haver o Produto 02");
            compra.Produtos.Should().NotContain(produto3, "Não deve haver o Produto 03");
        }

        [Trait("Categoria", "Compra")]
        [Fact(DisplayName = "Remover produto não existente")]
        public void RemoverProduto_ProdutoNaoExistente_NaoDeveoAlterarAListagemDeProdutos()
        {
            //Arrange
            var compra = _compraFixture.CriarCompra();
            var produto1 = _compraFixture.CriarCompraProduto();
            var produto2 = _compraFixture.CriarCompraProduto();
            var produto3 = _compraFixture.CriarCompraProduto();
            var listaProdutos = _compraFixture.CriarListaProdutos(produto1, produto2);
            compra.DefinirProdutos(listaProdutos);

            // Act
            compra.RemoverProduto(produto3);

            // Assert
            compra.Produtos.Should().HaveCount(2, "Não deve haver alterações na listagem de produtos");
            compra.Produtos.Should().Contain(produto1, "Deve haver o Produto 01");
            compra.Produtos.Should().Contain(produto2, "Deve haver o Produto 01");
            compra.Produtos.Should().NotContain(produto3, "Não deve haver o Produto 03");
        }
    }
}
