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
    /// Interaction logic for Movimento.xaml
    /// </summary>
    public partial class Movimento : UserControl {

        bool carregouPrimeiraVez;
        DateTime mesAtual;

        public BD.BancoDados BancoDados { get; set; }

        public Movimento() {
            InitializeComponent();

            CarregarAbas(DateTime.Now);

            carregouPrimeiraVez = false;
        }


        #region Abas

        private void SelecionarAba() {
            if (tcMovimento.SelectedIndex > 0)
                CarregarAbas(mesAtual.AddMonths(tcMovimento.SelectedIndex - 3));
        }
        private void SelecionarAba(object sender, MouseButtonEventArgs e) {
            SelecionarAba();
        }
        private void SelecionarAba(object sender, KeyEventArgs e) {
            SelecionarAba();
        }

        private void btnSelecionar_Click(object sender, RoutedEventArgs e) {
            if (maSelecionar.Valor.HasValue)
                CarregarAbas(maSelecionar.Valor.Value);
            else
                MessageBox.Show("Selecione um mês e um ano antes!", "Selecionar mês", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        void CarregarAbas(DateTime dataInicio) {
            List<object> abasRemover = new List<object>();

            for (int i = 1; i < tcMovimento.Items.Count; i++)
                abasRemover.Add(tcMovimento.Items[i]);

            foreach (object item in abasRemover)
                tcMovimento.Items.Remove(item);

            DateTime data = dataInicio.AddMonths(-2);

            while (data <= dataInicio.AddMonths(2)) {
                DataTabItem aba = new DataTabItem();
                aba.Data = data;

                string mes = data.ToString("MMMM");
                mes = mes.Substring(0, 1).ToUpper() + mes.Substring(1, mes.Length - 1);

                aba.Header = mes;
                if (data.Year != DateTime.Now.Year)
                    aba.Header += " / " + data.Year.ToString();

                tcMovimento.Items.Add(aba);

                data = data.AddMonths(1);
            }

            DateTime mesAnterior = mesAtual;
            mesAtual = new DateTime(dataInicio.Year, dataInicio.Month, 1);
            resumo.MesAno = mesAtual;

            tcMovimento.SelectedIndex = 3;

            maSelecionar.Valor = mesAtual;

            if (carregouPrimeiraVez && (mesAtual != mesAnterior)) {
                resumo.CarregarGrids();
                CarregarGrid();
            }
        }

        #endregion


        #region Campos

        #region Eventos

        private void btnAdicionar_Click(object sender, RoutedEventArgs e) {
            LimparCampos();
            CarregarCombos();

            gridCampos.Visibility = Visibility.Visible;
            dgMovimento.IsEnabled = false;

            AtualizarControles();
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e) {
            if (null != dgMovimento.SelectedItem) {
                gridCampos.Visibility = Visibility.Visible;

                CarregarCombos();

                BD.Movimento movimento = (BD.Movimento)dgMovimento.SelectedItem;
                CarregarCampos(movimento);

                dgMovimento.IsEnabled = false;

                AtualizarControles();
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e) {
            if (dgMovimento.SelectedItems.Count > 0 && MessageBox.Show("Deseja mesmo excluir os registros selecionados?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                List<BD.Movimento> excluir = new List<BD.Movimento>();

                foreach (var movimento in (from BD.Movimento movimento in dgMovimento.SelectedItems where movimento.CodMovimentoReserva.HasValue select movimento))
                    excluir.Add(movimento);

                foreach (var movimento in (from BD.Movimento movimento in dgMovimento.SelectedItems select movimento))
                    excluir.Add(movimento);

                foreach (var movimento in excluir)
                    BancoDados.DeleteObject(movimento);

                BancoDados.SaveChanges();

                CarregarGrid();
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e) {
            StringBuilder erros = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbValor.Text))
                erros.AppendLine("O campo \"Valor\" está em branco.");
            if (cboConta.SelectedIndex < 1 && cboCartao.SelectedIndex < 1)
                erros.AppendLine("Não foi selecionada uma conta ou um cartão.");

            if (erros.Length > 0) {
                MessageBox.Show("Os seguintes erros foram encontrados ao salvar:\n\n" + erros.ToString().Trim(), "Erros ao salvar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            BD.Movimento movimento;

            if (string.IsNullOrWhiteSpace(tbCodMovimento.Text)) {
                movimento = new BD.Movimento();
                BancoDados.AddToMovimento(movimento);
            } else {
                int codMovimento = int.Parse(tbCodMovimento.Text);
                movimento = (from movimentoEditar in BancoDados.Movimento
                             where movimentoEditar.CodMovimento == codMovimento
                             select movimentoEditar).FirstOrDefault();
            }

            Salvar(movimento);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {
            CarregarGrid();
        }

        private void btnReplicar_Click(object sender, RoutedEventArgs e) {
            if (dgMovimento.SelectedItems.Count <= 0) {
                MessageBox.Show("Selecione os itens que deseja replicar.", "Replicar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            } else if (MessageBox.Show("Tem certeza de que deseja copiar os " + dgMovimento.SelectedItems.Count.ToString() + " itens selecionados para o próximo mês?\n\n(Verifique se eles ja existem antes para evitar a duplicação.)", "Replicar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                foreach (BD.Movimento movimento in dgMovimento.SelectedItems) {
                    BD.Movimento movimentoNovo = new BD.Movimento();

                    movimentoNovo.Ativo = movimento.Ativo;
                    movimentoNovo.CodCartao = movimento.CodCartao;
                    movimentoNovo.CodCategoria = movimento.CodCategoria;
                    movimentoNovo.CodConta = movimento.CodConta;
                    movimentoNovo.CodMovimentoDescricao = movimento.CodMovimentoDescricao;
                    movimentoNovo.DataHoraInclusao = DateTime.Now;
                    movimentoNovo.MesAno = movimento.MesAno.AddMonths(1);
                    movimentoNovo.Observacao = movimento.Observacao;
                    movimentoNovo.Reserva = movimento.Reserva;
                    movimentoNovo.Valor = movimento.Valor;

                    BancoDados.AddToMovimento(movimentoNovo);
                }

                BancoDados.SaveChanges();

                MessageBox.Show("Os itens foram replicados para o próximo mês com sucesso.", "Replicar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void dgMovimento_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            AtualizarControles();
        }

        private void dgMovimento_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            btnAlterar_Click(sender, new RoutedEventArgs());
        }

        private void dgMovimento_LoadingRow(object sender, DataGridRowEventArgs e) {
            BD.Movimento movimento = (BD.Movimento)e.Row.DataContext;

            if (movimento.Reserva)
                e.Row.Foreground = Brushes.Blue;
            else if (movimento.Valor < 0)
                e.Row.Foreground = Brushes.Red;
            else if (movimento.Valor > 0)
                e.Row.Foreground = Brushes.Green;
        }

        #endregion

        #region Outros

        public void CarregarDados() {
            if (!carregouPrimeiraVez) {
                resumo.BancoDados = BancoDados;
                resumo.CarregarGrids();
                CarregarGrid();
            }

            carregouPrimeiraVez = true;
        }

        private void AtualizarControles() {
            btnAlterar.IsEnabled = (null != dgMovimento.SelectedItem && gridCampos.Visibility != Visibility.Visible);
            btnExcluir.IsEnabled = (null != dgMovimento.SelectedItem && gridCampos.Visibility != Visibility.Visible);
            btnAdicionar.IsEnabled = (gridCampos.Visibility != Visibility.Visible);
            btnSalvar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
            btnCancelar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
            btnReplicar.IsEnabled = (gridCampos.Visibility != Visibility.Visible);
        }

        private void CarregarGrid() {
            dgMovimento.ItemsSource = from movimento in BancoDados.Movimento
                                      where movimento.MesAno.Year == mesAtual.Year && movimento.MesAno.Month == mesAtual.Month
                                      orderby movimento.Reserva descending, movimento.Data, movimento.Cartao.Descricao, movimento.Conta.AgenciaConta, movimento.Categoria.Descricao, movimento.Valor
                                      select movimento;

            gridCampos.Visibility = Visibility.Collapsed;
            dgMovimento.IsEnabled = true;

            AtualizarControles();
        }

        private void CarregarCombos() {
            List<BD.Categoria> categorias = new List<BD.Categoria>();
            categorias.AddRange(from categoria in BancoDados.Categoria
                                orderby categoria.Descricao
                                select categoria);
            categorias.Insert(0, BD.Categoria.CreateCategoria(0, false, string.Empty));
            cboCategoria.ItemsSource = categorias;


            List<BD.MovimentoDescricao> descricoes = new List<BD.MovimentoDescricao>();
            descricoes.AddRange(from descricao in BancoDados.MovimentoDescricao
                                orderby descricao.Movimentos.Count() descending, descricao.Descricao
                                select descricao);
            descricoes.Insert(0, BD.MovimentoDescricao.CreateMovimentoDescricao(0, string.Empty));
            cboDescricao.ItemsSource = descricoes;


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


            List<BD.Movimento> movimentos = new List<BD.Movimento>();
            movimentos.AddRange(from movimento in BancoDados.Movimento
                                where movimento.Reserva == true && movimento.MesAno.Year == mesAtual.Year && movimento.MesAno.Month == mesAtual.Month
                                orderby movimento.MovimentoDescricao.Descricao
                                select movimento);
            movimentos.Insert(0, BD.Movimento.CreateMovimento(0, false, DateTime.Today, DateTime.Today, 0, 0, false));
            cboMovimento.ItemsSource = movimentos;
        }

        private void Salvar(BD.Movimento movimento) {
            BD.MovimentoDescricao movimentoDescricao;

            if (null != cboDescricao.SelectedItem && (int)cboDescricao.SelectedValue > 0 && ((BD.MovimentoDescricao)cboDescricao.SelectedItem).Descricao.Trim() == cboDescricao.Text.Trim())
                movimentoDescricao = (BD.MovimentoDescricao)cboDescricao.SelectedItem;
            else {
                movimentoDescricao = (from descricao in BancoDados.MovimentoDescricao
                                      where descricao.Descricao.Trim().ToLower() == cboDescricao.Text.Trim().ToLower()
                                      select descricao).FirstOrDefault();

                if (null == movimentoDescricao) {
                    movimentoDescricao = BD.MovimentoDescricao.CreateMovimentoDescricao(0, cboDescricao.Text.Trim());
                    BancoDados.AddToMovimentoDescricao(movimentoDescricao);
                }
            }

            int valor = tbValor.Numero;

            if (rbTipoDebito.IsChecked.HasValue && rbTipoDebito.IsChecked.Value)
                valor *= -1;

            movimento.Ativo = cbAtivo.IsChecked.Value;
            movimento.Reserva = cbReserva.IsChecked.Value;
            movimento.Data = dtData.SelectedDate;
            movimento.MesAno = mesAtual;
            //movimento.MesAno = dtMes.SelectedDate.Value;
            movimento.CodCategoria = (null != cboCategoria.SelectedValue && (int)cboCategoria.SelectedValue > 0 ? (int?)cboCategoria.SelectedValue : null);
            //movimento.Valor = (rbTipoDebito.IsChecked.Value ? (int)Math.Floor((double.Parse(tbValor.Text) * 100)) * -1 : (int)Math.Floor((double.Parse(tbValor.Text) * 100)));
            movimento.Valor = valor;
            movimento.MovimentoDescricao = movimentoDescricao;
            movimento.CodConta = (null != cboConta.SelectedValue && (int)cboConta.SelectedValue > 0 ? (int?)cboConta.SelectedValue : null);
            movimento.CodCartao = (null != cboCartao.SelectedValue && (int)cboCartao.SelectedValue > 0 ? (int?)cboCartao.SelectedValue : null);
            movimento.CodMovimentoReserva = (null != cboMovimento.SelectedValue && (int)cboMovimento.SelectedValue > 0 ? (int?)cboMovimento.SelectedValue : null);
            movimento.Observacao = tbObservacao.Text;

            if (movimento.CodMovimento == 0)
                movimento.DataHoraInclusao = DateTime.Now;

            BancoDados.SaveChanges();
            CarregarGrid();
        }

        private void LimparCampos() {
            tbCodMovimento.Clear();
            cbAtivo.IsChecked = true;
            cbReserva.IsChecked = false;
            dtData.SelectedDate = null;
            dtMes.SelectedDate = null;
            cboCategoria.SelectedIndex = 0;
            tbValor.Clear();
            cboDescricao.SelectedIndex = 0;
            cboConta.SelectedIndex = 0;
            cboCartao.SelectedIndex = 0;
            cboMovimento.SelectedIndex = 0;
            tbObservacao.Clear();
        }

        private void CarregarCampos(BD.Movimento movimento) {
            tbCodMovimento.Text = movimento.CodMovimento.ToString();
            cbAtivo.IsChecked = movimento.Ativo;
            cbReserva.IsChecked = movimento.Reserva;
            dtData.SelectedDate = movimento.Data;
            dtMes.SelectedDate = movimento.MesAno;
            cboCategoria.SelectedValue = movimento.CodCategoria;
            rbTipoCredito.IsChecked = (movimento.Valor >= 0);
            rbTipoDebito.IsChecked = (movimento.Valor < 0);
            tbValor.Numero = Math.Abs(movimento.Valor);
            cboDescricao.SelectedValue = movimento.CodMovimentoDescricao;
            cboConta.SelectedValue = movimento.CodConta;
            cboCartao.SelectedValue = movimento.CodCartao;
            cboMovimento.SelectedValue = movimento.CodMovimentoReserva;
            tbObservacao.Text = movimento.Observacao;
        }

        #endregion

        private void dgMovimento_Sorting(object sender, DataGridSortingEventArgs e) {
            MessageBox.Show(e.Column.SortMemberPath);
        }

        #endregion

    }
}
