using Dominio.Contratos;
using Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Servico
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Produto> AddAsync(string nome, decimal preco)
        {
            var entidade = new Produto().DefinirNome(nome);
            entidade.DefinirPreco(preco);
            await _repository.AddAsync(entidade);
            return entidade;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            await _repository.RemoveAsync(id);
            return true;
        }
    }
}
