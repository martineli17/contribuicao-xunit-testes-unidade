namespace Dominio.Entidades
{
    public class CompraProduto
    {
        public Produto Produto { get; private set; }
        public double Quantidade { get; private set; }
        public decimal ValorTotal { get; private set; }

        public CompraProduto DefinirProduto(Produto produto)
        {
            Produto = produto;
            return this;
        }

        public CompraProduto DefinirQuantidade(double quantidade)
        {
            if (quantidade < 0)
                quantidade *= -1;
            Quantidade = quantidade;
            return this;
        }

        public CompraProduto AumentarQuantidade(double quantidade)
        {
            if (quantidade < 0)
                quantidade *= -1;
            Quantidade += quantidade;
            return this;
        }

        public CompraProduto DiminuirQuantidade(double quantidade)
        {
            Quantidade -= quantidade;
            return this;
        }

        public decimal CalcularValorTotal()
        {
            ValorTotal = (Produto?.Preco ?? 0) * (decimal)Quantidade;
            return ValorTotal;
        }
    }
}
