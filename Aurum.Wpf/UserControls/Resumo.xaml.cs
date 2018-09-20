using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Aurum.Wpf.UserControls {
    /// <summary>
    /// Interaction logic for Resumo.xaml
    /// </summary>
    public partial class Resumo : UserControl {

        bool carregouPrimeiraVez;

        ValorConverter valorConverter;

        public BD.BancoDados BancoDados { get; set; }
        public DateTime? MesAno { get; set; }
        public bool Reduzido { get; set; }


        public Resumo() {
            InitializeComponent();
        }


        public void CarregarDados() {
            if (!carregouPrimeiraVez) {
                CarregarGrids();
            }

            carregouPrimeiraVez = true;
        }


        public void CarregarGrids() {
            
            dgConta.ItemsSource =
                from conta in BancoDados.Conta
                group new { conta }
                   by new { Conta = conta.AgenciaConta }
                   into grupo
                   select new
                   {
                       grupo.Key.Conta,
                       Total =
                           grupo.Sum(c =>
                               c.conta.Movimentos.Count == 0
                               ? 0
                               : (
                                   c.conta.Movimentos.Sum(movimento =>
                                       (!MesAno.HasValue || (movimento.MesAno.Year == MesAno.Value.Year && movimento.MesAno.Month == MesAno.Value.Month))
									   && (!Reduzido || null != movimento.Data)
                                       ? (
                                           !Reduzido && movimento.Reserva
                                           ? (
                                               movimento.MovimentosDestaReserva.Sum(mr => mr.Valor) < movimento.Valor
                                               ? movimento.MovimentosDestaReserva.Sum(mr => mr.Valor)
                                               : movimento.Valor
                                           )
                                           : (
                                               Reduzido || null == movimento.MovimentoReserva
                                               ? movimento.Valor
                                               : 0
                                           )
                                       )
                                       : 0
                                   )
                               )
                           )
                           + grupo.Sum(c =>
                               c.conta.Cartoes.Count == 0
                               ? 0
                               : (
                                   c.conta.Cartoes.Sum(r =>
                                       r.Movimentos.Count == 0
                                       ? 0
                                       : (
                                           r.Movimentos.Sum(movimento =>
                                               (!MesAno.HasValue || (movimento.MesAno.Year == MesAno.Value.Year && movimento.MesAno.Month == MesAno.Value.Month))
											   && (!Reduzido || null != movimento.Data)
                                               ? (
                                                   !Reduzido && movimento.Reserva
                                                   ? (
                                                       movimento.MovimentosDestaReserva.Sum(mr => mr.Valor) < movimento.Valor
                                                       ? movimento.MovimentosDestaReserva.Sum(mr => mr.Valor)
                                                       : movimento.Valor
                                                   )
                                                   : (
                                                       Reduzido || null == movimento.MovimentoReserva
                                                       ? movimento.Valor
                                                       : 0
                                                   )
                                               )
                                               : 0
                                           )
                                       )
                                   )
                               )
                           )
                   };
            
            
            if (!Reduzido) {

                dgCartao.ItemsSource =
                    from cartao in BancoDados.Cartao
                    group cartao by new { Cartao = cartao.Descricao } into grupo
                    select new {
                        grupo.Key.Cartao,
                        Total =
                            grupo.Sum(cartao =>
                                cartao.Movimentos.Count == 0
                                ? 0
                                : (
                                    cartao.Movimentos.Sum(movimento =>
                                        (!MesAno.HasValue || (movimento.MesAno.Year == MesAno.Value.Year && movimento.MesAno.Month == MesAno.Value.Month))
                                        ? movimento.Valor
										// ? (
                                            // movimento.Reserva
                                            // ? (
                                                // movimento.MovimentosDestaReserva.Sum(mr => mr.Valor) < movimento.Valor
                                                // ? movimento.MovimentosDestaReserva.Sum(mr => mr.Valor)
                                                // : movimento.Valor
                                            // )
                                            // : (
                                                // null == movimento.MovimentoReserva
                                                // ? movimento.Valor
                                                // : 0
                                            // )
                                        // )
                                        : 0
                                    )
                                )
                            )
                    };

                dgCategoria.ItemsSource =
                    from movimento in BancoDados.Movimento
                    where (!MesAno.HasValue || (movimento.MesAno.Year == MesAno.Value.Year && movimento.MesAno.Month == MesAno.Value.Month))
                    group movimento
                        by movimento.Categoria.Descricao
                        into grupo
                        orderby grupo.Sum(p => p.Valor)
                        select new {
                            Categoria = grupo.Key,
                            Total = grupo.Sum(p => p.Valor)
                        };


                dgReserva.ItemsSource =
                    from movimento in BancoDados.Movimento
                    where movimento.Reserva && (!MesAno.HasValue || (movimento.MesAno.Year == MesAno.Value.Year && movimento.MesAno.Month == MesAno.Value.Month))
                    select new {
                        Reserva = movimento.MovimentoDescricao.Descricao,
                        Total = Math.Abs(movimento.Valor) + (movimento.MovimentosDestaReserva.Count > 0 ? movimento.MovimentosDestaReserva.Sum(m => m.Valor) : 0)
                    };
            }
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e) {
            CarregarGrids();
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e) {
            dynamic registro = e.Row.DataContext;

            if (registro.Total < 0)
                e.Row.Foreground = Brushes.Red;
            else if (registro.Total > 0)
                e.Row.Foreground = Brushes.Green;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            valorConverter = (ValorConverter)Resources["ValorConverter"];
            
            if (Reduzido) {
                btnAtualizar.Visibility = Visibility.Collapsed;
                lblMesAno.Visibility = Visibility.Visible;
                lblMesAno.Content = MesAno.Value.MesAno();
                dgCartao.Visibility = Visibility.Collapsed;
                dgCategoria.Visibility = Visibility.Collapsed;
                dgReserva.Visibility = Visibility.Collapsed;
            }
        }

        private void CalcularTotal() {
            if (Reduzido && null != dgConta.SelectedItem) {
                int valorConta = ((dynamic)dgConta.SelectedItem).Total;
                int total = (valorConta - tbBanco.Numero - tbDinheiro.Numero);

                tbTotal.Text = valorConverter.Converter(total);

                if (total == 0)
                    tbTotal.Foreground = Brushes.Black;
                else if (total > 0)
                    tbTotal.Foreground = Brushes.Green;
                else if (total < 0)
                    tbTotal.Foreground = Brushes.Red;
               
                gridCalculo.Visibility = Visibility.Visible;
            }
        }

        private void CalcularTotal(object sender, TextChangedEventArgs e) {
            CalcularTotal();
        }
        private void CalcularTotal(object sender, SelectionChangedEventArgs e) {
            CalcularTotal();
        }
    }
}
