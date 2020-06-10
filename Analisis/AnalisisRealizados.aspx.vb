Imports System.Data

Partial Class Analisis_AnalisisRealizados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarGrilla()
        End If
    End Sub

    Public Function CargarGrilla() As String
        Dim msgError As New StringBuilder
        Dim dt As New DataTable
        Try
            Dim vLista As List(Of CL_Analisis) = DT_Analisis.SelectAnalisis()
            If vLista.Count > 0 Then
                Grilla.DataSource = vLista
                Grilla.DataBind()

            Else
                ShowNoResultFound(vLista, Grilla)
            End If
        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error1, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try

        Return msgError.ToString
    End Function


    Private Sub ShowNoResultFound(ByVal source As List(Of CL_Analisis), ByVal gv As GridView)
        '        source.Rows.Add(source.NewRow())
        Dim vUsuario As New CL_Analisis
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


    Private Sub Grilla_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grilla.RowCommand
        Try

            Select Case e.CommandName
                Case "detalle"
                    Dim fila As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).Parent.Parent, GridViewRow)
                    Dim id As String = CType(fila.FindControl("lb_id_analisis"), Label).Text
                    Dim nombre As String = CType(fila.FindControl("lb_nombre_analisis"), Label).Text
                    Response.Redirect("/Analisis/AnalisisOpinion.aspx?id_analisis=" + id + "&nombre_analisis=" + nombre)
            End Select

        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")

        End Try

    End Sub
End Class
