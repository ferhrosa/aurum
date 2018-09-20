using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Aurum.Dados.Acesso
{
    public class Conexao : IDbConnection
    {
        IDbConnection _conexao;


        public Conexao(string nomeConnectionString)
        {
            Criar(nomeConnectionString);
        }

        public Conexao()
        {
            Criar("BancoDados");
        }

        private void Criar(string nomeConnectionString)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[nomeConnectionString];

            if ( connectionString == null )
                throw new ConfigurationErrorsException("Não foi encontrada a Connection String \"" + nomeConnectionString + "\" no arquivo de configurações.");

            switch ( connectionString.ProviderName )
            {
                case "System.Data.SqlClient":
                    this._conexao = new SqlConnection(connectionString.ConnectionString);
                    break;

                default:
                    throw new ConfigurationErrorsException("O \"ProviderName\" da Connection String \"" + nomeConnectionString + "\" é inválido (valor configurado: \"" + connectionString.ProviderName + "\").");
            }
        }


        internal IDbConnection ConexaoInterna
        {
            get
            {
                return this._conexao;
            }
        }

        public new Type GetType()
        {
            return this._conexao.GetType();
        }


        #region Implementação de IDbConnection

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return this._conexao.BeginTransaction(il);
        }

        public IDbTransaction BeginTransaction()
        {
            return this._conexao.BeginTransaction();
        }

        public void ChangeDatabase(string databaseName)
        {
            this._conexao.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            this._conexao.Close();
        }

        public string ConnectionString
        {
            get
            {
                return this._conexao.ConnectionString;
            }
            set
            {
                this._conexao.ConnectionString = value;
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                return this._conexao.ConnectionTimeout;
            }
        }

        public IDbCommand CreateCommand()
        {
            return this._conexao.CreateCommand();
        }

        public string Database
        {
            get
            {
                return this._conexao.Database;
            }
        }

        public void Open()
        {
            this._conexao.Open();
        }

        public ConnectionState State
        {
            get
            {
                return this._conexao.State;
            }
        }

        public void Dispose()
        {
            this._conexao.Dispose();
        }

        #endregion Implementação de IDbConnection
    }
}
