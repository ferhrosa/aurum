using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Aurum {
    class ValorConverter : IValueConverter {
        
        public string Converter(double valor) {
            return "R$ " + string.Format("{0:0.00}", (double)valor / 100);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double valor;

            double.TryParse(value.ToString(), out valor);

            return Converter(valor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
