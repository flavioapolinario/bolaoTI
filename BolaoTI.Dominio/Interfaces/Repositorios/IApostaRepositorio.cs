using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IApostaRepositorio : IRepositorioGenerico<Dominio.Aposta>
    {
        List<Aposta> FindApostasByPartida(int idPartida);

        List<Aposta> FindApostasByFase(Guid idUsuario, int idFase);

        List<Aposta> FindApostasByFilter(int IdOrganizacao, int CampeonatoId, int IdFase, Guid? IdUsuario);

        List<Aposta> FindApostasByUsuario(int IdOrganizacao, int CampeonatoId, Guid IdUsuario);

        void RealizarAposta(IList<Aposta> apostas);
    }
}
