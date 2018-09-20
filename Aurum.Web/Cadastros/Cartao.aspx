<%@ Page Title="Cartões" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cartao.aspx.cs" Inherits="Aurum.Web.Cadastros_Cartao" %>

<asp:Content ContentPlaceHolderID="cphConteudo" runat="server">

    <h2>Cartões</h2>

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
            <asp:Label ID="lblConta" runat="server" Text="Conta:" />
            <asp:DropDownList ID="ddlConta" runat="server" DataTextField="AgenciaConta" DataValueField="CodConta" />
        </div>

        <div>
            <asp:Label ID="lblDescricao" runat="server" Text="Descrição:" />
            <asp:TextBox ID="txtDescricao" runat="server" />
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ErrorMessage="*" ControlToValidate="txtDescricao" />
        </div>

        <div>
            <asp:Label ID="lblNumero" runat="server" Text="Número:" />
            <asp:TextBox ID="txtNumero" runat="server" />
            <asp:RequiredFieldValidator ID="rfvNumero" runat="server" ErrorMessage="*" ControlToValidate="txtNumero" />
        </div>

        <div>
            <asp:Label ID="lblTitular" runat="server" Text="Titular:" />
            <asp:TextBox ID="txtTitular" runat="server" />
            <asp:RequiredFieldValidator ID="rfvTitular" runat="server" ErrorMessage="*" ControlToValidate="txtTitular" />
        </div>

        <div>
            <asp:Label ID="lblValidade" runat="server" Text="Validade:" />
            <asp:TextBox ID="txtValidade" runat="server" CssClass="data" />
            <act:CalendarExtender runat="server" TargetControlID="txtValidade" PopupButtonID="imgValidade" />
            <img runat="server" src="~/Imagens/Calendario.png" id="imgValidade" style="cursor: pointer" />
        </div>

    </div>

    <aurum:GridView ID="gvLista" runat="server" CssClass="grid" DataKeyNames="CodCartao" />

</asp:Content>
