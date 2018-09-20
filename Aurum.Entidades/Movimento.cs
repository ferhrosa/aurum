using System;
using System.Xml.Serialization;
using Aurum.Entidades.Atributos;
using Aurum.Entidades.Base;

namespace Aurum.Entidades
{
    [XmlRoot(Namespace = "http://utilem.net/Aurum/Entidades/Movimento")]
    public class Movimento : Entidade
    {

        #region Propriedades dos campos da tabela

        public long? CodMovimento { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public DateTime Data { get; set; }
        public bool Consolidado { get; set; }
        public DateTime MesAno { get; set; }
        public int CodMovimentoDescricao { get; set; }
        public int CodCategoria { get; set; }
        public int Valor { get; set; }
        public int? CodConta { get; set; }
        public int? CodCartao { get; set; }
        public long? CodMovimentoPrimeiraParcela { get; set; }
        public byte? NumeroParcela { get; set; }
        public byte? TotalParcelas { get; set; }
        public string Observacao { get; set; }

        #endregion Propriedades dos campos da tabela

        #region Propriedades de entidades filhas

        MovimentoDescricao _movimentoDescricao;
        [EntidadeFilha]
        public virtual MovimentoDescricao MovimentoDescricao
        {
            get
            {
                if ( _movimentoDescricao == null )
                    _movimentoDescricao = new MovimentoDescricao();

                return _movimentoDescricao;
            }
            protected set
            {
                _movimentoDescricao = value;
            }
        }

        Categoria _categoria;
        [EntidadeFilha]
        public virtual Categoria Categoria
        {
            get
            {
                if ( _categoria == null )
                    _categoria = new Categoria();

                return _categoria;
            }
        }

        Conta _conta;
        [EntidadeFilha]
        public virtual Conta Conta
        {
            get
            {
                if ( _conta == null )
                    _conta = new Conta();

                return _conta;
            }
        }

        Cartao _cartao;
        [EntidadeFilha]
        public virtual Cartao Cartao
        {
            get
            {
                if ( _cartao == null )
                    _cartao = new Cartao();

                return _cartao;
            }
        }

        Movimento _movimentoPrimeiraParcela;
        [EntidadeFilha]
        public virtual Movimento MovimentoPrimeiraParcela
        {
            get
            {
                if ( _movimentoPrimeiraParcela == null )
                    _movimentoPrimeiraParcela = new Movimento();

                return _movimentoPrimeiraParcela;
            }
        }

        #endregion Propriedades de entidades filhas


        #region Campos calculados

        [Calculada]
        public string ValorFormatado
        {
            get
            {
                return ((double)(this.Valor > 0 ? this.Valor : this.Valor * -1) / 100F).ToString("n2");
            }
            set
            {
                value = value.Replace(".", String.Empty);

                var partes = value.Split(',');

                if ( partes.Length == 0 || partes.Length > 2 )
                    throw new Exception("Valor em formato inválido.");

                var valorTexto = partes[0];

                if ( partes.Length == 2 )
                {
                    var centavos = partes[1];

                    if ( centavos.Length > 2 )
                        centavos = centavos.Substring(0, 2);
                    else if ( centavos.Length == 1 )
                        centavos += "0";
                    else if ( centavos.Length == 0 )
                        centavos = "00";

                    valorTexto += centavos;
                }
                else
                {
                    valorTexto += "00";
                }
                
                int valor;

                if ( int.TryParse(valorTexto, out valor) )
                    this.Valor = valor;
                else
                    throw new FormatException("Valor inválido");
            }
        }

        [Calculada]
        public string ValorMoeda
        {
            get
            {
                return ((double)this.Valor / 100F).ToString("c2");
            }
        }

        #endregion Campos calculados

    }
}