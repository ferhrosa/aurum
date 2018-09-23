using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aurum.Modelo.Entidades
{
    public class Movimentacao : IValidatableObject
    {
        /// <summary>
        /// ID (único) da Movimentação.
        /// </summary>
        public Guid? Id { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        public int? IdConta { get; set; }

        public Conta Conta { get; set; }

        public int? IdCartao { get; set; }

        public Cartao Cartao { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public bool Efetivada { get; set; }

        [Required]
        public DateTime DataHoraInclusao { get; set; }

        public Guid? IdMovimentacaoPrimeiraParcela { get; set; }

        public Movimentacao MovimentacaoPrimeiraParcela { get; set; }

        public short? NumeroParcela { get; set; }

        public short? TotalParcelas { get; set; }

        public string Observacao { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IdConta.HasValue && !IdConta.HasValue)
            {
                yield return new ValidationResult("Deve ser informado pelo menos um: Conta ou Cartão", new string[] { "IdConta", "IdCartao" });
            }

            if (IdConta.HasValue && IdConta.HasValue)
            {
                yield return new ValidationResult("Somente deve ser informado um: Conta ou Cartão", new string[] { "IdConta", "IdCartao" });
            }
        }
    }
}
