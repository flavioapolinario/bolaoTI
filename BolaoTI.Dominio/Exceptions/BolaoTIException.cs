using System;

namespace BolaoTI.Dominio.Exceptions
{
    public class BolaoTIException : Exception
    {
        public BolaoTIException(string mensagem)
            : base(mensagem) { }

        public BolaoTIException(string mensagem, Exception exception)
            : base(mensagem, exception) { }
    }
}
