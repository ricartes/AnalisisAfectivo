<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="PalabraNoEncontrada.aspx.vb" Inherits="Mantenedores_PalabraNoEncontrada" %>
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
   <h1 class="h3 mb-1 text-gray-800">Parámetros: Palabras no encontradas</h1>
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

                        <ItemTemplate>
                           <asp:ImageButton ID="bt_f_guardar" runat="server" CausesValidation="True" CommandName="nuevo"
                                ImageUrl="~/Imagenes/save.png"  Width="25px" Height="25px" Text="Actualizar" ToolTip="Guardar"
                                OnClick="bt_f_guardar_Click"  ValidationGroup="insert" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Codigo">


                        <ItemTemplate>
                            <asp:Label ID="lb_cod_palabra" runat="server" Text='<%# Bind("CODIGO") %>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Texto">

                        <ItemTemplate>
                            <asp:Label ID="lb_texto" runat="server" Text='<%# Bind("TEXTO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                     <asp:TemplateField HeaderText="Sentimiento">
                        <ItemTemplate>
                           <asp:DropDownList ID="ddl_sentimiento" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Intensidad">
                        <ItemTemplate>
                          <asp:TextBox ID="tx_intensidad" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>


                    </Columns>

                    </asp:GridView>


        </ContentTemplate>

    </asp:UpdatePanel>


</asp:Content>

