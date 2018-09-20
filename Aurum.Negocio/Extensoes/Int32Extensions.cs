using System;

namespace Aurum.Negocio.Extensoes
{
    public static class Int32Extensions
    {
        public static DateTime? MesAno(this int mesAno)
        {
            DateTime? data = null;

            if (mesAno.ToString().Length == 6)
            {
                var ano = Int32.Parse(mesAno.ToString().Substring(0, 4));
                var mes = Int32.Parse(mesAno.ToString().Substring(4, 2));

                if (mes >= 1 && mes <= 12)
                {
                    data = new DateTime(ano, mes, 1);
                }
            }

            return data;
        }
    }
}
