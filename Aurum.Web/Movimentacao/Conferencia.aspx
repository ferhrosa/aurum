<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Conferencia.aspx.cs" Inherits="Aurum.Web.Movimentacao_Conferencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="server">

    <h2>Conferência</h2>

    <asp:Label ID="lblErro" runat="server" Text="" CssClass="label_erro" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

    <div class="cadastro" runat="server" id="divCadastro">
        <div>
            <asp:Label ID="lblPeriodo" runat="server" Text="Período:" />
            <asp:TextBox ID="txtDataInicio" runat="server" CssClass="data" />
            <act:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDataInicio" PopupButtonID="imgDataInicio" />
            <img src="~/Imagens/Calendario.png" runat="server" id="imgDataInicio" style="cursor: pointer" />
            <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" ErrorMessage="Informe a Data de Início." ControlToValidate="txtDataInicio" />

            <asp:Label ID="lblPeriodo2" runat="server" Text="a" Style="margin-right: 10px;" />
            <asp:TextBox ID="txtDataFim" runat="server" CssClass="data" />
            <act:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDataFim" PopupButtonID="imgDataFim" />
            <img src="~/Imagens/Calendario.png" runat="server" id="imgDataFim" style="cursor: pointer" />
            <asp:RequiredFieldValidator ID="rfvDataTermino" runat="server" ErrorMessage="Informe a Data de Término." ControlToValidate="txtDataFim" />

            <asp:Button ID="btnMostrar" runat="server" Text="Mostrar" />
        </div>
    </div>

    <asp:Repeater ID="repeater" runat="server">
        <ItemTemplate>

            <div style="display: inline-block; margin-right: 20px; margin-bottom: 20px">
                <asp:Label ID="lblMesAno" runat="server" Font-Bold="true" />

                <aurum:GridView ID="gridView" runat="server" CssClass="grid" AutoGenerateColumns="false" RowStyle-Wrap="false" ExibirSelecao="false" CampoValor="Valor" LabelValor="lblValor">
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
            </div>

        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
