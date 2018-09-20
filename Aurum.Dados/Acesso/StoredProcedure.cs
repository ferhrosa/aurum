using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Aurum.Dados.Base;

namespace Aurum.Dados.Acesso
{
    public class StoredProcedure : Comando
    {
        int? _retorno;

        public new string CommandText
        {
            get
            {
                return base.CommandText;
            }
            set
            {
                base.CommandText = value;

                if ( this._comando is SqlCommand )
                {
                    this.Connection.Open();

                    SqlCommandBuilder.DeriveParameters((SqlCommand)this._comando);

                    this.Connection.Close();
                }
            }
        }

        public new CommandType CommandType
        {
            get
            {
                return CommandType.StoredProcedure;
            }
            protected set
            {
                base.CommandType = CommandType.StoredProcedure;
            }
        }

        [Obsolete("Usar os métodos CarregarParametro e DefinirParametro.")]
        public new IDataParameterCollection Parameters
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Valor de retorno do stored procedure.
        /// </summary>
        public int? Retorno
        {
            get
            {
                return _retorno;
            }
            private set
            {
                this._retorno = value;
            }
        }


        public StoredProcedure()
        {
        }

        public StoredProcedure(string nome)
        {
            base.CommandType = CommandType.StoredProcedure;
            this.CommandText = nome;
        }


        public void DefinirParametro(string nome, object valor)
        {
            var parametro = (IDbDataParameter)base.Parameters[nome];

            // Se for um valor string e não for nulo, mas estiver em branco, torna-o nulo.
            if ( valor is string && valor != null && String.IsNullOrEmpty(((string)valor).Trim()) )
                valor = null;

            parametro.Value = (valor != null ? valor : DBNull.Value);
        }

        public object CarregarParametro(string nome)
        {
            var parametro = (IDbDataParameter)base.Parameters[nome];

            if ( parametro.Value == DBNull.Value )
                return null;

            return parametro.Value;
        }

        /// <summary>
        /// Executa o stored procedure sem carregar nenhum registro desse.
        /// </summary>
        public void Executar()
        {
            try
            {
                Connection.Open();

                ExecuteNonQuery();
            }
            finally
            {
                CarregarRetorno();
                Connection.Close();
            }
        }

        /// <summary>
        /// Executa o stored procedure carregando um registro desse.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto que será retornado a partir do registro carregado.</typeparam>
        public T CarregarUmRegistro<T>() where T : Aurum.Entidades.Base.Entidade, new()
        {
            try
            {
                this.Connection.Open();

                using ( var dataReader = this.ExecuteReader() )
                {
                    if ( dataReader.Read() )
                    {
                        var entidade = new T();
                        entidade.CarregarCampos(dataReader);

                        return entidade;
                    }
                    else
                    {
                        return new T();
                    }
                }
            }
            finally
            {
                CarregarRetorno();
                this.Connection.Close();
            }
        }

        /// <summary>
        /// Executa o stored procedure carregando uma lista de registros desse.
        /// </summary>
        /// <typeparam name="T">Tipo de cada item da lista gerada a partir dos registros carregados.</typeparam>
        public List<T> CarregarListaRegistros<T>() where T : Aurum.Entidades.Base.Entidade, new()
        {
            try
            {
                this.Connection.Open();

                using ( var dataReader = this.ExecuteReader() )
                {
                    var lista = new List<T>();

                    while ( dataReader.Read() )
                    {
                        var entidade = new T();
                        entidade.CarregarCampos(dataReader);

                        lista.Add(entidade);
                    }

                    return lista;
                }
            }
            finally
            {
                CarregarRetorno();
                this.Connection.Close();
            }
        }

        /// <summary>
        /// Executa o stored procedure carregando uma lista com os valores do primeiro campo de todos os registros carregados desse.
        /// </summary>
        /// <typeparam name="T">Tipo de cada item da lista, que também deve ser o tipo do primeiro campo da lista de registros carregados.</typeparam>
        public List<T> CarregarListaValores<T>()
        {
            try
            {
                this.Connection.Open();

                using ( var dataReader = this.ExecuteReader() )
                {
                    CarregarRetorno();

                    var lista = new List<T>();

                    while ( dataReader.Read() )
                        lista.Add((T)dataReader.GetValue(0));

                    return lista;
                }
            }
            finally
            {
                this.Connection.Close();
            }
        }


        private void CarregarRetorno()
        {
            foreach ( SqlParameter parametro in (SqlParameterCollection)base.Parameters )
            {
                if ( parametro.Direction == ParameterDirection.ReturnValue )
                {
                    // Se o valor de retorno for DBNull, não armazena o valor de retorno.
                    if ( parametro.Value == DBNull.Value )
                        return;

                    this.Retorno = (int?)parametro.Value;
                }
            }
        }
    }
}
