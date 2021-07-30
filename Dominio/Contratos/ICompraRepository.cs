using Dominio.Entidades;
using System.Threading.Tasks;

namespace Dominio.Contratos
{
    public interface ICompraRepository
    {
        Task AddAsync(Compra produto);
    }
}
