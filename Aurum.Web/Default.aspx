<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Aurum.Web._Default" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="cphConteudo">
    <div id="indice">
        <h2>Movimentação</h2>

        <aurum:IndiceItem runat="server"
            Titulo="Conferência"
            Descricao="Saldo atual das contas separados por meses (consolidados até data atual)"
            ImageUrl="~/Imagens/Movimento.png"
            NavigateUrl="~/movimentacao/conferencia.aspx" />

        <aurum:IndiceItem runat="server"
            Titulo="Resumo"
            Descricao="Resumo dos saldos separados por meses (Conta, Cartão e Categoria)"
            ImageUrl="~/Imagens/Movimento.png"
            NavigateUrl="~/movimentacao/resumo.aspx" />

        <aurum:IndiceItem runat="server"
            Titulo="Movimentações"
            Descricao="Cadastro das movimentações de entrada e saída por meses"
            ImageUrl="~/Imagens/Movimento.png"
            NavigateUrl="~/movimentacao/movimentacao.aspx" />


        <h2>Cadastros</h2>

        <aurum:IndiceItem runat="server"
            Titulo="Contas"
            Descricao="Cadastro de contas bancárias"
            ImageUrl="~/Imagens/Conta.png"
            NavigateUrl="~/cadastros/conta.aspx" />

        <aurum:IndiceItem runat="server"
            Titulo="Cartões"
            Descricao="Cadastro de cartões de crédito"
            ImageUrl="~/Imagens/Cartao.png"
            NavigateUrl="~/cadastros/cartao.aspx" />

        <aurum:IndiceItem runat="server"
            Titulo="Categorias"
            Descricao="Cadastro de categorias de movimentações"
            ImageUrl="~/Imagens/Categoria.png"
            NavigateUrl="~/cadastros/categoria.aspx" />

    </div>
</asp:Content>
