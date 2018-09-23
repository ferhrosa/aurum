using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Modelo.Entidades
{
    public class Cartao
    {

        /// <summary>
        /// ID (único) do Cartão.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Descrição da Conta.
        /// </summary>
        [Required]
        public string Descricao { get; set; }

        /// <summary>
        /// ID da Conta relacionada a este Cartão.
        /// </summary>
        public int? IdConta { get; set; }

        /// <summary>
        /// Conta relacionada a este Cartão.
        /// </summary>
        public Conta Conta { get; set; }

    }
}
