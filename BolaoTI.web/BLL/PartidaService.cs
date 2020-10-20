using BolaoTI.web.BLL.Interface;
using BolaoTI.web.DAL;
using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BolaoTI.web.BLL
{
    public class PartidaService : IPartidaService
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private RegraService regraService = new RegraService();

        public void FecharPartida(Partida partida)
        {
            if (partida == null)
                throw new Exception("Partida não encontrada");

            unitOfWork.PartidaRepository.Update(partida);

            var apostasPartida = unitOfWork.ApostaRepository.Get(filter: a => a.PartidaID == partida.Id, includeProperties: "Usuario, PartidaApostada");

            if (apostasPartida != null)
            {
                foreach (var aposta in apostasPartida)
                {
                    aposta.PontosAposta = regraService.CalculaPontos(aposta, partida);

                    unitOfWork.ApostaRepository.Update(aposta);
                }
            }
        }

        public void FecharPartida(List<Partida> partidas)
        {
            if (partidas == null)
                throw new Exception("Partida não encontrada");

            partidas.ForEach(FecharPartida);
        }

        public void Save()
        {
            unitOfWork.Save();
        }
    }
}