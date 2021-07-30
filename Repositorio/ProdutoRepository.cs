using Dominio.Contratos;
using Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Repositorio
{
    /// <summary>
    /// APENAS PARA SIMULAR O ACESSO AO BANCO DE DADOS
    /// </summary>
    public class ProdutoRepository : IProdutoRepository
    {
        public Task AddAsync(Produto produto)
        {
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Guid id)
        {
            return Task.CompletedTask;
        }
    }
}
