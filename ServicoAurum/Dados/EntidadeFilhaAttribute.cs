using System;

namespace ServicoAurum.Dados
{
    /// <summary>
    /// Diz que a propriedade de uma entidade é uma entidade filha dessa.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class EntidadeFilhaAttribute : Attribute
    {
    }
}