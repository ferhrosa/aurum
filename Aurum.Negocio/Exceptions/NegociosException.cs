using System;

namespace Aurum.Negocio.Exceptions
{
    public class NegociosException : Exception
    {
        public NegociosException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
