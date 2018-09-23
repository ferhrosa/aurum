using Aurum.Modelo.Enums;
using System.ComponentModel.DataAnnotations;

namespace Aurum.Modelo.Entidades
{
    /// <summary>
    /// Conta
    /// </summary>
    public class Conta
    {

        /// <summary>
        /// ID (único) da Conta.
        /// </summary>
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// Descrição da Conta.
        /// </summary>
        [Required]
        public string Descricao { get; set; }

        /// <summary>
        /// Tipo de Conta.
        /// </summary>
        [Required]
        public TipoConta Tipo { get; set; }

        /// <summary>
        /// Número de agência e da conta.
        /// </summary>
        public string AgenciaConta { get; set; }

    }
}
