using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Aurum {
    class ValorTextBox : NumeroTextBox {

        private new bool AceitarVirgula {
            get {
                return true;
            }
        }

        public new int Numero {
            get {
                if (string.IsNullOrWhiteSpace(Text) || Text.Trim() == ",")
                    return 0;

                List<string> partes = new List<string>();
                partes.AddRange(Text.Split(','));

                if (partes.Count == 1)
                    partes.Add("00");
                else if (string.IsNullOrWhiteSpace(partes[1]))
                    partes[1] = "00";
                else if (partes[1].Length == 1)
                    partes[1] += "0";

                int resultado = 0;
                int.TryParse(partes[0] + partes[1], out resultado);

                return resultado;
            }
            set {
                Text = ((double)value / 100).ToString("0.00");
            }
        }


        public ValorTextBox() : base() {
            base.AceitarVirgula = true;
            this.KeyDown += ValorTextBox_KeyDown;
        }

        private void ValorTextBox_KeyDown(object sender, KeyEventArgs e) {
            base.NumeroTextBox_KeyDown(sender, e);

            if (this.Text.Contains(",")
                && this.Text.Split(',')[1].Length >= 2
                && !this.SelectedText.Contains(",")
                && (
                    this.SelectionLength == 0 
                    && this.SelectionStart > this.Text.IndexOf(",")
                )
            ) {
                if (e.Key != Key.Tab)
                    if (e.Key != Key.Back || e.Key != Key.Delete)
                        e.Handled = true;
            }
        }


    }
}
