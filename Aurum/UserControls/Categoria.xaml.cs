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
    /// Interaction logic for Categoria.xaml
    /// </summary>
    public partial class Categoria : UserControl {

        bool carregouPrimeiraVez;

        public BD.BancoDados BancoDados { get; set; }

        public Categoria() {
            InitializeComponent();

            carregouPrimeiraVez = false;
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e) {
            LimparCampos();

            gridCampos.Visibility = Visibility.Visible;
            dgCategoria.IsEnabled = false;

            AtualizarControles();
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e) {
            if (null != dgCategoria.SelectedItem) {
                gridCampos.Visibility = Visibility.Visible;

                BD.Categoria categoria = (BD.Categoria)dgCategoria.SelectedItem;
                CarregarCampos(categoria);

                dgCategoria.IsEnabled = false;
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e) {
            if (null != dgCategoria.SelectedItem && MessageBox.Show("Deseja mesmo excluir o registro selecionado?", "Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                BancoDados.DeleteObject((BD.Categoria)dgCategoria.SelectedItem);
                BancoDados.SaveChanges();

                CarregarGrid();
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e) {
            BD.Categoria categoria;

            if (string.IsNullOrWhiteSpace(tbCodCategoria.Text)) {
                categoria = new BD.Categoria();
                BancoDados.AddToCategoria(categoria);
            } else {
                int codCategoria = int.Parse(tbCodCategoria.Text);
                categoria = (from categoriaEditar in BancoDados.Categoria
                             where categoriaEditar.CodCategoria == codCategoria
                             select categoriaEditar).FirstOrDefault();
            }

            Salvar(categoria);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {
            CarregarGrid();
        }

        private void dgCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            AtualizarControles();
        }

        public void CarregarDados() {
            if (!carregouPrimeiraVez) {
                CarregarGrid();
            }

            carregouPrimeiraVez = true;
        }

        private void AtualizarControles() {
            btnAlterar.IsEnabled = (null != dgCategoria.SelectedItem);
            btnExcluir.IsEnabled = (null != dgCategoria.SelectedItem);
            btnAdicionar.IsEnabled = (gridCampos.Visibility != Visibility.Visible);
            btnSalvar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
            btnCancelar.IsEnabled = (gridCampos.Visibility == Visibility.Visible);
        }

        private void CarregarGrid() {
            dgCategoria.ItemsSource = from categoria in BancoDados.Categoria
                                   orderby categoria.Descricao
                                   select categoria;

            gridCampos.Visibility = Visibility.Collapsed;
            dgCategoria.IsEnabled = true;

            AtualizarControles();
        }

        private void Salvar(BD.Categoria categoria) {
            categoria.Ativo = cbAtivo.IsChecked.Value;
            categoria.Descricao = tbDescricao.Text;

            BancoDados.SaveChanges();
            CarregarGrid();
        }

        private void LimparCampos() {
            tbCodCategoria.Clear();
            cbAtivo.IsChecked = true;
            tbDescricao.Clear();
        }

        private void CarregarCampos(BD.Categoria categoria) {
            tbCodCategoria.Text = categoria.CodCategoria.ToString();
            cbAtivo.IsChecked = categoria.Ativo;
            tbDescricao.Text = categoria.Descricao.ToString();
        }

        private void dgCategoria_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            btnAlterar_Click(sender, new RoutedEventArgs());
        }

    }
}
