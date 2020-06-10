<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" EnableEventValidation = "false" CodeFile="AnalisisOpinion.aspx.vb" Inherits="Analisis_AnalisisOpinion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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

        #Grilla td
        {
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <h1 class="h3 mb-1 text-gray-800">Análisis:
        <asp:Label ID="lb_nombre_analisis" runat="server"></asp:Label>
    </h1>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <asp:UpdatePanel ID="UpdatePanel" runat="server" >
                    <ContentTemplate>

            <asp:GridView ID="Grilla" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                EmptyDataText="Sin Datos para Mostrar.." ClientIDMode="Static" ShowHeaderWhenEmpty="True"
                CssClass="table table-bordered table-hover table-condensed table-striped" DataKeyNames="ID"
                HorizontalAlign="Center" Width="100%" ShowFooter="false" BackColor="white">
                <Columns>


                    <asp:TemplateField HeaderText="Codigo" Visible="false">

                        <ItemTemplate>
                            <asp:Label ID="lb_id_opinion" runat="server" Text='<%# Bind("ID") %>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Opinión">

                        <ItemTemplate>
                            <asp:Label ID="lb_texto_opinion" runat="server" Text='<%# Bind("TEXTO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Preprocesado">

                        <ItemTemplate>
                            <asp:Label ID="lb_prepro_opinion" runat="server" Text='<%# Bind("PREPROCESADO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="ID Sentimiento Usuario" Visible="false">

                        <ItemTemplate>
                            <asp:Label ID="lb_id_sentimiento_usuario" runat="server" Text='<%# Bind("SENTIMIENTO_USUARIO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sentimiento Experto">

                        <ItemTemplate>
                            <asp:Label ID="lb_sentimiento_usuario" runat="server" Text='<%# Bind("NOMBRE_SENTIMIENTO_USUARIO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>





                    <asp:TemplateField HeaderText="Sentimiento Calculado">

                        <ItemTemplate>
                            <asp:Label ID="lb_sentimiento_obtenido" runat="server" Text='<%# Bind("NOMBRE_SENTIMIENTO_ANALISIS")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                  <asp:TemplateField HeaderText="Palabras Encontradas">

                        <ItemTemplate>
                            <asp:Label ID="lb_cantidad_palabras" runat="server" Text='<%# Bind("TOTAL_PALABRAS")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Negaciones Encontradas">

                        <ItemTemplate>
                            <asp:Label ID="lb_cantidad_negacioness" runat="server" Text='<%# Bind("TOTAL_NEGACIONES")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Perfil Opinión">
                        <ItemTemplate>
                            <asp:ImageButton ID="bt_plan" runat="server" CausesValidation="False" CommandName="Sub"
                                ImageUrl="~/Imagenes/milupa.png" Width="25px" Height="25px" Text="Plan" ToolTip="Detalle"
                                CommandArgument="<%# Container.DataItemIndex %>" data-toggle="modal" data-target=".bs-example-modal-lg" />
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




                <asp:GridView ID="Grilla2" runat="server" AutoGenerateColumns="False" AllowSorting="True" Visible="false"
                EmptyDataText="Sin Datos para Mostrar.." ClientIDMode="Static" ShowHeaderWhenEmpty="True"
                CssClass="table table-bordered table-hover table-condensed table-striped" DataKeyNames="ID"
                HorizontalAlign="Center" Width="100%" ShowFooter="false" BackColor="white">
                <Columns>


                    <asp:TemplateField HeaderText="Codigo" Visible="false">

                        <ItemTemplate>
                            <asp:Label ID="lb_id_opinion" runat="server" Text='<%# Bind("ID") %>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Opinión">

                        <ItemTemplate>
                            <asp:Label ID="lb_texto_opinion" runat="server" Text='<%# Bind("TEXTO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Preprocesado">

                        <ItemTemplate>
                            <asp:Label ID="lb_prepro_opinion" runat="server" Text='<%# Bind("PREPROCESADO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="ID Sentimiento experto" Visible="false">

                        <ItemTemplate>
                            <asp:Label ID="lb_id_sentimiento_usuario" runat="server" Text='<%# Bind("SENTIMIENTO_USUARIO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sentimiento Experto">

                        <ItemTemplate>
                            <asp:Label ID="lb_sentimiento_usuario" runat="server" Text='<%# Bind("NOMBRE_SENTIMIENTO_USUARIO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Categorías Afectivas">

                        <ItemTemplate>
                            <asp:Label ID="lb_sentimiento_obtenido" runat="server" Text='<%# Bind("NOMBRE_SENTIMIENTO_ANALISIS")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Coincidencias">

                        <ItemTemplate>
                            <asp:Label ID="lb_cantidad_sentimiento" runat="server" Text='<%# Bind("TOTAL_PALABRAS_SENTIMIENTO")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Intensidad afectiva">

                        <ItemTemplate>
                            <asp:Label ID="lb_intensidad_opinion" runat="server" Text='<%# Bind("INTENSIDAD_OPINION_ANALISIS")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                     <asp:TemplateField HeaderText="Sentimiento Predominante">

                        <ItemTemplate>
                            <asp:Label ID="lb_sentimiento_predominante" runat="server" Text='<%# Bind("NOMBRE_SENTIMIENTO_PREDOMINANTE")%>' CssClass="small"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                   <asp:TemplateField HeaderText="Intensidad Predominante">

                        <ItemTemplate>
                            <asp:Label ID="lb_intensidad_predominante" runat="server" Text='<%# Bind("INTENSIDAD_ANALISIS")%>' CssClass="small"></asp:Label>
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



               <asp:Button ID="BtnExcel" runat="server" CssClass="btn btn-success" Style="width: 100%;"
                                    Text="Exportar A Excel" OnClick="ExportarExcel" />



    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                    <ContentTemplate>
                        <h3 class="modal-title" id="myModalLabel" runat="server" style="color: Black;"></h3>

                        <div class="modal-body">
                           
                            <table class="table table-bordered">
                                <tr style="background: DARKRED; color: White;">
                                    <th>Texto
                                    </th>


                                </tr>
                                <tr>
                                    <th>
                                        <asp:Label ID="lb_texto_opinion_modal" runat="server"></asp:Label>
                                    </th>

                                </tr>

                                <tr style="background: DARKRED; color: White;">
                                    <th>Preprocesado
                                    </th>


                                </tr>
                                <tr>
                                    <th>
                                        <asp:Label ID="lb_texto_preprocesado_modal" runat="server"></asp:Label>
                                    </th>
                                </tr>
                            </table>
                              <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                              
                                                <asp:Label ID="Labelll1" style="font-weight: 700;color:Black;" runat="server" Text="Cargando..."></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>                                      
                            <asp:GridView ID="GrillaModal" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                EmptyDataText="Sin Datos para Mostrar.." ClientIDMode="Static" ShowHeaderWhenEmpty="True"
                                CssClass="table table-bordered table-hover table-condensed table-striped" DataKeyNames="ID_OPINION,ID_SENTIMIENTO"
                                HorizontalAlign="Center" Width="100%" ShowFooter="false" AllowPaging="true" BackColor="white"
                                PageSize="20" OnPageIndexChanging="OnPageIndexChanging">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sentimiento">

                                        <ItemTemplate>
                                            <asp:Label ID="lb_cod_sentimiento_modal" Visible="false" runat="server" Text='<%# Bind("ID_SENTIMIENTO")%>' CssClass="small"></asp:Label>
                                            <asp:Label ID="lb_sentimiento_modal" runat="server" Text='<%# Bind("NOMBRE_SENTIMIENTO")%>' CssClass="small"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Palabras encontradas">

                                        <ItemTemplate>

                                            <asp:Label ID="lb_encontradas_modal" runat="server" Text='<%# Bind("REGISTROS_ENCONTRADOS")%>' CssClass="small"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Intensidad Calculada">

                                        <ItemTemplate>

                                            <asp:Label ID="lb_intensidad_calculada_modal" runat="server" Text='<%# Bind("INTENSIDAD_SENTIMIENTO")%>' CssClass="small"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Detalle ">

                                        <ItemTemplate>
                                            <asp:ImageButton ID="bt_detalle_palabras" runat="server" CausesValidation="False" CommandName="SubPalabra"
                                                ImageUrl="~/Imagenes/mostrar.png" Width="25px" Height="25px" Text="Plan" ToolTip="SubMenu"
                                                CommandArgument="<%# Container.DataItemIndex %>" data-toggle="modal" data-target="#test2" />



                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>


                            </asp:GridView>

                           



                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>

                <asp:Label ID="lbl_id_opinion" runat="server" Visible="false"></asp:Label>
            </div>

        </div>

    </div>



    <div id="test2" class="modal fade bs-example-modal-lg" role="dialog" style="z-index: 1600;">
        <div class="modal-dialog modal-lg" style=" width: 100%;height:1500px !important;">
            <!-- Modal content-->
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <h3 class="modal-title" id="myModalLabel2" runat="server" style="color: Black;"></h3>
                        <div class="modal-body">
                              <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                            <ProgressTemplate>
                                              
                                                <asp:Label ID="Labelll1" style="font-weight: 700;color:Black;" runat="server" Text="Cargando..."></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>                                      
                             <asp:GridView ID="GrillaModal_palabra" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                EmptyDataText="Sin Datos para Mostrar.." ClientIDMode="Static" ShowHeaderWhenEmpty="True"
                                CssClass="table table-bordered table-hover table-condensed table-striped" 
                                HorizontalAlign="Center" Width="100%" ShowFooter="false"  BackColor="white">
                                <Columns>
                                    <asp:TemplateField HeaderText="Palabra">

                                        <ItemTemplate>

                                          <asp:Label ID="lb_palabra_modal" runat="server" Text='<%# Bind("TEXTO")%>' CssClass="small"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Raiz Léxica">

                                        <ItemTemplate>

                                           <asp:Label ID="lb_raiz_modal" runat="server" Text='<%# Bind("RAIZ_LEXICA")%>' CssClass="small"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                
                                    <asp:TemplateField HeaderText="Sentimiento">

                                        <ItemTemplate>

                                           <asp:Label ID="lb_sentimiento_modal2" runat="server" Text='<%# Bind("NOMBRE_SENTIMIENTO")%>' CssClass="small"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                
                                    <asp:TemplateField HeaderText="Intensidad">

                                        <ItemTemplate>

                                          <asp:Label ID="lb_sentimiento_modal2" runat="server" Text='<%# Bind("INTENSIDAD")%>' CssClass="small"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                </asp:GridView>

                            

                        </div>
                          <asp:Label ID="lbl_id_opinion2" runat="server" Visible="false"></asp:Label>
                          <asp:Label ID="lbl_id_sentimiento" runat="server" Visible="false"></asp:Label>
                           <asp:Label ID="lbl_id_analisis" runat="server" Visible="false"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>

    </div>



</asp:Content>

