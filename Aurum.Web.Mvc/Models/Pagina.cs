using System.Collections.Generic;

namespace Aurum.Web.Models
{
    public enum TipoPagina
    {
        Indice = 1,
        Cadastro = 2,
        Outro = 9
    }
    
    public class Pagina
    {
        public TipoPagina Tipo { get; set; }
        
        public string Titulo { get; set; }
        public IEnumerable<IndiceItem> Indice { get; set; }
        public IEnumerable<Entidades.Base.Entidade> Registros { get; set; }
        public IEnumerable<string> Templates { get; set; }
    }
}