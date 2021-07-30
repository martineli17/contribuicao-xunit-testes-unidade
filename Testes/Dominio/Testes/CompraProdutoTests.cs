using Dominio.Entidades;
using FluentAssertions;
using Xunit;

namespace Testes.Dominio.Testes
{
    public class CompraProdutoTests
    {
        private CompraProduto _compraProduto;
        public CompraProdutoTests()
        {
            _compraProduto = new CompraProduto();
        }

        [Fact(DisplayName = "Definir produto")]
        [Trait("Categoria", "Compra Produto")]
        public void DefinirProduto_NovoProduto_DeveAlterarOProduto()
        {
            // Arrange
            var produto = new Produto().DefinirNome("Produto 01").DefinirPreco(250);

            // Act
            _compraProduto.DefinirProduto(produto);

            // Assert
            _compraProduto.Produto.Should().Be(produto, "Deve ser igual ao produto que foi inserido");
        }

        [Theory(DisplayName = "Definir quantidade maior que 0")]
        [Trait("Categoria", "Compra Produto")]
        [InlineData(0)]
        [InlineData(29.87)]
        [InlineData(100)]
        [InlineData(4587.23)]
        public void DefinirQuantidade_QuantidadeMaiorQue0_DeveAlterarQuantidade(double quantidade)
        {
            // Act
            _compraProduto.DefinirQuantidade(quantidade);

            // Assert
            _compraProduto.Quantidade.Should().Be(quantidade, "Quantidade deve estar de acordo com o parâmetro passado");
        }

        [Theory(DisplayName = "Definir quantidade menor que 0")]
        [Trait("Categoria", "Compra Produto")]
        [InlineData(-0.9)]
        [InlineData(-29.87)]
        [InlineData(-100)]
        [InlineData(-4587.23)]
        public void DefinirQuantidade_QuantidadeMenorQue0_DeveAlterarQuantidadeParaPositiva(double quantidade)
        {
            // Act
            _compraProduto.DefinirQuantidade(quantidade);

            // Assert
            _compraProduto.Quantidade.Should().Be(quantidade * -1, "Quantidade deve estar positiva e de acordo com o parâmetro passado");
        }

        [Theory(DisplayName = "Aumentar quantidade maior que 0")]
        [Trait("Categoria", "Compra Produto")]
        [InlineData(0.9)]
        [InlineData(29.87)]
        [InlineData(100)]
        [InlineData(4587.23)]
        public void AumentarQuantidade_QuantidadeMaiorQue0_DeveAlterarQuantidadeParaPositivaESomar(double quantidade)
        {
            //Arrange
            _compraProduto.DefinirQuantidade(quantidade);

            // Act
            _compraProduto.AumentarQuantidade(quantidade);

            // Assert
            _compraProduto.Quantidade.Should().Be(quantidade * 2, "Quantidade deve estar positiva e somada com o valor anterior");
        }

        [Theory(DisplayName = "Aumentar quantidade menor que 0")]
        [Trait("Categoria", "Compra Produto")]
        [InlineData(-0.9)]
        [InlineData(-29.87)]
        [InlineData(-100)]
        [InlineData(-4587.23)]
        public void AumentarQuantidade_QuantidadeMenorQue0_DeveAlterarQuantidadeParaPositivaESomar(double quantidade)
        {
            //Arrange
            _compraProduto.DefinirQuantidade(quantidade);

            // Act
            _compraProduto.AumentarQuantidade(quantidade);

            // Assert
            _compraProduto.Quantidade.Should().Be(quantidade * -1 * 2, "Quantidade deve estar positiva e somada com o valor anterior");
        }

        [Theory(DisplayName = "Diminuir quantidade")]
        [Trait("Categoria", "Compra Produto")]
        [InlineData(0.1)]
        [InlineData(29.87)]
        [InlineData(100)]
        [InlineData(4587.23)]
        public void DiminuirQuantidade_QuantidadeMaiorQue0_DeveAlterarQuantidadeAtualizada(double quantidade)
        {
            //Arrange
            _compraProduto.DefinirQuantidade(quantidade);

            // Act
            _compraProduto.DiminuirQuantidade(quantidade / 2);

            // Assert
            _compraProduto.Quantidade.Should().Be(quantidade / 2, "Quantidade deve estar positiva e somada com o valor anterior");
        }

        [Theory(DisplayName = "Calcular valor total - Produto existante")]
        [Trait("Categoria", "Compra Produto")]
        [InlineData(10, 0.1)]
        [InlineData(50, 100)]
        [InlineData(12.80, 29.87)]
        [InlineData(57.80, 4587.23)]
        public void CalcularValorTotal_ProdutoExistente_DeveRetornarOValorTotalDosItensDaCompra(decimal preco, double quantidade)
        {
            //Arrange
            var produto = new Produto().DefinirPreco(preco);
            _compraProduto.DefinirQuantidade(quantidade).DefinirProduto(produto);

            // Act
            _compraProduto.CalcularValorTotal();

            // Assert
            _compraProduto.ValorTotal.Should().Be((decimal)quantidade * preco, "Valor total do produto deve estar de acordo com sua quantidade e preco");
        }

        [Theory(DisplayName = "Calcular valor total - Produto não existante")]
        [Trait("Categoria", "Compra Produto")]
        [InlineData(10, 0.1)]
        [InlineData(50, 100)]
        [InlineData(12.80, 29.87)]
        [InlineData(57.80, 4587.23)]
        public void CalcularValorTotal_ProdutoNaoExistente_DeveRetornarOValorTotalDosItensDaCompra(decimal preco, double quantidade)
        {
            //Arrange
            _compraProduto.DefinirQuantidade(quantidade).DefinirProduto(null);

            // Act
            _compraProduto.CalcularValorTotal();

            // Assert
            _compraProduto.ValorTotal.Should().Be(0, "Valor total do produto deve ser 0, por não existir");
        }
    }
}
