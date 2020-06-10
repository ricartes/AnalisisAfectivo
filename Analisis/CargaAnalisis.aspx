<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CargaAnalisis.aspx.vb" Inherits="Analisis_CargaAnalisis" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <style>

            input[type="text"]
        {
            font-size:12px;
        }

        /* The switch - the box around the slider */
        .switch
        {
            position: relative;
            display: inline-block;
            width: 20px;
            height: 17px;
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
            left:2px;
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
        $(function () {
            $(".fechita").datepicker({
                changeMonth: true,
                yearRange: "-120:+1",
                changeYear: true,
                dateFormat: "01/mm/yy"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <h1 class="h3 mb-1 text-gray-800">Análisis: Carga Planilla</h1>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        <table title="Carga planilla análisis"  class="table"  style="width:100%">

           <tr>
            <th colspan="6" >Carga planilla: Debe seleccionar un archivo xlsx. La HOJA del LIBRO debe llamarse opiniones. Este debe contener en la columna A el texto de la opinión, y en la columna B el nombre del sentimiento dado por el experto. <br /> Ingresar los datos necesarios para la relización.</th>
        </tr>
            <tr>

            <th>
                <span">Nombre</span>
            </th>
             
               
            <th>
                <asp:TextBox ID="tx_nombre" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
            </th>



                
            <th>
                <asp:FileUpload ID="txtArchivo" runat="server" />
            </th>
            <th>
                <asp:Button ID="btnBuscar_carga" CssClass="btn btn-success" runat="server" Text="Cargar" />
            </th>
        </tr>
            <tr><th colspan="6"> <asp:Label runat="server" ID="mensajito"></asp:Label></th></tr>
    </table>
</asp:Content>

