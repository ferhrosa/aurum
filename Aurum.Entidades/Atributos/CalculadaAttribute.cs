using System;

namespace Aurum.Entidades.Atributos
{
    /// <summary>
    /// Diz que a propriedade é calculada em seu acesso, ou seja, não armazena valor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class CalculadaAttribute : Attribute
    {
    }
}