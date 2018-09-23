using Model = Aurum.AcessoDados;

namespace Aurum.Dominio.Servicos
{
    public class BaseService
    {
        protected Model.AurumEntities contexto = new Model.AurumEntities();
    }
}
