using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BolaoTI.web.ViewModels;
using BolaoTI.web.DAL.Interface;
using BolaoTI.web.Models;

namespace BolaoTI.web.DAL
{
    public class RankingRepository : GenericRepository<Aposta>, IRankingRepository
    {
        public RankingRepository(BolaoTIContext context)
            : base(context)
        {

        }

        private int GetValorPorPonto(int IdUsuario, int Ponto, List<Aposta> apostas)
        {
            var valor = (from a in apostas
                         where a.Usuario.UserId == IdUsuario && a.PontosAposta == Ponto
                         select a.PontosAposta).Sum();
            return valor;
        }

        public List<RankingData> GetRanking()
        {
            var ranking = new List<RankingData>();
            var apostas = (from a in context.Apostas.Include("Usuario") select a).ToList();

            ranking = (from a in apostas
                       group a by a.Usuario into apostaUsuario
                       select new RankingData()
                       {                           
                           IdUsuario = apostaUsuario.Key.UserId,
                           NomeUsuario = apostaUsuario.Key.UserName,
                           Usuario = apostaUsuario.Key.UserName,
                           TotalPontos = apostaUsuario.Sum(p => p.PontosAposta),
                           ExhibitionName = apostaUsuario.Key.ExhibitionName
                       }).ToList();

            foreach (var usuarioRank in ranking)
            {                
                usuarioRank.DezTotalPontos = GetValorPorPonto(usuarioRank.IdUsuario, 10, apostas);
                usuarioRank.SeteTotalPontos = GetValorPorPonto(usuarioRank.IdUsuario, 7, apostas);
                usuarioRank.CincoTotalPontos = GetValorPorPonto(usuarioRank.IdUsuario, 5, apostas);
                usuarioRank.DoisTotalPontos = GetValorPorPonto(usuarioRank.IdUsuario, 2, apostas);
            }

            return ranking;
        }      

        public List<RankingData> GetRankingPorFase(Models.Fase fase)
        {
            throw new NotImplementedException();
        }       
    }
}