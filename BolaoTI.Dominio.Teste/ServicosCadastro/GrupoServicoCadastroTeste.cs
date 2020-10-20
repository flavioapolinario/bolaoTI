using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Dominio.Servicos;
using BolaoTI.Dominio.Interfaces.Repositorios;
using Moq;
using BolaoTI.Dominio.Exceptions;

namespace BolaoTI.Dominio.Teste.ServicosCadastro
{
    [TestClass]
    public class GrupoServicoCadastroTeste
    {
        private GrupoServicoCadastro _grupoServicoCadastro;
        private Mock<IGrupoRepositorio> _mockIGrupoRepositorio;

        [TestInitialize]
        public void IniciarTestes()
        {
            _mockIGrupoRepositorio = new Mock<IGrupoRepositorio>();
            _grupoServicoCadastro = new GrupoServicoCadastro(_mockIGrupoRepositorio.Object);
        }

        private Grupo GetGrupoFake()
        {
            return new Grupo()
            {
                Nome = "Grupo A",
                Fase = new Fase() { Nome = "Fase de Grupos" }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_um_Grupo_que_ja_existe_lança_exception()
        {
            // arrage
            Grupo grupo = GetGrupoFake();
            _mockIGrupoRepositorio
                .Setup(x => x.GrupoExistente(grupo.Nome, grupo.Fase.Id))
                .Returns(true);

            // act
            _grupoServicoCadastro.CadastrarGrupo(grupo);
        }

        [TestMethod]
        public void Quando_cadastrar_um_Grupo_que_não_existe_insere_no_repositorio()
        {
            // arrage
            Grupo grupo = GetGrupoFake();
            _mockIGrupoRepositorio
                .Setup(x => x.GrupoExistente(grupo.Nome, grupo.Fase.Id))
                .Returns(false);

            // act
            _grupoServicoCadastro.CadastrarGrupo(grupo);

            // assert
            _mockIGrupoRepositorio.Verify(x => x.Insert(grupo), Times.Once());
        }
    }
}
