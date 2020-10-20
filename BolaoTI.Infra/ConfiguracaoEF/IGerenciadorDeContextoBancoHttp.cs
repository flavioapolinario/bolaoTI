
namespace BolaoTI.Infra.ConfiguracaoEF
{
    public interface IGerenciadorDeContextoBancoHttp
    {
        BolaoTIContext Contexto { get; }

        void Finalizar();
    }
}
