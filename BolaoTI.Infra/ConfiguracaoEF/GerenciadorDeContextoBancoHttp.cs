using System.Web;

namespace BolaoTI.Infra.ConfiguracaoEF
{
    public class GerenciadorDeContextoBancoHttp : IGerenciadorDeContextoBancoHttp
    {
        public const string ContextoHttp = "ContextoHttp";

        public BolaoTIContext Contexto
        {
            get
            {
                if (HttpContext.Current.Items[ContextoHttp] == null)
                {
                    HttpContext.Current.Items[ContextoHttp] = new BolaoTIContext();
                }
                return HttpContext.Current.Items[ContextoHttp] as BolaoTIContext;
            }
        }

        #region IGerenciadorDeRepositorio Members

        public void Finalizar()
        {
            if (HttpContext.Current.Items[ContextoHttp] != null)
            {
                (HttpContext.Current.Items[ContextoHttp] as BolaoTIContext).Dispose();
            }
        }

        #endregion
    }
}
