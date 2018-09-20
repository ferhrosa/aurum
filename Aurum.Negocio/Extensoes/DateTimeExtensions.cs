using System;

namespace Aurum.Negocio.Extensoes
{
    public static class DateTimeExtensions
    {
        public static int MesAno(this DateTime data)
        {
            return Int32.Parse(data.ToString("yyyyMM"));
        }
    }
}
