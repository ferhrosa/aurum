using System.ComponentModel.DataAnnotations;

namespace Aurum.Modelo.Entidades
{
    /// <summary>
    /// Categoria de movimentação financeira.
    /// </summary>
    public class Categoria
    {

        /// <summary>
        /// ID (único) da Categoria.
        /// </summary>
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// Nome da Categoria.
        /// </summary>
        [Required]
        public string Nome { get; set; }

    }
}
