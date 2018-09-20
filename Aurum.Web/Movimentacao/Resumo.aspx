<%@ Page Title="Resumos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Resumo.aspx.cs" Inherits="Aurum.Web.Movimentacao_Resumo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="server">

    <h2>Resumos</h2>

    <aurum:Meses ID="meses" runat="server" />

    <aurum:GridView ID="gvListaContas" runat="server" CssClass="grid" AutoGenerateColumns="false" RowStyle-Wrap="false" ExibirSelecao="false" CampoValor="Valor" LabelValor="lblValor" Style="float: left; margin-right: 50px; width: 250px;">
        <Columns>
            <%--<asp:BoundField HeaderText="Código" DataField="CodConta" />--%>
            <asp:BoundField HeaderText="Conta" DataField="Conta.AgenciaConta" />
            <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblValor" runat="server"><%# Eval("ValorMoeda") %></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </aurum:GridView>

    <aurum:GridView ID="gvListaCartao" runat="server" CssClass="grid" AutoGenerateColumns="false" RowStyle-Wrap="false" ExibirSelecao="false" CampoValor="Valor" LabelValor="lblValor" Style="float: left; margin-right: 50px; width: 250px;">
        <Columns>
            <%--<asp:BoundField HeaderText="Código" DataField="CodCartao" />--%>
            <asp:BoundField HeaderText="Cartão" DataField="Cartao.Descricao" />
            <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblValor" runat="server"><%# Eval("ValorMoeda") %></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </aurum:GridView>

    <aurum:GridView ID="gvListaCategoria" runat="server" CssClass="grid" AutoGenerateColumns="false" RowStyle-Wrap="false" ExibirSelecao="false" CampoValor="Valor" LabelValor="lblValor" Style="float: left; margin-right: 50px; width: 250px;">
        <Columns>
            <%--<asp:BoundField HeaderText="Código" DataField="CodCategoria" />--%>
            <asp:BoundField HeaderText="Categoria" DataField="Categoria.Descricao" />
            <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblValor" runat="server"><%# Eval("ValorMoeda") %></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </aurum:GridView>


</asp:Content>
