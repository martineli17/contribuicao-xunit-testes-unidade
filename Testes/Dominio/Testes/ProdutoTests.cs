using Dominio.Entidades;
using FluentAssertions;
using Xunit;

namespace Testes.Dominio.Testes
{
    public class ProdutoTests
    {
        private Produto _produto;
        public ProdutoTests()
        {
            _produto = new Produto();
        }

        [Trait("Categoria", "Produto")]
        [Theory(DisplayName = "Definir nome")]
        [InlineData("Produto 01")]
        [InlineData("Produto 02")]
        [InlineData("Produto 03")]
        public void DefinirNome_NomeValido_DeveAlterarONome(string nome)
        {
            // Act
            _produto.DefinirNome(nome);

            // Assert
            _produto.Nome.Should().Be(nome, "Produto de ter seu nome definido de acordo com o parâmetro passado");

        }

        [Theory(DisplayName = "Definir preço maior que 0")]
        [Trait("Categoria", "Produto")]
        [InlineData(0)]
        [InlineData(29.87)]
        [InlineData(100)]
        [InlineData(4587.23)]
        public void DefinirPreco_PrecoMaiorQue0_DeveAlterarPreco(decimal preco)
        {
            // Act
            _produto.DefinirPreco(preco);

            // Assert
            _produto.Preco.Should().Be(preco, "Produto de ter seu preço definido de acordo com o parâmetro passado");

        }

        [Theory(DisplayName = "Definir preço menor que 0")]
        [Trait("Categoria", "Produto")]
        [InlineData(-29.87)]
        [InlineData(-100)]
        [InlineData(-4587.23)]
        public void DefinirPreco_PrecoMenorQue0_DeveAlterarPrecoComValorPositivo(decimal preco)
        {
            // Act
            _produto.DefinirPreco(preco);

            // Assert
            _produto.Preco.Should().Be((preco * -1), "Produto de ter seu preço com valor positivo");

        }
    }
}
