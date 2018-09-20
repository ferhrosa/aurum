using System.ComponentModel.DataAnnotations;

namespace Aurum.Modelo
{
    public class Categoria
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

    }
}
