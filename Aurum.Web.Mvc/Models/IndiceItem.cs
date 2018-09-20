using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurum.Web.Models
{
    public class IndiceItem
    {
        public string Grupo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }

        string _imagem;
        /// <summary>
        /// Nome da imagem PNG que será exibida no item do índice (sem a extensão).
        /// Caso não seja informado, será retornado o nome "Transparente".
        /// </summary>
        public string Imagem {
            get
            {
                return (String.IsNullOrWhiteSpace(_imagem) ? "Transparente" : _imagem);
            }
            set
            {
                _imagem = value;
            }
        }
    }
}