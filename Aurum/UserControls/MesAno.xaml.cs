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
    /// Interaction logic for MesAno.xaml
    /// </summary>
    public partial class MesAno : UserControl {

        public DateTime? Valor {
            get {
                if (cboMes.SelectedIndex < 0 || cboMes.SelectedIndex > 11 || tbAno.Numero <= 0)
                    return null;
                
                return new DateTime((int)tbAno.Numero, cboMes.SelectedIndex + 1, 1);
            }
            set {
                cboMes.SelectedIndex = value.Value.Month - 1;
                tbAno.Text = value.Value.Year.ToString();
            }
        }


        public MesAno() {
            InitializeComponent();
        }
    }
}
