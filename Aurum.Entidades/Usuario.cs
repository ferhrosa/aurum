using System.Xml.Serialization;
using Aurum.Entidades.Base;

namespace Aurum.Entidades
{
    [XmlRoot(Namespace = "http://utilem.net/Aurum/Entidades/Conta")]
    public class Usuario : Entidade
    {

        #region Propriedades dos campos da tabela

        public int? CodUsuario { get; set; }
        public bool Ativo { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        #endregion Propriedades dos campos da tabela


        public bool Existe
        {
            get
            {
                return this.CodUsuario.HasValue;
            }
        }
    }
}