using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aurum.Modelo
{
    public class Objetivo
    {
        [Key]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(50, ErrorMessage = "A descrição deve ter no máximo 50 caracteres.")]
        [StringLength(50, ErrorMessage = "A descrição deve ter no máximo 50 caracteres.")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "Concluído")]
        public bool Concluido { get; set; }

        [Display(Name = "Valor Total")]
        public int Valor { get; set; }

        [ForeignKey("Carteira")]
        [Display(Name = "Carteira Vinculada")]
        public int Carteira_Codigo { get; set; }
        public Carteira Carteira { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data de término é obrigatória.")]
        [Display(Name = "Data Término")]
        public DateTime Data { get; set; }
    }
}
