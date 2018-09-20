using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aurum.Modelo
{
    public class Movimentacao
    {
        [Key]
        public long Codigo { get; set; }

        public DateTime Data { get; set; }

        public bool Consolidado { get; set; }

        public DateTime MesAno { get; set; }

        [Display(Name = "Descrição")]
        [ForeignKey("Descricao")]
        public int Descricao_Codigo { get; set; }
        public MovimentacaoDescricao Descricao { get; set; }

        [Display(Name = "Categoria")]
        [ForeignKey("Categoria")]
        public int Categoria_Codigo { get; set; }
        public Categoria Categoria { get; set; }

        public int Valor { get; set; }

        [Display(Name = "Carteira")]
        [ForeignKey("Carteira")]
        public int Carteira_Codigo { get; set; }
        public Carteira Carteira { get; set; }

        [Display(Name = "Objetivo")]
        [ForeignKey("Objetivo")]
        public int? Objetivo_Codigo { get; set; }
        public Objetivo Objetivo { get; set; }

        public Movimentacao PrimeiraParcela { get; set; }

        [Display(Name = "Parcela nº")]
        public short? NumeroParcela { get; set; }

        public short? TotalParcelas { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
