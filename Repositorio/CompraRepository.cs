using Dominio.Contratos;
using Dominio.Entidades;
using System.Threading.Tasks;

namespace Repositorio
{
    /// <summary>
    /// APENAS PARA SIMULAR O ACESSO AO BANCO DE DADOS
    /// </summary>
    public class CompraRepository : ICompraRepository
    {
        public Task AddAsync(Compra produto)
        {
            return Task.CompletedTask;
        }
    }
}
