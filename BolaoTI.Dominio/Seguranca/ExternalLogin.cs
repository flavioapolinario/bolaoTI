using System;

namespace BolaoTI.Dominio
{
    public class ExternalLogin
    {
        private Usuario _usuario;
        private string _loginProvider;
        private string _providerKey;
        private Guid _userId;

        public string LoginProvider
        {
            get { return _loginProvider; }
            set { _loginProvider = value; }
        }

        public string ProviderKey
        {
            get { return _providerKey; }
            set { _providerKey = value; }
        }

        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public Usuario Usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                UserId = value.Id;
            }
        }
    }
}
