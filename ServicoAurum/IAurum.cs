using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ServicoAurum.Entidades;

namespace ServicoAurum
{
    [ServiceContract]
    public interface IAurum
    {
        #region Cartão

        [OperationContract]
        List<Cartao> ListarCartao();
        
        [OperationContract]
        int SalvarCartao(int? codCartao, bool ativo, int? codConta, string descricao, string numero, string titular, byte vencimento, DateTime? validade, bool possuiAdicional, string telefoneSac);

        [OperationContract]
        void ExcluirCartao(int codCartao);

        #endregion Cartão


        #region Conta

        [OperationContract]
        List<Conta> ListarConta();

        #endregion Conta
    }
}
