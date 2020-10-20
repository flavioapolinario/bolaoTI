
using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;
using System.Linq;
using System.Text;

namespace BolaoTI.Dominio.Servicos
{
    public class OrganizacaoServicoCadastro : IOrganizacaoServicoCadastro
    {
        private readonly IOrganizacaoRepositorio _organizacaoRepositorio;

        public OrganizacaoServicoCadastro(IOrganizacaoRepositorio organizacaoRepositorio)
        {
            _organizacaoRepositorio = organizacaoRepositorio;
        }

        public void CadastrarOrganizacao(Organizacao organizacao)
        {
            if (_organizacaoRepositorio.OrganizacaoExistente(organizacao.Nome))
                throw new BolaoTIException(string.Format(Messages.AlertMessage_Registro_Existente, Classes.Organizacao_Class, Field.Organizacao_Nome_Field, organizacao.Nome));

            StringBuilder mensagem;
            if (_organizacaoRepositorio.CampeonatoAssociado(organizacao.Campeonatos, out mensagem))
                throw new BolaoTIException(mensagem.ToString());
            
            if (_organizacaoRepositorio.UsuarioAssociado(organizacao.Usuarios, out mensagem))
                throw new BolaoTIException(mensagem.ToString());

            _organizacaoRepositorio.Insert(organizacao);
        }
    }
}
