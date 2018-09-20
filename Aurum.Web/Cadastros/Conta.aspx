<%@ Page Title="Contas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Conta.aspx.cs" Inherits="Aurum.Web.Cadastros_Conta" %>

<asp:Content ContentPlaceHolderID="cphConteudo" runat="server">

    <h2>Contas</h2>

    <div id="botoes">
        <asp:Button ID="btnInserir" runat="server" Text="Inserir" />
        <asp:Button ID="btnAlterar" runat="server" Text="Alterar" />
        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" OnClientClick="return ConfirmarExclusao();" />
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
    </div>

    <asp:Label ID="lblErro" runat="server" Text="" CssClass="label_erro" />

    <div class="cadastro" runat="server" id="divCadastro">

        <div>
            <asp:Label ID="lblCodigo" runat="server" Text="Código:" />
            <asp:TextBox ID="txtCodigo" runat="server" Enabled="false" CssClass="Codigo" />
            <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" />
        </div>

        <div>
            <asp:Label ID="lblBanco" runat="server" Text="Banco:" />
            <asp:TextBox ID="txtBanco" runat="server" />
            <asp:RequiredFieldValidator ID="rfvBanco" runat="server" ErrorMessage="*" ControlToValidate="txtBanco" />
        </div>

        <div>
            <asp:Label ID="lblTipo" runat="server" Text="Tipo:" />
            <asp:RadioButtonList ID="rblTipoConta" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Text="Corrente" Value="1" />
                <asp:ListItem Text="Poupança" Value="2" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvTipoConta" runat="server" ErrorMessage="*" ControlToValidate="rblTipoConta" />
        </div>

        <div>
            <asp:Label ID="lblAgenciaConta" runat="server" Text="Ag. e Conta:" />
            <asp:TextBox ID="txtAgenciaConta" runat="server" />
            <asp:RequiredFieldValidator ID="rfvAgenciaConta" runat="server" ErrorMessage="*" ControlToValidate="txtAgenciaConta" />
        </div>

    </div>

    <aurum:GridView ID="gvLista" runat="server" CssClass="grid" DataKeyNames="CodConta" />

</asp:Content>
<asp:Content ContentPlaceHolderID="cphFinal" runat="server">
</asp:Content>
