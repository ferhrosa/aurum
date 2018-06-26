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
using Aurum.UserControls;

namespace Aurum {
    /// <summary>
    /// Interaction logic for Principal.xaml
    /// </summary>
    public partial class Principal : Window {

        public BD.BancoDados BancoDados { get; set; }


        public Principal() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            BancoDados = new BD.BancoDados();

            // Coloca como mês inicial o próprio mês atual.
            maInicio.Valor = DateTime.Now;
            // Coloca como mês final a data atual e mais dois meses, fazendo com
            // que por padrão mostre os resumos de 3 meses, a partir do atual.
            maFim.Valor = DateTime.Now.AddMonths(2);

            conta.BancoDados = BancoDados;
            cartao.BancoDados = BancoDados;
            categoria.BancoDados = BancoDados;
            taxa.BancoDados = BancoDados;
            movimento.BancoDados = BancoDados;
        }

        private void tcPrincipal_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch (tcPrincipal.SelectedIndex) {
                case 1: movimento.CarregarDados(); break;
                case 2: conta.CarregarDados(); break;
                case 3: cartao.CarregarDados(); break;
                case 4: categoria.CarregarDados(); break;
                case 5: taxa.CarregarDados(); break;
            }
        }

        private void btnMostrar_Click(object sender, RoutedEventArgs e) {
            spResumos.Children.Clear();

            if (maInicio.Valor.HasValue && maFim.Valor.HasValue && maInicio.Valor.Value <= maFim.Valor.Value) {
                // Guarda a data do resumo em uma variável.
                DateTime data = maInicio.Valor.Value;

                // Verifica se já passou do mês final. Caso positivo, não cria mais resumos.
                while (data <= maFim.Valor.Value) {
                    // Cria um controle de resumo, mostrando somente de forma reduzida.
                    Resumo resumo = new Resumo();
                    resumo.BancoDados = BancoDados;
                    resumo.Reduzido = true;
                    resumo.MesAno = data;

                    resumo.CarregarDados();

                    spResumos.Children.Add(resumo);

                    // Adiciona um mês à data dos resumos, fazendo com que mostre na próxima execução do laço o próximo mês.
                    data = data.AddMonths(1);
                }
            }
        }
    }

}
