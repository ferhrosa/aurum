using System.Xml.Serialization;
using Aurum.Entidades.Base;

namespace Aurum.Entidades
{
    [XmlRoot(Namespace = "http://utilem.net/Aurum/Entidades/MovimentoDescricao")]
    public class MovimentoDescricao : Entidade
    {

        #region Propriedades dos campos da tabela

        public int? CodMovimentoDescricao { get; set; }
        public string Descricao { get; set; }

        #endregion Propriedades dos campos da tabela

    }
}