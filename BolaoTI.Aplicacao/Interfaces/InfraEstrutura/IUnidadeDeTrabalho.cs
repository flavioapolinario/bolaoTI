using System;
using System.Threading;
using System.Threading.Tasks;

namespace BolaoTI.Aplicacao.Interfaces.Infraestrutura
{
    public interface IUnidadeDeTrabalho : IDisposable
    {
        void Iniciar();

        void Completar();
        Task<int> CompletarAsync();
        Task<int> CompletarAsync(CancellationToken cancellationToken);
    }
}
