using BolaoTI.web.Models;
using BolaoTI.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolaoTI.web.DAL.Interface
{
    public interface IRankingRepository
    {
        List<RankingData> GetRanking();

        List<RankingData> GetRankingPorFase(Fase fase);        
    }
}
