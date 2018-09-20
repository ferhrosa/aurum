using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Negocio.Extensoes
{
    public static class ObjectExtensions
    {
        public static List<PropertyInfo> ListarPropriedades(this object objeto)
        {
            var lista = new List<PropertyInfo>();

            var propriedades = objeto.GetType().GetProperties();

            foreach (var propriedade in propriedades)
            {
                lista.Add(propriedade);
            }

            return lista;
        }

        public static T Clonar<T>(this T objetoOriginal) where T : new()
        {
            var novoObjeto = new T();

            foreach (var propriedade in objetoOriginal.ListarPropriedades())
            {
                if (propriedade.CanWrite)
                    propriedade.SetValue(novoObjeto, propriedade.GetValue(objetoOriginal, null), null);
            }

            return novoObjeto;
        }
    }
}
