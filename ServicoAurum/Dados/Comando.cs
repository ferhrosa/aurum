using System;
using System.Data;
using System.Data.SqlClient;

namespace ServicoAurum.Dados
{
    public class Comando : IDbCommand
    {
        protected IDbCommand _comando;


        public Comando()
        {
            Criar(null, null);
        }

        private void Criar(string texto, Conexao conexao)
        {
            if ( conexao == null )
                conexao = new Conexao();

            var tipoConexao = conexao.GetType();

            if ( tipoConexao.Equals(typeof(SqlConnection)) )
                this._comando = new SqlCommand(texto, (SqlConnection)conexao.ConexaoInterna);
            else
                throw new NotImplementedException();
        }


        #region Implementação de IDbCommand

        public void Cancel()
        {
            this._comando.Cancel();
        }

        public string CommandText
        {
            get
            {
                return this._comando.CommandText;
            }
            set
            {
                this._comando.CommandText = value;
            }
        }

        public int CommandTimeout
        {
            get
            {
                return this._comando.CommandTimeout;
            }
            set
            {
                this._comando.CommandTimeout = value;
            }
        }

        public CommandType CommandType
        {
            get
            {
                return this._comando.CommandType;
            }
            set
            {
                this._comando.CommandType = value;
            }
        }

        public IDbConnection Connection
        {
            get
            {
                return this._comando.Connection;
            }
            set
            {
                this._comando.Connection = value;
            }
        }

        public IDbDataParameter CreateParameter()
        {
            return this._comando.CreateParameter();
        }

        public int ExecuteNonQuery()
        {
            return this._comando.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return this._comando.ExecuteReader(behavior);
        }

        public IDataReader ExecuteReader()
        {
            return this._comando.ExecuteReader();
        }

        public object ExecuteScalar()
        {
            return this._comando.ExecuteScalar();
        }

        public IDataParameterCollection Parameters
        {
            get
            {
                return this._comando.Parameters;
            }
        }

        public void Prepare()
        {
            this._comando.Prepare();
        }

        public IDbTransaction Transaction
        {
            get
            {
                return this._comando.Transaction;
            }
            set
            {
                this._comando.Transaction = value;
            }
        }

        public UpdateRowSource UpdatedRowSource
        {
            get
            {
                return this._comando.UpdatedRowSource;
            }
            set
            {
                this._comando.UpdatedRowSource = value;
            }
        }

        public void Dispose()
        {
            this._comando.Dispose();
        }

        #endregion Implementação de IDbCommand
    }
}
