using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Aurum {
    class ContaTipoConverter : IValueConverter {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int codConta;

            int.TryParse(value.ToString(), out codConta);

            switch (codConta) {
                case 1: return "Corrente";
                case 2:return "Poupança";
                default: return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
