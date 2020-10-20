using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolaoTI.web.DAL.Interface
{
    public interface IApostaRepository 
    {
        List<BolaoTI.web.ViewModels.ApostaPartidaData> GetPartidasPorFase(int IdFase, int IdUsuario);

        void SalvarAposta(IEnumerable<BolaoTI.web.ViewModels.ApostaPartidaData> apostas);
    }
}
