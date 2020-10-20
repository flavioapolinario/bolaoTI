using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BolaoTI.web.BLL.Interface;
using BolaoTI.web.DAL;
using BolaoTI.web.Models;

namespace BolaoTI.web.BLL
{
    public class FaseService : IFaseService
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private RegraService regraService = new RegraService();

        public void Fechar(int idFase)
        {
            var fase = unitOfWork.FaseRepository.Get(filter: f => f.Id == idFase,
                includeProperties: "Grupos.Rodadas.Partidas.Apostas").FirstOrDefault();

            if (fase == null)
                throw new Exception("Fase não encontrada");

            foreach (var grupo in fase.Grupos)
            {
                foreach (var rodada in grupo.Rodadas)
                {
                    foreach (var partida in rodada.Partidas.Where(p => p.GolsTimeHome.HasValue && p.GolsTimeAway.HasValue))
                    {
                        foreach (var aposta in partida.Apostas)
                        {
                            aposta.PontosAposta = regraService.CalculaPontos(aposta, partida);
                            unitOfWork.ApostaRepository.Update(aposta);
                        }
                    }
                }
            }
            unitOfWork.Save();
        }
    }
}