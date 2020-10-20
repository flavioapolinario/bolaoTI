using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class ApostaServicoAplicacao : IApostaServicoAplicacao
    {
        private readonly IRegraServicoAplicacao _regraServicoAplicacao;
        private readonly IApostaRepositorio _apostaRepositorio;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public ApostaServicoAplicacao(IRegraServicoAplicacao regraServicoAplicacao,
                                      IApostaRepositorio ApostaRepositorio,
                                      IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _regraServicoAplicacao = regraServicoAplicacao;
            _apostaRepositorio = ApostaRepositorio;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }

        public List<Aposta> RecuperarApostasPorFiltro(int idOrganizacao, int idCampeonato, int idFase, Guid? idUsuario)
        {
            return _apostaRepositorio.FindApostasByFilter(idOrganizacao, idCampeonato, idFase, idUsuario);
        }

        public void RealizarAposta(IList<Aposta> apostas)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _apostaRepositorio.RealizarAposta(apostas);

                unidadeDeTrabalho.Completar();
            }
        }

        public void CalculaPontos(Partida partida)
        {
            var apostas = _apostaRepositorio.FindApostasByPartida(partida.Id);
            apostas.ForEach(aposta =>
            {
                aposta.PontosAposta = _regraServicoAplicacao.CalculaPontos(aposta, partida);
                _apostaRepositorio.Update(aposta);
            });
        }
    }
}
