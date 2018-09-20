using Aurum.Modelo.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Modelo.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<Aurum.Modelo.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexto contexto)
        {

#if DEBUG

            if (!contexto.Categoria.Any())
            {
                contexto.Categoria.AddRange(new Categoria[] {
                    new Categoria() { Ativo = true, Descricao = "Outros" },
                    new Categoria() { Ativo = true, Descricao = "Alimentação" },
                    new Categoria() { Ativo = true, Descricao = "Carro" },
                });
            }

            contexto.SaveChanges();

            if (!contexto.Carteira.Any())
            {
                var contaCorrente=new Carteira() {
                    Ativo = true,
                    Tipo = TipoCarteira.ContaCorrente,
                    Descricao = "Conta corrente"
                };

                contexto.Carteira.AddRange(new Carteira[] {
                    contaCorrente,
                    new Carteira() { Ativo = true, Tipo = TipoCarteira.Dinheiro, Descricao = "Dinheiro" },
                    new Carteira() { Ativo = true, Tipo = TipoCarteira.ContaPoupanca, Descricao = "Poupança" },
                    new Carteira() {
                        Ativo = true,
                        Tipo = TipoCarteira.CartaoCredito,
                        Descricao = "MasterCard",
                        DiaVencimentoFatura = 15,
                        CarteiraMae = contaCorrente
                    },
                });
            }

            contexto.SaveChanges();

            if (!contexto.Objetivo.Any() && contexto.Carteira.Any(c => c.Tipo == TipoCarteira.ContaPoupanca))
            {
                contexto.Objetivo.AddRange(new Aurum.Modelo.Objetivo[] {
                    new Aurum.Modelo.Objetivo() {
                        Ativo = true,
                        Descricao = "Carro novo",
                        Data = DateTime.Now.AddYears(1),
                        Valor = (10000) * 100,  // os dois últimos dígitos são os centavos.
                        Carteira = contexto.Carteira.First(c => c.Tipo == TipoCarteira.ContaPoupanca)
                    },
                    new Aurum.Modelo.Objetivo() {
                        Ativo = true,
                        Descricao = "Playstation 4",
                        Data = DateTime.Now.AddMonths(16),
                        Valor = (4000) * 100,  // os dois últimos dígitos são os centavos.
                        Carteira = contexto.Carteira.First(c => c.Tipo == TipoCarteira.ContaPoupanca)
                    },
                });
            }

            contexto.SaveChanges();

#endif
        }
    }
}
