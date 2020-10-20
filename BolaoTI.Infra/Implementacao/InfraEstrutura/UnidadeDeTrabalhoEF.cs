using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Infra.ConfiguracaoEF;
using System.Threading.Tasks;

namespace BolaoTI.Infra.Implementacao.InfraEstrutura
{
    public class UnidadeDeTrabalhoEF : IUnidadeDeTrabalho
    {
        private BolaoTIContext _contexto;

        public UnidadeDeTrabalhoEF(BolaoTIContext contexto)
        {
            _contexto = contexto;
        }

        public void Iniciar()
        {

        }

        public void Completar()
        {
            _contexto.SaveChanges();
        }

        public Task<int> CompletarAsync()
        {
            return _contexto.SaveChangesAsync();
        }

        public Task<int> CompletarAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _contexto.SaveChangesAsync(cancellationToken);
        }

        public void Dispose() { }
    }
}
