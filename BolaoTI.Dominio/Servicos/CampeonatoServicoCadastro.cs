using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;

namespace BolaoTI.Dominio.Servicos
{
    public class CampeonatoServicoCadastro : ICampeonatoServicoCadastro
    {
        private readonly ICampeonatoRepositorio _campeonatoRepositorio;

        public CampeonatoServicoCadastro(ICampeonatoRepositorio campeonatoRepositorio)
        {
            _campeonatoRepositorio = campeonatoRepositorio;
        }

        public void CadastrarCampeonato(Campeonato campeonato)
        {
            if (_campeonatoRepositorio.CampeonatoExistente(campeonato.Nome, campeonato.Inicio, campeonato.Fim))
            {
                throw new BolaoTIException(string.Format(Messages.AlertMessage_Registro_Existente, Classes.Campeonato_Class, Field.Campeonato_Nome_Field, campeonato.Nome));
            }

            _campeonatoRepositorio.Insert(campeonato);
        }
    }
}
