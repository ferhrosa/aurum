using System;
using System.Collections.Generic;
using System.Linq;
using Aurum.Dados.Acesso;
using Aurum.Dados.Excecoes;

namespace Aurum.Dados.Entidades
{
    public class Usuario : Aurum.Entidades.Usuario
    {

        /// <summary>
        /// Autentica o usuário a partir do usuário e da senha.
        /// </summary>
        public static Usuario Autenticar(string login, string senha)
        {
            using ( var sp = new StoredProcedure("AutenticarUsuario") )
            {
                // Define os parâmetros a serem passados ao stored procedure.
                sp.DefinirParametro("@login", login);
                sp.DefinirParametro("@senha", senha);

                // Executa o stored procedure e carrega os registros carregados.
                var resultado = sp.CarregarUmRegistro<Usuario>();

                // Faz o tratamento do retorno do stored procedure.
                switch ( sp.Retorno )
                {
                    case 0:  // Usuário autenticado com sucesso.
                        return resultado;
                    case 1: 
                        throw new Exception("Usuário e senha inválidos.");
                    default:  // O valor retornado pelo stored procedure era inesperado.
                        throw new ExRetornoInesperado(sp);
                }
            }
        }

    }
}