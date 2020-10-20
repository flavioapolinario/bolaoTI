using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Infra.ConfiguracaoEF;

namespace BolaoTI.Infra.Implementacao.InfraEstrutura
{
    public class FabricaDeUnidadeDeTrabalhoEF : IFabricaDeUnidadeDeTrabalho
    {
        private readonly BolaoTIContext _contexto;

        public FabricaDeUnidadeDeTrabalhoEF(BolaoTIContext contexto)
        {
            this._contexto = contexto;
        }

        public IUnidadeDeTrabalho Criar()
        {
            return new UnidadeDeTrabalhoEF(_contexto);
        }
    }
}
