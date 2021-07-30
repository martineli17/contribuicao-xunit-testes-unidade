using Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Contratos
{
    public interface ICompraService
    {
        Task<Compra> AddAsync(ICollection<CompraProduto> compraProdutos, string comprador);
    }
}
