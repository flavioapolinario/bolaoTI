using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Dominio.Servicos;
using BolaoTI.Dominio.Interfaces.Repositorios;
using Moq;
using BolaoTI.Dominio.Exceptions;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Teste.ServicosCadastro
{
    [TestClass]
    public class UsuarioServicoCadastroTeste
    {
        private UsuarioServicoCadastro _UsuarioServicoCadastro;
        private Mock<IUsuarioRepositorio> _mockIUsuarioRepositorio;

        [TestInitialize]
        public void IniciarTestes()
        {
            _mockIUsuarioRepositorio = new Mock<IUsuarioRepositorio>();
            _UsuarioServicoCadastro = new UsuarioServicoCadastro(_mockIUsuarioRepositorio.Object);
        }

        private Usuario GetUsuarioFake()
        {
            return new Usuario() { Id = It.IsAny<Guid>(), Nome = "Flávio Apolinário", Email = @"flavioapolinario@hotmail.com", Telefone = "99118-1453", Perfis = It.IsAny<List<Perfil>>() };
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_um_Usuario_que_ja_existe_lança_exception()
        {
            // arrage
            var usuario = GetUsuarioFake();
            _mockIUsuarioRepositorio
                .Setup(x => x.UsuarioExistente(usuario.Email))
                .Returns(true);

            // act
            _UsuarioServicoCadastro.CadastrarUsuario(usuario);
        }

        [TestMethod]
        public void Quando_cadastrar_um_Usuario_que_não_existe_insere_no_repositorio()
        {
            // arrage
            var usuario = GetUsuarioFake();
            _mockIUsuarioRepositorio
                .Setup(x => x.UsuarioExistente(usuario.Email))
                .Returns(false);

            // act
            _UsuarioServicoCadastro.CadastrarUsuario(usuario);

            // assert
            _mockIUsuarioRepositorio.Verify(x => x.Insert(usuario), Times.Once());
        }
    }
}
