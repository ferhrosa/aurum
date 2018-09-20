using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Aurum.Modelo
{
    public class MovimentacaoDescricao
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(50)]
        public string Descricao { get; set; }
    }
}
