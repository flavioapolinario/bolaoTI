using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Dominio.Servicos;
using Moq;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Dominio.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace BolaoTI.Dominio.Teste.ServicosCadastro
{
    [TestClass]
    public class OrganizacaoServicoCadastroTeste
    {
        private OrganizacaoServicoCadastro _organizacaoServicoCadastro;
        private Mock<IOrganizacaoRepositorio> _mockIOrganizacaoRepositorio;

        [TestInitialize]
        public void IniciarTestes()
        {
            _mockIOrganizacaoRepositorio = new Mock<IOrganizacaoRepositorio>();
            _organizacaoServicoCadastro = new OrganizacaoServicoCadastro(_mockIOrganizacaoRepositorio.Object);
        }

        private Organizacao GetOrganizacaoFake()
        {
            return new Organizacao()
            {
                Nome = "BolaoTI Organizacao",
                Campeonatos = It.IsAny<List<Campeonato>>(),
                Usuarios = It.IsAny<List<Usuario>>()
            };
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_uma_Organizacao_que_ja_existe_lança_exception()
        {
            // arrage
            var organizacao = GetOrganizacaoFake();
            _mockIOrganizacaoRepositorio
                .Setup(x => x.OrganizacaoExistente(organizacao.Nome))
                .Returns(true);

            // act
            _organizacaoServicoCadastro.CadastrarOrganizacao(organizacao);
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_uma_Organizacao_que_ja_existe_Campeonato_Associado_lança_exception()
        {
            // arrage
            StringBuilder mensagem = new StringBuilder();
            var organizacao = GetOrganizacaoFake();
            _mockIOrganizacaoRepositorio
                .Setup(x => x.OrganizacaoExistente(organizacao.Nome))
                .Returns(false);

            _mockIOrganizacaoRepositorio
                .Setup(x => x.CampeonatoAssociado(organizacao.Campeonatos, out mensagem))
                .Returns(true);

            _mockIOrganizacaoRepositorio
                .Setup(x => x.UsuarioAssociado(organizacao.Usuarios, out mensagem))
                .Returns(false);

            // act
            _organizacaoServicoCadastro.CadastrarOrganizacao(organizacao);
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_uma_Organizacao_que_ja_existe_Usuario_Associado_lança_exception()
        {
            // arrage
            StringBuilder mensagem = new StringBuilder();
            var organizacao = GetOrganizacaoFake();
            _mockIOrganizacaoRepositorio
                .Setup(x => x.OrganizacaoExistente(organizacao.Nome))
                .Returns(false);

            _mockIOrganizacaoRepositorio
                .Setup(x => x.CampeonatoAssociado(organizacao.Campeonatos, out mensagem))
                .Returns(false);

            _mockIOrganizacaoRepositorio
                .Setup(x => x.UsuarioAssociado(organizacao.Usuarios, out mensagem))
                .Returns(true);

            // act
            _organizacaoServicoCadastro.CadastrarOrganizacao(organizacao);
        }

        [TestMethod]
        public void Quando_cadastrar_uma_Organizacao_que_não_existe_insere_no_repositorio()
        {
            // arrage
            StringBuilder mensagem = null;
            var organizacao = GetOrganizacaoFake();

            _mockIOrganizacaoRepositorio
               .Setup(x => x.OrganizacaoExistente(organizacao.Nome))
               .Returns(false);

            _mockIOrganizacaoRepositorio
                .Setup(x => x.CampeonatoAssociado(organizacao.Campeonatos, out mensagem))
                .Returns(false);

            _mockIOrganizacaoRepositorio
                .Setup(x => x.UsuarioAssociado(organizacao.Usuarios, out mensagem))
                .Returns(false);

            // act
            _organizacaoServicoCadastro.CadastrarOrganizacao(organizacao);

            // assert
            _mockIOrganizacaoRepositorio.Verify(x => x.Insert(organizacao), Times.Once());
        }
    }
}
