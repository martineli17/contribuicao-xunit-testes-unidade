using Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Dominio.Contratos
{
    public interface IProdutoService
    {
        Task<Produto> AddAsync(string nome, decimal preco);
        Task<bool> RemoveAsync(Guid id);
    }
}
