using System;
using System.Xml.Serialization;
using Aurum.Entidades.Atributos;
using Aurum.Entidades.Base;

namespace Aurum.Entidades
{
    [XmlRoot(Namespace = "http://utilem.net/Aurum/Entidades/Cartao")]
    public class Cartao : Entidade
    {

        #region Propriedades dos campos da tabela

        public int? CodCartao { get; set; }
        public bool Ativo { get; set; }
        public int? CodConta { get; set; }
        public string Descricao { get; set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public DateTime? Validade { get; set; }

        #endregion Propriedades dos campos da tabela

        #region Propriedades de entidades filhas

        Conta _conta;
        [EntidadeFilha]
        public Conta Conta
        {
            get
            {
                if ( _conta == null )
                    _conta = new Conta();

                return _conta;
            }
        }

        #endregion Propriedades de entidades filhas

    }
}