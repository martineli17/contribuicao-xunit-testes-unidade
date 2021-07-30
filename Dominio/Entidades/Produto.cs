namespace Dominio.Entidades
{
    public class Produto
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }

        public Produto DefinirNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public Produto DefinirPreco(decimal preco)
        {
            if (preco < 0)
                preco *= -1;
            Preco = preco;
            return this;
        }
    }
}
