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
    public class UsuarioServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<IUsuarioServicoCadastro> mockUsuarioServicoCadastro;
        private Mock<IUsuarioRepositorio> mockUsuarioRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private UsuarioServicoAplicacao servico;
        
        private Usuario GetUsuarioFake()
        {
            return new Usuario() { Id = It.IsAny<Guid>(), Nome = "Flávio Apolinário", Email = @"flavioapolinario@hotmail.com", Perfis = It.IsAny<List<Perfil>>() };
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockUsuarioServicoCadastro = new Mock<IUsuarioServicoCadastro>();
            mockUsuarioRepositorio = new Mock<IUsuarioRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();
            
            servico = new UsuarioServicoAplicacao(mockUsuarioRepositorio.Object, mockUsuarioServicoCadastro.Object, this);
        }

        [TestMethod]
        public void Quando_RecuperarUsuarioPorID_recuperar_todos_do_repositorio()
        {
            // arrange            
            var Usuario = GetUsuarioFake();
            mockUsuarioRepositorio.Setup(_ => _.FindById(It.IsAny<Guid>())).Returns(Usuario);

            // act
            var retorno = servico.RecuperarPorId(It.IsAny<Guid>());

            // assert
            mockUsuarioRepositorio.VerifyAll();
            Assert.AreSame(Usuario, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarTodosOsUsuarios_recuperar_todos_do_repositorio()
        {
            // arrange
            var usuarios = new List<Usuario>();
            mockUsuarioRepositorio.Setup(_ => _.FindAll()).Returns(usuarios);

            // act
            var retorno = servico.RecuperarTodosOsUsuarios();

            // assert
            mockUsuarioRepositorio.VerifyAll();
            Assert.AreSame(usuarios, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var usuarios = new List<Usuario>();
            mockUsuarioRepositorio.Setup(_ => _.FindByFilter(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<Guid[]>())).Returns(usuarios);

            // act
            var retorno = servico.RecuperarPorFiltro(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<Guid[]>());

            // assert
            mockUsuarioRepositorio.VerifyAll();
            Assert.AreSame(usuarios, retorno);
        }

        [TestMethod]
        public void Quando_CadastrarUsuario_chamar_cadastrar_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var Usuario = GetUsuarioFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockUsuarioServicoCadastro.Setup(_ => _.CadastrarUsuario(Usuario)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.CadastrarUsuario(Usuario);
            }
        }

        [TestMethod]
        public void Quando_Remover_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            Usuario Usuario = GetUsuarioFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockUsuarioRepositorio.Setup(_ => _.Delete(Usuario)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(Usuario);
            }
        }

        [TestMethod]
        public void Quando_RemoverPorId_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockUsuarioRepositorio.Setup(_ => _.Delete(It.IsAny<Guid>())).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(It.IsAny<Guid>());
            }
        }

        [TestMethod]
        public void Quando_Atualizar_chamar_Update_do_repositorio_dentro_de_uma_transacao()
        {
            Usuario Usuario = GetUsuarioFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockUsuarioRepositorio.Setup(_ => _.Update(Usuario)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Atualizar(Usuario);
            }
        }
    }
}
