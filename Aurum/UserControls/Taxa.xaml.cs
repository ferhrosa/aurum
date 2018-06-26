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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aurum.UserControls {
    /// <summary>
    /// Interaction logic for Cartao.xaml
    /// </summary>
    public partial class Taxa : UserControl {

        bool carregouPrimeiraVez;

        public BD.BancoDados BancoDados { get; set; }

        public Taxa() {
            InitializeComponent();

            carregouPrimeiraVez = false;
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e) {
            LimparCampos();
            CarregarCombos();

            gridCampos.Visibility = Visibility.Visible;
            dgTaxa.IsEnabled = false;

            AtualizarControles();
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e) {
            if (null != dgTaxa.SelectedItem) {
                gridCampos.Visibility = Visibility.Visible;

                CarregarCombos();

                BD.Taxa taxa = (BD.Taxa)dgTaxa.SelectedItem;
                CarregarCampos(taxa);

                dgTaxa.IsEnabled = false;
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e) {
            if (null != dgTaxa.SelectedItem && MessageBox.Show("Deseja mesmo excluir o registro selecionado?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                BancoDados.DeleteObject((BD.Taxa)dgTaxa.SelectedItem);
                BancoDados.SaveChanges();

                CarregarGrid();
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e) {
            BD.Taxa taxa;

            if (string.IsNullOrWhiteSpace(tbCodTaxa.Text)) {
                taxa = new BD.Taxa();
                BancoDados.AddToTaxa(taxa);
            } else {
                int codTaxa = int.Parse(tbCodTaxa.Text);
                taxa = (from taxaEditar in BancoDados.Taxa
                          where taxaEditar.CodTaxa == codTaxa
                          select taxaEditar).FirstOrDefault();
            }

            Salvar(taxa);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {
            CarregarGrid();
        }

        private void dgTaxa_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            AtualizarControles();
        }

        public void CarregarDados() {
            if (!carregouPrimeiraVez) {
                CarregarGrid();
            }

            carregouPrimeiraVez = true;
        }

        private void AtualizarControles() {
            btnAlterar.IsEnabled = (null != dgTaxa.SelectedItem);
            btnExcluir.IsEnabled = (null != dgTaxa.SelectedItem);
            btnAdicionar.IsEnabled = (gridCampos.Visibility != Visibility.Visible);
            btnSalvar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
            btnCancelar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
        }

        private void CarregarGrid() {
            dgTaxa.ItemsSource = from taxa in BancoDados.Taxa
                                   select taxa;

            gridCampos.Visibility = Visibility.Collapsed;
            dgTaxa.IsEnabled = true;

            AtualizarControles();
        }

        private void CarregarCombos() {
            List<BD.Conta> contas = new List<BD.Conta>();
            contas.AddRange(from conta in BancoDados.Conta
                            orderby conta.AgenciaConta
                            select conta);

            contas.Insert(0, BD.Conta.CreateConta(0, false, string.Empty, 0, string.Empty));

            cboConta.ItemsSource = contas;

            List<BD.Cartao> cartoes = new List<BD.Cartao>();
            cartoes.AddRange(from cartao in BancoDados.Cartao
                            orderby cartao.Descricao
                            select cartao);

            cartoes.Insert(0, BD.Cartao.CreateCartao(0, false, string.Empty, string.Empty, string.Empty, 0, false));

            cboCartao.ItemsSource = cartoes;
        }

        private void Salvar(BD.Taxa taxa) {
            taxa.Ativo = cbAtivo.IsChecked.Value;
            taxa.CodConta = (null != cboConta.SelectedValue && (int)cboConta.SelectedValue > 0 ? (int?)cboConta.SelectedValue : null);
            taxa.CodCartao = (null != cboCartao.SelectedValue && (int)cboCartao.SelectedValue > 0 ? (int?)cboCartao.SelectedValue : null);
            taxa.Valor = (int)Math.Floor((double.Parse(tbValor.Text) * 100));

            BancoDados.SaveChanges();
            CarregarGrid();
        }

        private void LimparCampos() {
            tbCodTaxa.Clear();
            cbAtivo.IsChecked = true;
            cboConta.SelectedIndex = 0;
            cboCartao.SelectedIndex = 0;
            tbValor.Clear();
        }

        private void CarregarCampos(BD.Taxa taxa) {
            tbCodTaxa.Text = taxa.CodTaxa.ToString();
            cbAtivo.IsChecked = taxa.Ativo;
            cboConta.SelectedValue = taxa.CodConta;
            cboCartao.SelectedValue = taxa.CodCartao;
            tbValor.Text = string.Format("{0:0.00}", (double)taxa.Valor / 100);
        }

        private void tbValor_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key < Key.D0 || e.Key > Key.D9)
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                    if (e.Key != Key.OemComma || tbValor.Text.Contains(","))
                        if (e.Key != Key.Back || e.Key != Key.Delete)
                            e.Handled = true;
        }

        private void dgTaxa_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            btnAlterar_Click(sender, new RoutedEventArgs());
        }

    }
}
