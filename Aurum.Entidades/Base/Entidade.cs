using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Aurum.Entidades.Atributos;

namespace Aurum.Entidades.Base
{
    public abstract class Entidade
    {
        List<PropertyInfo> _propriedades;

        private List<PropertyInfo> Propriedades
        {
            get
            {
                if ( this._propriedades == null )
                {
                    this._propriedades = new List<PropertyInfo>();

                    var propriedades = this.GetType().GetProperties();

                    foreach ( var propriedade in propriedades )
                    {
                        // Verifica se a propriedade está marcada como sendo uma entidade filha.
                        var atributos = propriedade.GetCustomAttributes(typeof(EntidadeFilhaAttribute), false);

                        // Se for entidade filha, não adiciona a propriedade à lista.
                        if ( !atributos.Any() )
                        {
                            // Verifica se a propriedade está marcada como sendo uma propriedade calculada.
                            atributos = propriedade.GetCustomAttributes(typeof(CalculadaAttribute), false);

                            // Se for propriedade calculada, não a adiciona à lista.
                            if ( !atributos.Any() )
                                this._propriedades.Add(propriedade);
                        }
                    }
                }

                return this._propriedades;
            }
        }


        List<PropertyInfo> _filhas;

        private List<PropertyInfo> Filhas
        {
            get
            {
                if ( this._filhas == null )
                {
                    this._filhas = new List<PropertyInfo>();

                    var propriedades = this.GetType().GetProperties();

                    foreach ( var propriedade in propriedades )
                    {
                        var atributos = propriedade.GetCustomAttributes(typeof(EntidadeFilhaAttribute), false);

                        if ( atributos.Any() )
                            this._filhas.Add(propriedade);
                    }
                }

                return this._filhas;
            }
        }


        public void CarregarCampos(IDataReader dataReader, string prefixo)
        {
            for ( int i = 0; i < dataReader.FieldCount; i++ )
            {
                var nome = dataReader.GetName(i);
                var valor = dataReader.GetValue(i);

                if ( valor != DBNull.Value )
                {
                    var propriedade = this.Propriedades.FirstOrDefault(p => prefixo + p.Name == nome);

                    if ( propriedade != null )
                        propriedade.SetValue(this, valor, null);
                    else
                    {
                        var filha = this.Filhas.FirstOrDefault(f => nome.StartsWith(f.Name));

                        if ( filha != null )
                        {
                            // Carrega a instância da entidade filha relacionada a esta entidade.
                            var entidadeFilha = (Entidade)filha.GetValue(this, null);

                            // Preenche os campos da entidade filha de acordo com as informações do DataReader.
                            entidadeFilha.CarregarCampos(dataReader, filha.Name);
                        }
                    }
                }
            }
        }

        public void CarregarCampos(IDataReader dataReader)
        {
            CarregarCampos(dataReader, String.Empty);
        }


        /// <summary>
        /// Cria uma cópia da entidade, clonando os valores de todas as propriedades (que não sejam de entidades filhas).
        /// </summary>
        /// <typeparam name="T">Deve ser informado o mesmo tipo da própria entidade</typeparam>
        /// <returns></returns>
        public T Clonar<T>() where T : Entidade, new()
        {
            var entidade = new T();

            if ( this.GetType() != entidade.GetType() )
                throw new Exception("O tipo informado é diferente do tipo de origem.");

            foreach ( var propriedade in this.Propriedades )
            {
                if ( propriedade.CanWrite )
                    propriedade.SetValue(entidade, propriedade.GetValue(this, null), null);
            }

            return entidade;
        }

    }
}
