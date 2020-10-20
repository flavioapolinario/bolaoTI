using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Implementacao.Servicos;
using Moq;
using Moq.Sequences;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio;

namespace BolaoTI.Aplicacao.Teste
{
    [TestClass]
    public class EstadioServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<IEstadioServicoCadastro> mockEstadioServicoCadastro;
        private Mock<IEstadioRepositorio> mockEstadioRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private EstadioServicoAplicacao servico;

        private Estadio GetEstadioFake()
        {
            return new Estadio() { Nome = "Mineirao", Cidade = "Belo Horizonte", Uf = "MG", Id = It.IsAny<int>() };
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockEstadioServicoCadastro = new Mock<IEstadioServicoCadastro>();
            mockEstadioRepositorio = new Mock<IEstadioRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new EstadioServicoAplicacao(mockEstadioRepositorio.Object, mockEstadioServicoCadastro.Object, this);
        }

        [TestMethod]
        public void Quando_RecuperarPorID_recuperar_todos_do_repositorio()
        {
            // arrange            
            var estadio = GetEstadioFake();
            mockEstadioRepositorio.Setup(_ => _.Get(It.IsAny<int>())).Returns(estadio);

            // act
            var retorno = servico.RecuperarPorId(It.IsAny<int>());

            // assert
            mockEstadioRepositorio.VerifyAll();
            Assert.AreSame(estadio, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarTodosOsEstadios_recuperar_todos_do_repositorio()
        {
            // arrange
            var estadios = new List<Estadio>();
            mockEstadioRepositorio.Setup(_ => _.FindAll()).Returns(estadios);

            // act
            var retorno = servico.RecuperarTodosOsEstadios();

            // assert
            mockEstadioRepositorio.VerifyAll();
            Assert.AreSame(estadios, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var estadios = new List<Estadio>();
            mockEstadioRepositorio.Setup(_ => _.FindByFilter("Mineirao", "Belo Horizonte", "MG")).Returns(estadios);

            // act
            var retorno = servico.RecuperarPorFiltro("Mineirao", "Belo Horizonte", "MG");

            // assert
            mockEstadioRepositorio.VerifyAll();
            Assert.AreSame(estadios, retorno);
        }

        [TestMethod]
        public void Quando_CadastrarEstadio_chamar_cadastrar_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var estadio = GetEstadioFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockEstadioServicoCadastro.Setup(_ => _.CadastrarEstadio(estadio)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.CadastrarEstadio(estadio);
            }
        }

        [TestMethod]
        public void Quando_Remover_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            Estadio estadio = GetEstadioFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockEstadioRepositorio.Setup(_ => _.Delete(estadio)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(estadio);
            }
        }

        [TestMethod]
        public void Quando_RemoverPorId_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockEstadioRepositorio.Setup(_ => _.Delete(It.IsAny<int>())).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(It.IsAny<int>());
            }
        }

        [TestMethod]
        public void Quando_Atualizar_chamar_Update_do_repositorio_dentro_de_uma_transacao()
        {
            Estadio estadio = GetEstadioFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockEstadioRepositorio.Setup(_ => _.Update(estadio)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Atualizar(estadio);
            }
        }

    }
}
