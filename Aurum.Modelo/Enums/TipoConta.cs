using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Modelo.Enums
{
    public enum TipoConta : byte
    {

        [Display(Name = "Conta Corrente")]
        Corrente = 1,

        [Display(Name = "Conta Poupança")]
        Poupanca = 2

    }
}
