<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Aurum.Web.Login" %>

<asp:Content ContentPlaceHolderID="cphConteudo" runat="server">
    <div id="login">
        <h2>Login</h2>

        <span>Usuário</span>
        <asp:TextBox ID="txtUsuario" runat="server" />

        <span>Senha</span>
        <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" />

        <asp:Button ID="btnEntrar" runat="server" Text="Entrar" />
    </div>
</asp:Content>
