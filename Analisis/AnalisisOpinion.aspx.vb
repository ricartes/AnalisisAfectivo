Imports System.Data
Imports System.IO

Partial Class Analisis_AnalisisOpinion
    Inherits System.Web.UI.Page

    Dim id As String
    Dim nombre As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If Not String.IsNullOrEmpty(Request.QueryString("id_analisis")) And Not String.IsNullOrEmpty(Request.QueryString("nombre_analisis")) Then
                ' Access the value ("Value1") '
                id = Request.QueryString("id_analisis")
                nombre = Request.QueryString("nombre_analisis")
                lb_nombre_analisis.Text = nombre
            End If

            CargarGrilla()
        End If
    End Sub

    Public Function CargarGrilla() As String
        Dim msgError As New StringBuilder
        Dim dt As New DataTable
        Try

            Dim vLista As List(Of CL_Opinion) = DT_Opinion.SelectOpinionAnalisis(id)
            If vLista.Count > 0 Then
                Grilla.DataSource = vLista
                Grilla.DataBind()


            Else
                ShowNoResultFound(vLista, Grilla)
            End If
            ' Now that you have your value, you could populate a TextBox with it'

        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error1, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try

        Return msgError.ToString
    End Function


    Public Function CargarGrillaDetalleOpinion(ByVal id_opinion As String) As String
        Dim msgError As New StringBuilder
        Dim dt As New DataTable
        Try

            Dim vLista As List(Of CL_DetalleOpinion) = DT_Opinion.SelectDetalleOpinion(id_opinion)
            If vLista.Count > 0 Then
                GrillaModal.DataSource = vLista
                GrillaModal.DataBind()

            Else
                ShowNoResultFoundGrillaDetalle(vLista, GrillaModal)
            End If
            ' Now that you have your value, you could populate a TextBox with it'
            'GrillaModal.Visible = True
        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error1, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try

        Return msgError.ToString
    End Function



    Public Function CargarGrillaPalabrasOpinion(ByVal id_opinion As String, ByVal id_analisis As String, ByVal id_sentimiento As String) As String
        Dim msgError As New StringBuilder
        Dim dt As New DataTable
        Try

            Dim vLista As List(Of CL_PalabraSentimiento) = DT_Opinion.SelectPalabraSentimiento(lbl_id_opinion.Text, Request.QueryString("id_analisis"), lbl_id_sentimiento.Text)
            If vLista.Count > 0 Then
                GrillaModal_palabra.DataSource = vLista
                GrillaModal_palabra.DataBind()

            Else
                ShowNoResultFoundGrillaDetalle2(vLista, GrillaModal_palabra)
            End If
            ' Now that you have your value, you could populate a TextBox with it'

        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error1, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try

        Return msgError.ToString
    End Function


    Private Sub ShowNoResultFound(ByVal source As List(Of CL_Opinion), ByVal gv As GridView)
        '        source.Rows.Add(source.NewRow())
        Dim vUsuario As New CL_Opinion
        source.Add(vUsuario)
        ' create a new blank row to the DataTable
        ' Bind the DataTable which contain a blank row to the GridView
        gv.DataSource = source
        gv.DataBind()
        ' Get the total number of columns in the GridView to know what the Column Span should be
        Dim columnsCount As Integer = gv.Columns.Count
        gv.Rows(0).Cells.Clear()
        ' clear all the cells in the row
        gv.Rows(0).Cells.Add(New TableCell())
        'add a new blank cell
        gv.Rows(0).Cells(0).ColumnSpan = columnsCount
        'set the column span to the new added cell
        'You can set the styles here
        gv.Rows(0).Cells(0).HorizontalAlign = HorizontalAlign.Center
        gv.Rows(0).Cells(0).ForeColor = System.Drawing.Color.Red
        gv.Rows(0).Cells(0).Font.Bold = True
        'set No Results found to the new added cell
        gv.Rows(0).Visible = False
    End Sub


    Private Sub ShowNoResultFoundGrillaDetalle(ByVal source As List(Of CL_DetalleOpinion), ByVal gv As GridView)
        '        source.Rows.Add(source.NewRow())
        Dim vUsuario As New CL_DetalleOpinion
        source.Add(vUsuario)
        ' create a new blank row to the DataTable
        ' Bind the DataTable which contain a blank row to the GridView
        gv.DataSource = source
        gv.DataBind()
        ' Get the total number of columns in the GridView to know what the Column Span should be
        Dim columnsCount As Integer = gv.Columns.Count
        gv.Rows(0).Cells.Clear()
        ' clear all the cells in the row
        gv.Rows(0).Cells.Add(New TableCell())
        'add a new blank cell
        gv.Rows(0).Cells(0).ColumnSpan = columnsCount
        'set the column span to the new added cell
        'You can set the styles here
        gv.Rows(0).Cells(0).HorizontalAlign = HorizontalAlign.Center
        gv.Rows(0).Cells(0).ForeColor = System.Drawing.Color.Red
        gv.Rows(0).Cells(0).Font.Bold = True
        'set No Results found to the new added cell
        gv.Rows(0).Visible = False
    End Sub


    Private Sub ShowNoResultFoundGrillaDetalle2(ByVal source As List(Of CL_PalabraSentimiento), ByVal gv As GridView)
        '        source.Rows.Add(source.NewRow())
        Dim vUsuario As New CL_PalabraSentimiento
        source.Add(vUsuario)
        ' create a new blank row to the DataTable
        ' Bind the DataTable which contain a blank row to the GridView
        gv.DataSource = source
        gv.DataBind()
        ' Get the total number of columns in the GridView to know what the Column Span should be
        Dim columnsCount As Integer = gv.Columns.Count
        gv.Rows(0).Cells.Clear()
        ' clear all the cells in the row
        gv.Rows(0).Cells.Add(New TableCell())
        'add a new blank cell
        gv.Rows(0).Cells(0).ColumnSpan = columnsCount
        'set the column span to the new added cell
        'You can set the styles here
        gv.Rows(0).Cells(0).HorizontalAlign = HorizontalAlign.Center
        gv.Rows(0).Cells(0).ForeColor = System.Drawing.Color.Red
        gv.Rows(0).Cells(0).Font.Bold = True
        'set No Results found to the new added cell
        gv.Rows(0).Visible = False
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Grilla.PageIndex = e.NewPageIndex
        CargarGrilla()
    End Sub

    Protected Sub OnPageIndexChangingDetalle(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GrillaModal.PageIndex = e.NewPageIndex
        CargarGrillaDetalleOpinion(lbl_id_opinion.Text)
    End Sub


    Private Sub Grilla_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grilla.RowCommand
        Try
            Dim fila As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).Parent.Parent, GridViewRow)
            Select Case e.CommandName
                Case ("Sub")
                    GrillaModal.Visible = False
                    myModalLabel.InnerText = "Perfil Opinión"
                    lbl_id_opinion.Text = CType(fila.FindControl("lb_id_opinion"), Label).Text
                    lb_texto_opinion_modal.Text = CType(fila.FindControl("lb_texto_opinion"), Label).Text
                    lb_texto_preprocesado_modal.Text = CType(fila.FindControl("lb_prepro_opinion"), Label).Text
                    CargarGrillaDetalleOpinion(lbl_id_opinion.Text)
                    GrillaModal.Visible = True
                    'CargarGrillaSubmenu(CType(fila.FindControl("lb_codmenu"), Label).Text)

            End Select
        Catch ex As Exception

        End Try
    End Sub


    Private Sub GrillaModal_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrillaModal.RowCommand
        Try
            Dim fila As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).Parent.Parent, GridViewRow)
            Select Case e.CommandName
                Case ("SubPalabra")
                    myModalLabel2.InnerText = "Detalle Sentimiento"
                    lbl_id_sentimiento.Text = CType(fila.FindControl("lb_cod_sentimiento_modal"), Label).Text
                    CargarGrillaPalabrasOpinion(lbl_id_opinion.Text, id, lbl_id_sentimiento.Text)
                    'lbl_id_opinion.Text = CType(fila.FindControl("lb_id_opinion"), Label).Text
                    'lb_texto_opinion_modal.Text = CType(fila.FindControl("lb_texto_opinion"), Label).Text
                    'lb_texto_preprocesado_modal.Text = CType(fila.FindControl("lb_prepro_opinion"), Label).Text
                    'CargarGrillaDetalleOpinion(lbl_id_opinion.Text)
                    'CargarGrillaSubmenu(CType(fila.FindControl("lb_codmenu"), Label).Text)

            End Select
        Catch ex As Exception

        End Try
    End Sub


    Public Sub ExportarExcel()

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" + Request.QueryString("nombre_analisis") + ".xls")
        Response.Charset = ""
        Response.ContentEncoding = System.Text.Encoding.Unicode
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
        Response.ContentType = "application/vnd.ms-excel"


        Dim vLista As List(Of CL_Opinion) = DT_Opinion.SelectOpinionReport(Request.QueryString("id_analisis"))
        If vLista.Count > 0 Then
            Grilla2.DataSource = vLista
            Grilla2.DataBind()
            Grilla2.Visible = True

        End If




        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            Grilla2.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()


        End Using

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub


    Protected Sub RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grilla2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow AndAlso Grilla2.EditIndex <> e.Row.RowIndex Then
            Dim lb_intensidad As Label = DirectCast(e.Row.FindControl("lb_intensidad_predominante"), Label)

            If lb_intensidad.Text = "0" Then
                lb_intensidad.Text = ""
            End If
        End If

    End Sub

End Class
