using System.Xml.Serialization;
using Aurum.Entidades.Base;

namespace Aurum.Entidades
{
    [XmlRoot(Namespace = "http://utilem.net/Aurum/Entidades/Conta")]
    public class Conta : Entidade
    {

        #region Propriedades dos campos da tabela

        public int? CodConta { get; set; }
        public bool Ativo { get; set; }
        public string Banco { get; set; }
        public byte Tipo { get; set; }
        public string AgenciaConta { get; set; }

        #endregion Propriedades dos campos da tabela

    }
}