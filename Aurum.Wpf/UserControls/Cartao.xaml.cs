using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Aurum.Wpf.ServicoAurum;

namespace Aurum.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for Cartao.xaml
    /// </summary>
    public partial class Cartao : UserControl
    {

        bool carregouPrimeiraVez;

        public AurumClient Servico { get; set; }

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

                CarregarCampos((ServicoAurum.Cartao)dgCartao.SelectedItem);

                dgCartao.IsEnabled = false;
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if ( null != dgCartao.SelectedItem && MessageBox.Show("Deseja mesmo excluir o registro selecionado?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes )
            {
                Servico.ExcluirCartao(((ServicoAurum.Cartao)dgCartao.SelectedItem).CodCartao.Value);

                CarregarGrid();
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if ( string.IsNullOrWhiteSpace(tbCodCartao.Text) )
                Salvar(new ServicoAurum.Cartao());
            else
                Salvar((ServicoAurum.Cartao)dgCartao.SelectedItem);
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
            dgCartao.ItemsSource = Servico.ListarCartao();

            gridCampos.Visibility = Visibility.Collapsed;
            dgCartao.IsEnabled = true;

            AtualizarControles();
        }

        private void CarregarComboConta()
        {
            List<ServicoAurum.Conta> contas = new List<ServicoAurum.Conta>(Servico.ListarConta());

            contas.Insert(0, new ServicoAurum.Conta());

            cboConta.ItemsSource = contas;
        }

        private void Salvar(ServicoAurum.Cartao cartao)
        {
            cartao.CodCartao = Servico.SalvarCartao(
                cartao.CodCartao,
                cbAtivo.IsChecked.Value,
                (null != cboConta.SelectedItem && (int)cboConta.SelectedValue > 0 ? (int?)cboConta.SelectedValue : null),
                tbDescricao.Text,
                tbNumero.Text,
                tbTitular.Text,
                byte.Parse(tbVencimento.Text),
                dtValidade.SelectedDate,
                cbAdicional.IsChecked.Value,
                tbSAC.Text
            );

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

        private void CarregarCampos(ServicoAurum.Cartao cartao)
        {
            tbCodCartao.Text = cartao.CodCartao.ToString();
            cbAtivo.IsChecked = cartao.Ativo;
            cboConta.SelectedValue = cartao.CodConta;
            tbDescricao.Text = cartao.Descricao.ToString();
            tbNumero.Text = cartao.Numero.ToString();
            tbTitular.Text = cartao.Titular.ToString();
            dtValidade.SelectedDate = cartao.Validade;
            tbVencimento.Text = cartao.Vencimento.ToString();
            tbSAC.Text = cartao.TelefoneSac;
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
