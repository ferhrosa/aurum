using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurum {
    public static class Extensoes {

        public static string MesAno(this DateTime data) {
            string mes = data.ToString("MMMM");
            mes = mes.Substring(0, 1).ToUpper() + mes.Substring(1, mes.Length - 1);

            string ano = string.Empty;
            if (data.Year != DateTime.Now.Year)
                ano += " / " + data.Year.ToString();

            return mes + ano;
        }

    }
}
