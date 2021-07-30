using Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Dominio.Contratos
{
    public interface IProdutoRepository
    {
        Task AddAsync(Produto produto);
        Task RemoveAsync(Guid id);
    }
}
