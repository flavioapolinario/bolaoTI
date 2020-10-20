using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Sequences;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Implementacao.Servicos;
using BolaoTI.Dominio;
using System.Collections.Generic;


namespace BolaoTI.Aplicacao.Teste
{
    [TestClass]
    public class TimeServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<ITimeServicoCadastro> mockTimeServicoCadastro;
        private Mock<ITimeRepositorio> mockTimeRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private TimeServicoAplicacao servico;

        private Time GetTimeFake()
        {
            return new Time() { Id = It.IsAny<int>(), Nome = "Brasil", NomeAbreviado = "BRA", ImagemBandeira = @"..\\Content\themes\base\images\Bandeiras\brasil_30x30.png" };
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockTimeServicoCadastro = new Mock<ITimeServicoCadastro>();
            mockTimeRepositorio = new Mock<ITimeRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new TimeServicoAplicacao(mockTimeRepositorio.Object, mockTimeServicoCadastro.Object, this);
        }

        [TestMethod]
        public void Quando_RecuperarPorID_recuperar_todos_do_repositorio()
        {
            // arrange            
            var Time = GetTimeFake();
            mockTimeRepositorio.Setup(_ => _.Get(It.IsAny<int>())).Returns(Time);

            // act
            var retorno = servico.RecuperarPorId(It.IsAny<int>());

            // assert
            mockTimeRepositorio.VerifyAll();
            Assert.AreSame(Time, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarTodosOsTimes_recuperar_todos_do_repositorio()
        {
            // arrange
            var Times = new List<Time>();
            mockTimeRepositorio.Setup(_ => _.FindAll()).Returns(Times);

            // act
            var retorno = servico.RecuperarTodosOsTimes();

            // assert
            mockTimeRepositorio.VerifyAll();
            Assert.AreSame(Times, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var Times = new List<Time>();
            mockTimeRepositorio.Setup(_ => _.FindByFilter("Brasil", "BRA")).Returns(Times);

            // act
            var retorno = servico.RecuperarPorFiltro("Brasil", "BRA");

            // assert
            mockTimeRepositorio.VerifyAll();
            Assert.AreSame(Times, retorno);
        }

        [TestMethod]
        public void Quando_CadastrarTime_chamar_cadastrar_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var Time = GetTimeFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockTimeServicoCadastro.Setup(_ => _.CadastrarTime(Time)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.CadastrarTime(Time);
            }
        }

        [TestMethod]
        public void Quando_Remover_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            Time Time = GetTimeFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockTimeRepositorio.Setup(_ => _.Delete(Time)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(Time);
            }
        }

        [TestMethod]
        public void Quando_RemoverPorId_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockTimeRepositorio.Setup(_ => _.Delete(It.IsAny<int>())).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(It.IsAny<int>());
            }
        }

        [TestMethod]
        public void Quando_Atualizar_chamar_Update_do_repositorio_dentro_de_uma_transacao()
        {
            Time Time = GetTimeFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockTimeRepositorio.Setup(_ => _.Update(Time)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Atualizar(Time);
            }
        }
    }
}
