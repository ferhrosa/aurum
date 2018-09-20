<%@ Page Title="Categorias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="Aurum.Web.Cadastros_Categoria" %>

<asp:Content ContentPlaceHolderID="cphConteudo" runat="server">
    
    <h2>Categorias</h2>

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
            <asp:Label ID="lblDescricao" runat="server" Text="Descrição:" />
            <asp:TextBox ID="txtDescricao" runat="server" />
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ErrorMessage="*" ControlToValidate="txtDescricao" />
        </div>

    </div>

    <aurum:GridView ID="gvLista" runat="server" CssClass="grid" DataKeyNames="CodCategoria" />

</asp:Content>
