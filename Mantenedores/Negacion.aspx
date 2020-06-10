<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Negacion.aspx.vb" Inherits="Mantenedores_Negacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <style>
        input[type="text"]
        {
            font-size: 10px;
        }

        .select_small
        {
            font-size: 10px;
             
        }
        /* The switch - the box around the slider */
        .switch
        {
            position: relative;
            display: inline-block;
            width: 20px;
            height: 17px;
        }

        .resaltar
        {
            text-decoration: none;
            color: black;
            background: yellow;
        }


        /* Hide default HTML checkbox */
        .switch input
        {
            display: none;
        }

        /* The slider */
        .slider
        {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: -13px;
            bottom: 0;
            background-color: #c62423;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before
            {
                position: absolute;
                content: "";
                height: 13px;
                width: 13px;
                left: 2px;
                bottom: 2px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider
        {
            background-color: #1abd1d;
        }

        input:focus + .slider
        {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before
        {
            -webkit-transform: translateX(16px);
            -ms-transform: translateX(16px);
            transform: translateX(16px);
        }

        /* Rounded sliders */
        .slider.round
        {
            border-radius: 17px;
        }

            .slider.round:before
            {
                border-radius: 50%;
            }
   
           /*#Grilla td, #Grilla th  #Grilla tr{
            padding: 2px;
            vertical-align: top;
            border-top: 1px solid #e3e6f0;
        }*/



         /*#Grilla tr{
          text-align: left;
          vertical-align: top;

         }*/

        #Grilla td{
          vertical-align: top;
          line-height: 1;
          padding-bottom: 1px;
           padding-top: 2px;

          }

        
        /*/#Grilla footer{
          vertical-align: top;
         padding-bottom: 30px;

          }*/





    </style>
    <script type="text/javascript">
        function exitosou(mensaje) {
            $("#MainContent_pasou").html(mensaje);
            $("#exitosou").show("fold", 1500, desapareceru);
        }
        function erroneou(mensaje) {
            $("#MainContent_nopasou").html(mensaje);
            $("#erroneou").show("fold", 1500);
        }
        function desapareceru() {
            setTimeout(function () {
                $("#exitosou").hide("fold", 1500);
            }, 1000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1 class="h3 mb-1 text-gray-800">Parámetros: Negación</h1>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="Grilla" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                EmptyDataText="Sin Datos para Mostrar.." ClientIDMode="Static" ShowHeaderWhenEmpty="True"
                CssClass="table table-bordered table-hover table-condensed table-striped" DataKeyNames="CODIGO"
                HorizontalAlign="Center" Width="100%" ShowFooter="True" AllowPaging="true" BackColor="white"
                PageSize="20"  OnPageIndexChanging="OnPageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="30px">
                        <EditItemTemplate>
                            <asp:ImageButton ID="bt_guardar" runat="server" CausesValidation="True" CommandName="Update"
                                ImageUrl="~/Imagenes/save.png" Width="25px" Height="25px" Text="Actualizar" ToolTip="Guardar" ValidationGroup="edita" />
                            &nbsp;<asp:ImageButton ID="bt_cancelar" runat="server" CausesValidation="False" CommandName="Cancel"
                                ImageUrl="~/Imagenes/cancel.png" Width="25px" Height="25px" Text="Cancelar" ToolTip="Cancelar" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="bt_f_guardar" runat="server" CausesValidation="True" CommandName="nuevo"
                                ImageUrl="~/Imagenes/save.png"  Width="25px" Height="25px" Text="Actualizar" ToolTip="Guardar"
                                OnClick="bt_f_guardar_Click"  ValidationGroup="insert" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="bt_editar" runat="server" CausesValidation="False" CommandName="Edit"
                                ImageUrl="~/Imagenes/edit.png" Width="25px" Height="25px" Text="Editar" ToolTip="Editar registro" />
                             
                          <asp:ImageButton ID="bt_delete" runat="server" CausesValidation="False" CommandName="Delete"
                                ImageUrl="~/Imagenes/trash.png" Width="25px" Height="25px" Text="Eliminar" OnClientClick=" return window.confirm('¿Está seguro que desea eliminar el registro seleccionado?');" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Codigo">
                        <EditItemTemplate>
                            <asp:Label ID="tx_e_cod_negacion" runat="server" Text='<%# Bind("CODIGO") %>' CssClass="small"></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_cod_negacion" runat="server" Text='<%# Bind("CODIGO") %>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Negación">
                        <EditItemTemplate>
                            <asp:TextBox ID="tx_e_negacion" CssClass="form-control small" runat="server" MaxLength="200" Text='<%# Bind("TEXTO") %>'
                                Width="100%"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RFV_tx_e_negacion" runat="server" ControlToValidate="tx_e_negacion"
                                ValidationGroup="edita">
                                          <img src="../Imagenes/error16.png" alt="Requerido"/>
                            </asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="tx_f_negacion" CssClass="form-control small" runat="server" MaxLength="200" Text='<%# Bind("TEXTO")%>'
                                Width="100%" ValidationGroup="insert"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFV_tx_f_negacion" runat="server" ControlToValidate="tx_f_negacion"
                                ValidationGroup="insert">
                                          <img src="../Imagenes/error16.png" alt="Requerido"/>
                            </asp:RequiredFieldValidator>

                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_negacion" runat="server" Text='<%# Bind("TEXTO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                   

                </Columns>
                <HeaderStyle CssClass="HeaderGrid" />
                <AlternatingRowStyle CssClass="RowAlternate" />
                <RowStyle CssClass="RowGrid" />
                <EditRowStyle CssClass="RowEdit" />
                <FooterStyle CssClass="RowFooter" />

                <SelectedRowStyle CssClass="RowSelect" />
                <EmptyDataRowStyle CssClass="RowEmpty" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

