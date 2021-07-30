using Dominio.Contratos;
using Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servico
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _repository;

        public CompraService(ICompraRepository repository)
        {
            _repository = repository;
        }
        public async Task<Compra> AddAsync(ICollection<CompraProduto> compraProdutos, string comprador)
        {
            var entidade = new Compra().DefinirProdutos(compraProdutos);
            entidade.DefinirComprador(comprador);
            await _repository.AddAsync(entidade);
            return entidade;
        }
    }
}
