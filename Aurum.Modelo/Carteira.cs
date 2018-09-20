using Aurum.Modelo.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aurum.Modelo
{
    public class Carteira
    {
        [Key]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório")]
        public TipoCarteira Tipo { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(30, ErrorMessage = "A descrição deve ter no máximo 30 caracteres.")]
        [StringLength(30, ErrorMessage = "A descrição deve ter no máximo 30 caracteres.")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "Vencimento")]
        public int? DiaVencimentoFatura { get; set; }

        [Display(Name = "Carteira Vinculada")]
        [ForeignKey("CarteiraMae")]
        public int? CarteiraMae_Codigo { get; set; }
        public Carteira CarteiraMae { get; set; }
    }
}
