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

namespace Aurum.UserControls
{
    /// <summary>
    /// Interaction logic for Cartao.xaml
    /// </summary>
    public partial class Cartao : UserControl
    {

        bool carregouPrimeiraVez;

        public BD.BancoDados BancoDados { get; set; }

        public Cartao()
        {
            InitializeComponent();

            carregouPrimeiraVez = false;
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            LimparCampos();
            CarregarComboConta();

            gridCampos.Visibility = Visibility.Visible;
            dgCartao.IsEnabled = false;

            AtualizarControles();
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            if ( null != dgCartao.SelectedItem )
            {
                gridCampos.Visibility = Visibility.Visible;

                CarregarComboConta();

                BD.Cartao cartao = (BD.Cartao)dgCartao.SelectedItem;
                CarregarCampos(cartao);

                dgCartao.IsEnabled = false;
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if ( null != dgCartao.SelectedItem && MessageBox.Show("Deseja mesmo excluir o registro selecionado?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes )
            {
                BancoDados.DeleteObject((BD.Cartao)dgCartao.SelectedItem);
                BancoDados.SaveChanges();

                CarregarGrid();
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            BD.Cartao cartao;

            if ( string.IsNullOrWhiteSpace(tbCodCartao.Text) )
            {
                cartao = new BD.Cartao();
                BancoDados.AddToCartao(cartao);
            }
            else
            {
                int codCartao = int.Parse(tbCodCartao.Text);
                cartao = (from cartaoEditar in BancoDados.Cartao
                          where cartaoEditar.CodCartao == codCartao
                          select cartaoEditar).FirstOrDefault();
            }

            Salvar(cartao);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            CarregarGrid();
        }

        private void dgCartao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AtualizarControles();
        }

        public void CarregarDados()
        {
            if ( !carregouPrimeiraVez )
            {
                CarregarGrid();
            }

            carregouPrimeiraVez = true;
        }

        private void AtualizarControles()
        {
            btnAlterar.IsEnabled = (null != dgCartao.SelectedItem);
            btnExcluir.IsEnabled = (null != dgCartao.SelectedItem);
            btnAdicionar.IsEnabled = (gridCampos.Visibility != Visibility.Visible);
            btnSalvar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
            btnCancelar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
        }

        private void CarregarGrid()
        {
            dgCartao.ItemsSource = from cartao in BancoDados.Cartao
                                   orderby cartao.Numero
                                   select cartao;

            gridCampos.Visibility = Visibility.Collapsed;
            dgCartao.IsEnabled = true;

            AtualizarControles();
        }

        private void CarregarComboConta()
        {
            List<BD.Conta> contas = new List<BD.Conta>();
            contas.AddRange(from conta in BancoDados.Conta
                            orderby conta.AgenciaConta
                            select conta);

            contas.Insert(0, BD.Conta.CreateConta(0, false, string.Empty, 0, string.Empty));

            cboConta.ItemsSource = contas;
        }

        private void Salvar(BD.Cartao cartao)
        {
            cartao.Ativo = cbAtivo.IsChecked.Value;
            cartao.CodConta = (null != cboConta.SelectedItem && (int)cboConta.SelectedValue > 0 ? (int?)cboConta.SelectedValue : null);
            cartao.Descricao = tbDescricao.Text;
            cartao.Numero = tbNumero.Text;
            cartao.Titular = tbTitular.Text;
            cartao.Validade = dtValidade.SelectedDate;
            cartao.Vencimento = byte.Parse(tbVencimento.Text);
            cartao.TelefoneSac = tbSAC.Text;
            cartao.PossuiAdicional = cbAdicional.IsChecked.Value;

            BancoDados.SaveChanges();

            CarregarGrid();
        }

        private void LimparCampos()
        {
            tbCodCartao.Clear();
            cbAtivo.IsChecked = true;
            cboConta.SelectedIndex = 0;
            tbDescricao.Clear();
            tbNumero.Clear();
            tbTitular.Clear();
            dtValidade.SelectedDate = null;
            tbVencimento.Clear();
            tbSAC.Clear();
            cbAdicional.IsChecked = false;
        }

        private void CarregarCampos(BD.Cartao cartao)
        {
            tbCodCartao.Text = cartao.CodCartao.ToString();
            cbAtivo.IsChecked = cartao.Ativo;
            cboConta.SelectedValue = cartao.CodConta;
            tbDescricao.Text = cartao.Descricao.ToString();
            tbNumero.Text = cartao.Numero.ToString();
            tbTitular.Text = cartao.Titular.ToString();
            dtValidade.SelectedDate = cartao.Validade;
            tbVencimento.Text = cartao.Vencimento.ToString();
            tbSAC.Text = cartao.TelefoneSac.ToString();
            cbAdicional.IsChecked = cartao.PossuiAdicional;
        }

        private void tbVencimento_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key < Key.D0 || e.Key > Key.D9 )
                if ( e.Key < Key.NumPad0 || e.Key > Key.NumPad9 )
                    if ( e.Key != Key.Back || e.Key != Key.Delete )
                        e.Handled = true;
        }

        private void dgCartao_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnAlterar_Click(sender, new RoutedEventArgs());
        }

    }
}
