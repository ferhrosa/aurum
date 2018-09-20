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

namespace Aurum.Wpf.UserControls {
    /// <summary>
    /// Interaction logic for Conta.xaml
    /// </summary>
    public partial class Conta : UserControl {

        bool carregouPrimeiraVez;


        public enum Tipo {
            Corrente = 1,
            Poupanca = 2
        }

        public BD.BancoDados BancoDados { get; set; }

        public Conta() {
            InitializeComponent();

            carregouPrimeiraVez = false;
        }


        private void btnAdicionar_Click(object sender, RoutedEventArgs e) {
            LimparCampos();
            
            gridCampos.Visibility = Visibility.Visible;
            dgConta.IsEnabled = false;

            AtualizarControles();
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e) {
            if (null != dgConta.SelectedItem) {
                gridCampos.Visibility = Visibility.Visible;

                BD.Conta conta = (BD.Conta)dgConta.SelectedItem;
                CarregarCampos(conta);

                dgConta.IsEnabled = false;
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e) {
            if (null != dgConta.SelectedItem && MessageBox.Show("Deseja mesmo excluir o registro selecionado?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                BancoDados.DeleteObject((BD.Conta)dgConta.SelectedItem);
                BancoDados.SaveChanges();

                CarregarGrid();
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e) {
            BD.Conta conta;

            if (string.IsNullOrWhiteSpace(tbCodConta.Text)) {
                conta = new BD.Conta();
                BancoDados.AddToConta(conta);
            } else {
                int codConta = int.Parse(tbCodConta.Text);
                conta = (from contaEditar in BancoDados.Conta
                         where contaEditar.CodConta == codConta
                         select contaEditar).FirstOrDefault();
            }

            Salvar(conta);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {
            CarregarGrid();
        }

        private void dgConta_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            AtualizarControles();
        }


        public void CarregarDados() {
            if (!carregouPrimeiraVez)
                CarregarGrid();

            carregouPrimeiraVez = true;
        }
        
        private void AtualizarControles() {
            btnAlterar.IsEnabled = (null != dgConta.SelectedItem);
            btnExcluir.IsEnabled = (null != dgConta.SelectedItem);
            btnAdicionar.IsEnabled = (gridCampos.Visibility != Visibility.Visible);
            btnSalvar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
            btnCancelar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
        }

        private void CarregarGrid() {
            dgConta.ItemsSource = from conta in BancoDados.Conta
                                  orderby conta.Banco, conta.AgenciaConta
                                  select conta;

            gridCampos.Visibility = Visibility.Collapsed;
            dgConta.IsEnabled = true;

            AtualizarControles();
        }

        private void Salvar(BD.Conta conta) {
            conta.Ativo = cbAtivo.IsChecked.Value;
            conta.Banco = tbBanco.Text;
            conta.Tipo = (rbTipoPoupanca.IsChecked.Value ? (byte)Tipo.Poupanca : (byte)Tipo.Corrente);
            conta.AgenciaConta = tbAgenciaConta.Text;

            BancoDados.SaveChanges();

            CarregarGrid();
        }

        private void LimparCampos() {
            tbCodConta.Clear();
            cbAtivo.IsChecked = true;
            tbBanco.Clear();
            rbTipoCorrente.IsChecked = true;
            rbTipoPoupanca.IsChecked = false;
            tbAgenciaConta.Clear();
        }

        private void CarregarCampos(BD.Conta conta) {
            tbCodConta.Text = conta.CodConta.ToString();
            cbAtivo.IsChecked = conta.Ativo;
            tbBanco.Text = conta.Banco;
            rbTipoCorrente.IsChecked = (conta.Tipo == (byte)Tipo.Corrente);
            rbTipoPoupanca.IsChecked = (conta.Tipo == (byte)Tipo.Poupanca);
            tbAgenciaConta.Text = conta.AgenciaConta;
        }

        private void dgConta_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            btnAlterar_Click(sender, new RoutedEventArgs());
        }

    }
}
