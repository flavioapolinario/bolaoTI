using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Aplicacao.Implementacao.Servicos;

namespace BolaoTI.Infra.Teste
{
    [TestClass]
    public class BolaoTIContextTeste
    {
        UsuarioServicoAplicacao servico;

        [TestInitialize]
        public void IniciarTestes()
        {
            servico = new UsuarioServicoAplicacao(mockUsuarioRepositorio.Object, mockUsuarioServicoCadastro.Object, this);
        }

        [TestMethod]
        public void Quando_Iniciar_Contexto_Criar_BancoDados()
        {
            
        }

    }
}
