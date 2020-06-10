Imports System.Data

Partial Class Mantenedores_Stopword
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
            Dim vLista As List(Of CL_Stopword) = DT_Stopword.SelectStopword()
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


    Private Sub ShowNoResultFound(ByVal source As List(Of CL_Stopword), ByVal gv As GridView)
        '        source.Rows.Add(source.NewRow())
        Dim vUsuario As New CL_Stopword
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


    Protected Sub bt_f_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            Dim stopword As New CL_Stopword
            stopword.TEXTO = CType(Grilla.FooterRow.FindControl("tx_f_texto"), TextBox).Text.Trim

            DT_Stopword.InsertStopword(stopword, Session("Usuario"))


            ' ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "exitoso('Nuevo usuario <i>insertado</i> correctamente');", True)


            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert('Stopword ingresado correctamente');", True)

            CargarGrilla()
        Catch ex As Exception
            Dim mensaje As String = ex.Message.ToString
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try

    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Grilla.PageIndex = e.NewPageIndex
        CargarGrilla()
    End Sub
    Protected Sub Grilla_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Grilla.RowEditing
        Grilla.EditIndex = e.NewEditIndex
        CargarGrilla()
    End Sub

    Protected Sub Grilla_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Grilla.RowCancelingEdit
        Dim msgError As New StringBuilder
        Try
            Grilla.EditIndex = -1
            CargarGrilla()
        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try
    End Sub

    Protected Sub Grilla_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Grilla.RowUpdating
        Try
            Dim fila As GridViewRow = Grilla.Rows(e.RowIndex)
            Dim registro As New CL_Stopword
            registro.CODIGO = CType(fila.FindControl("tx_e_cod_stopwowd"), Label).Text
            registro.TEXTO = CType(fila.FindControl("tx_e_texto"), TextBox).Text

            DT_Stopword.UpdateStopword(registro, Session("Usuario"))
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert('Stopword editado correctamente');", True)
            Grilla.EditIndex = -1
            CargarGrilla()

        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try
    End Sub


    Protected Sub Grilla_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Grilla.RowDeleting
        Try
            Dim fila As GridViewRow = Grilla.Rows(e.RowIndex)
            Dim registro As New CL_Stopword
            registro.CODIGO = DirectCast(fila.FindControl("lb_cod_stopword"), Label).Text


            DT_Stopword.DeleteStopword(registro, Session("Usuario"))

            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert('Stopword eliminada correctamente');", True)


            CargarGrilla()
        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try
    End Sub

End Class
