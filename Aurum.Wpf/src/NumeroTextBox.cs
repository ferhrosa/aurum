using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Aurum {
    class NumeroTextBox : TextBox {

        public bool AceitarVirgula { get; set; }

        public double Numero {
            get {
                double numero;
                double.TryParse(Text, out numero);

                return numero;
            }
            set {
                Text = value.ToString();
            }
        }


        public NumeroTextBox()
            : base() {
            this.KeyDown += NumeroTextBox_KeyDown;
        }

        protected void NumeroTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key < Key.D0 || e.Key > Key.D9)
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                    if (!AceitarVirgula || e.Key != Key.OemComma || (this.Text.Contains(",") && !this.SelectedText.Contains(",")))
                        if (!AceitarVirgula || e.Key != Key.Decimal || (this.Text.Contains(",") && !this.SelectedText.Contains(",")))
                            if (e.Key != Key.Tab)
                                if (e.Key != Key.Back || e.Key != Key.Delete)
                                    e.Handled = true;
        }

    }
}
