<%@ Page Title="Movimentações" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Movimentacao.aspx.cs" Inherits="Aurum.Web.Movimentacao_Movimentacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphConteudo" runat="server">

    <h2>Movimentos</h2>

    <aurum:Meses ID="meses" runat="server" />

    <div id="botoes">
        <asp:Button ID="btnInserir" runat="server" Text="Inserir" />
        <asp:Button ID="btnAlterar" runat="server" Text="Alterar" />
        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" OnClientClick="return ConfirmarExclusao();" />
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
        <div class="espacador"></div>
        <asp:Button ID="btnReplicar" runat="server" Text="Replicar" CausesValidation="false" />
        <asp:Button ID="btnConsolidar" runat="server" Text="Consolidar" CausesValidation="false" OnClientClick="return confirm('Deseja realmente marcar todos os movimentos selecionados como consolidados?');" />
    </div>

    <asp:Label ID="lblErro" runat="server" Text="" CssClass="label_erro" />
    <asp:ValidationSummary runat="server" />

    <div class="cadastro" runat="server" id="divCadastro">

        <div>
            <asp:Label ID="lblCodigo" runat="server" Text="Código:" />
            <asp:TextBox ID="txtCodigo" runat="server" Enabled="false" CssClass="Codigo" />
        </div>

        <div>
            <asp:Label ID="lblData" runat="server" Text="Data:" />
            <asp:TextBox ID="txtData" runat="server" CssClass="data" />
            <act:CalendarExtender runat="server" TargetControlID="txtData" PopupButtonID="imgData" />
            <img src="~/Imagens/Calendario.png" runat="server" id="imgData" style="cursor: pointer" />
            <asp:RequiredFieldValidator ID="rfvData" runat="server" ErrorMessage="Informe a Data da movimentação." ControlToValidate="txtData" />
            <asp:CheckBox ID="chkConsolidado" runat="server" Text="Consolidado" Style="margin-left: 70px" />
        </div>

        <div>
            <asp:Label ID="lblTipo" runat="server" Text="Tipo:" />
            <asp:RadioButtonList ID="rblTipo" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Text="Crédito" Value="+" />
                <asp:ListItem Text="Débito" Value="-" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ErrorMessage="Informe o Tipo da movimentação." ControlToValidate="rblTipo" />
        </div>

        <div>
            <asp:Label ID="lblValor" runat="server" Text="Valor:" />
            <asp:TextBox ID="txtValor" runat="server" CssClass="valor" />
            <act:FilteredTextBoxExtender runat="server" TargetControlID="txtValor" FilterType="Custom" ValidChars="0123456789," />
            <asp:RequiredFieldValidator ID="rfvValor" runat="server" ErrorMessage="Informe o Valor da movimentação." ControlToValidate="txtValor" />
            <asp:CompareValidator runat="server" ControlToValidate="txtValor" Type="Currency" Operator="DataTypeCheck" ErrorMessage="Informe um Valor válido." />
        </div>

        <div>
            <asp:Label ID="lblCategoria" runat="server" Text="Categoria:" />
            <asp:DropDownList ID="ddlCategoria" runat="server" DataValueField="CodCategoria" DataTextField="Descricao" />
            <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ErrorMessage="Informe o Categoria da movimentação." ControlToValidate="ddlCategoria" />
        </div>

        <div>
            <asp:Label ID="lblDescricao" runat="server" Text="Descrição:" />
            <asp:TextBox ID="txtDescricao" runat="server" />
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ErrorMessage="Informe a Descrição da movimentação." ControlToValidate="txtDescricao" />
            <act:AutoCompleteExtender ID="aceDescricao" runat="server" TargetControlID="txtDescricao" ServicePath="~/Servicos/AutoComplete.asmx" ServiceMethod="ListarMovimentoDescricao" OnClientItemSelected="SelecionarDescricao" UseContextKey="false" MinimumPrefixLength="1" CompletionInterval="200" />
            <asp:HiddenField ID="hfDescricao" runat="server" />
        </div>

        <div>
            <asp:Label ID="lblConta" runat="server" Text="Conta:" />
            <asp:DropDownList ID="ddlConta" runat="server" DataValueField="CodConta" DataTextField="AgenciaConta" />
            <%--<asp:RequiredFieldValidator ID="rfvConta" runat="server" ErrorMessage="Informe o Categoria da movimentação." ControlToValidate="ddlCategoria" />--%>
        </div>

        <div>
            <asp:Label ID="lblCartao" runat="server" Text="Cartão:" />
            <asp:DropDownList ID="ddlCartao" runat="server" DataValueField="CodCartao" DataTextField="Descricao" />
            <%--<asp:RequiredFieldValidator ID="rfvCartao" runat="server" ErrorMessage="Informe o Categoria da movimentação." ControlToValidate="ddlCategoria" />--%>
        </div>

        <asp:Panel ID="pnlParcelas" runat="server">
            <asp:Label ID="lblNumeroParcela" runat="server" Text="Parcela nº:" />
            <asp:TextBox ID="txtNumeroParcela" runat="server" MaxLength="3" CssClass="valor" />
            <asp:RangeValidator runat="server" ControlToValidate="txtNumeroParcela" Type="Integer" MinimumValue="1" MaximumValue="255" ErrorMessage="O número da parcela é inválido" />
            <asp:Label ID="lblParcelas" runat="server" Text="de: " />
            <asp:TextBox ID="txtTotalParcelas" runat="server" MaxLength="3" CssClass="valor" />
            <asp:RangeValidator runat="server" ControlToValidate="txtTotalParcelas" Type="Integer" MinimumValue="2" MaximumValue="255" ErrorMessage="O total de parcelas é inválido" />
            <act:FilteredTextBoxExtender runat="server" TargetControlID="txtNumeroParcela" FilterType="Numbers" />
            <act:FilteredTextBoxExtender runat="server" TargetControlID="txtTotalParcelas" FilterType="Numbers" />
        </asp:Panel>

        <div>
            <asp:Label ID="lblObservacao" runat="server" Text="Observação:" />
            <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Rows="3" />
        </div>

    </div>

    <aurum:GridView ID="gvLista" runat="server" CssClass="grid" DataKeyNames="codMovimento" AutoGenerateColumns="false" RowStyle-Wrap="false" CampoValor="Valor" LabelValor="lblValor">
        <Columns>
            <asp:BoundField HeaderText="Código" DataField="CodMovimento" />
            <asp:BoundField HeaderText="Data" DataField="Data" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <asp:CheckBoxField HeaderText="Consolidado" DataField="Consolidado" ItemStyle-HorizontalAlign="Center" />
            <%--<asp:BoundField HeaderText="Mês" DataField="MesAno" DataFormatString="{0:MM/yyyy}" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />--%>
            <asp:BoundField HeaderText="Categoria" DataField="Categoria.Descricao" />
            <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblValor" runat="server"><%# Eval("ValorMoeda") %></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Descrição" DataField="MovimentoDescricao.Descricao" />
            <asp:BoundField HeaderText="Conta" DataField="Conta.AgenciaConta" />
            <asp:BoundField HeaderText="Cartão" DataField="Cartao.Descricao" />
            <asp:TemplateField HeaderText="Parcela" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblParcela" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </aurum:GridView>


    <act:ModalPopupExtender runat="server" TargetControlID="btnReplicar"
        PopupControlID="pnlReplicar" BackgroundCssClass="popup-fundo"
        CancelControlID="btnReplicarCancelar" />

    <asp:Panel runat="server" ID="pnlReplicar" CssClass="popup" Width="600px">
        <header>Replicar itens selecionados</header>

        <section class="principal">
            <div class="icone">
                <img runat="server" src="~/Imagens/Questao.png" />
            </div>

            <div class="conteudo">
                <p>Todos os itens selecionados serão replicados para os próximos meses.</p>
                <p>
                    Quantidade de meses para ter os itens replicados:
                    <asp:TextBox runat="server" ID="txtReplicarMeses" Width="20px" MaxLength="2" />
                    <act:FilteredTextBoxExtender runat="server" TargetControlID="txtReplicarMeses" FilterType="Numbers" />

                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Replicar" ControlToValidate="txtReplicarMeses" Text="<br />É necessário informar a quantidade de meses." Display="Dynamic" CssClass="field-validation-error" />
                </p>
                <p id="pReplicarMeses">&nbsp</p>
            </div>
        </section>

        <section class="botoes">
            <asp:Button runat="server" ID="btnReplicarOk" Text="Replicar" ValidationGroup="Replicar" />
            <asp:Button runat="server" ID="btnReplicarCancelar" Text="Cancelar" />
        </section>
    </asp:Panel>


    <script type="text/javascript">
        var txtReplicarMeses;
        var pReplicarMeses;

        var mes = ["janeiro", "fevereiro", "março", "abril", "maio", "junho", "julho", "agosto", "setembro", "outubro", "novembro", "dezembro"];

        var mesAtual = new Date(<%= meses.Mes.Year %>, <%= meses.Mes.Month %> - 1, <%= meses.Mes.Day %>);

        $(document).ready(function (e)
        {
            txtReplicarMeses = $("#<%= txtReplicarMeses.ClientID %>");
            txtReplicarMeses.keyup(PreencherMeses);
            txtReplicarMeses.change(PreencherMeses);

            pReplicarMeses = $("#pReplicarMeses");
        });

        function SelecionarDescricao(sender, e)
        {
            $('#<%= hfDescricao.ClientID %>').val(e.get_value());
        }

        function PreencherMeses()
        {
            var quantidade = txtReplicarMeses.val();

            pReplicarMeses.html("&nbsp;");

            if (!isNaN(quantidade))
            {
                var meses = [];
                var data = new Date(mesAtual);
                var contador = 0;

                while (contador < quantidade) {
                    data = new Date(new Date(data).setMonth(data.getMonth() + 1));

                    var descricaoMes = " " + mes[data.getMonth()];
                    
                    if (data.getFullYear() != mesAtual.getFullYear())
                        descricaoMes += "/" + data.getFullYear();

                    meses.push(descricaoMes);

                    contador++;
                }
                
                if (meses.length)
                    pReplicarMeses.text("Meses: " + meses);
            }
        }
    </script>

</asp:Content>
