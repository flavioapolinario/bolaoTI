using System;
using System.Collections.Generic;
namespace BolaoTI.Dominio
{
    public partial class Usuario
    {
        private Guid _id;
        private string _nome;
        private string _email;
        private string _telefone;
        private string _passwordHash;
        private string _securityStamp;

        private List<Perfil> _perfis;
        private List<Claim> _claims;
        private List<ExternalLogin> _externalLogins;
        private List<Ranking> _rankings;
        private List<Aposta> _apostas;
        private List<Organizacao> _organizacoes;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }

        public string SecurityStamp
        {
            get { return _securityStamp; }
            set { _securityStamp = value; }
        }

        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

        public List<Perfil> Perfis
        {
            get { return _perfis ?? (_perfis = new List<Perfil>()); }
            set { _perfis = value; }
        }

        public virtual List<Claim> Claims
        {
            get { return _claims ?? (_claims = new List<Claim>()); }
            set { _claims = value; }
        }

        public virtual List<ExternalLogin> ExternalLogins
        {
            get { return _externalLogins ?? (_externalLogins = new List<ExternalLogin>()); }
            set { _externalLogins = value; }
        }

        public List<Ranking> Rankings
        {
            get { return _rankings ?? (_rankings = new List<Ranking>()); }
            set { _rankings = value; }
        }

        public List<Aposta> Apostas
        {
            get { return _apostas ?? (_apostas = new List<Aposta>()); }
            set { _apostas = value; }
        }

        public List<Organizacao> Organizacoes
        {
            get { return _organizacoes ?? (_organizacoes = new List<Organizacao>()); }
            set { _organizacoes = value; }
        }


        public bool EhParticipante
        {
            get
            {
                return (this.Organizacoes.Count > 0 && 
                        this.Perfis.Count > 0);
            }
        }
    }
}
