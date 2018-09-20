using System.Xml.Serialization;
using Aurum.Entidades.Base;

namespace Aurum.Entidades
{
    [XmlRoot(Namespace="http://utilem.net/Aurum/Entidades/Categoria")]
    public class Categoria : Entidade
    {

        #region Propriedades dos campos da tabela

        public int? CodCategoria { get; set; }
        public bool Ativo { get; set; }
        public string Descricao { get; set; }

        #endregion Propriedades dos campos da tabela

    }
}