using Aurum.Modelo.Entidades;
using AutoMapper;
using Model = Aurum.AcessoDados;

namespace Aurum.Dominio
{
    internal static class Maps
    {

        static Maps()
        {
            Mapper.Initialize(cfg =>
            {
                // Cartão
                cfg.CreateMap<Model.Cartao, Cartao>();
                cfg.CreateMap<Cartao, Model.Cartao>();

                // Categoria
                cfg.CreateMap<Model.Categoria, Categoria>();
                cfg.CreateMap<Categoria, Model.Categoria>();

                // Conta
                cfg.CreateMap<Model.Conta, Conta>();
                cfg.CreateMap<Conta, Model.Conta>();

                // Movimentação
                cfg.CreateMap<Model.Movimentacao, Movimentacao>();
                cfg.CreateMap<Movimentacao, Model.Movimentacao>();
            });
        }

        /// <summary>
        /// Converte um objeto em outro.
        /// </summary>
        /// <typeparam name="TDestino">Tipo do objeto de destino.</typeparam>
        /// <param name="origem">Objeto a converter.</param>
        /// <returns>Objeto convertido.</returns>
        public static TDestino Map<TDestino>(this object origem)
        {
            return Mapper.Map<TDestino>(origem);
        }

    }
}
