Imports System.Data

Partial Class Mantenedores_PalabraNoEncontrada
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
            Dim vLista As List(Of CL_Palabra) = DT_Palabra.SelectPalabraNoEncontrada()
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


    Private Sub ShowNoResultFound(ByVal source As List(Of CL_Palabra), ByVal gv As GridView)
        '        source.Rows.Add(source.NewRow())
        Dim vUsuario As New CL_Palabra
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

    Public Function ComboSentimiento(ByVal combo As DropDownList) As String
        Dim msgError As New StringBuilder
        Dim dt As New System.Data.DataTable
        Try
            Dim vLista As List(Of ComboBox) = DT_ComboBox.Sentimiento()
            If vLista.Count > 0 Then
                combo.DataValueField() = "CODIGO"
                combo.DataTextField() = "descripcion"
                combo.DataSource = vLista
                combo.DataBind()
                'combo.Items.Insert(0, New ListItem("Todos", "0"))
            End If
        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try
    End Function

    Protected Sub bt_f_guardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim palabra As New CL_Palabra

        Dim cb As ImageButton = DirectCast(sender, ImageButton)
        Dim parentRow As GridViewRow = DirectCast(cb.NamingContainer, GridViewRow)

        palabra.CODIGO = CType(parentRow.FindControl("lb_cod_palabra"), Label).Text.Trim
        palabra.TEXTO = CType(parentRow.FindControl("lb_texto"), Label).Text.Trim
        palabra.CODIGO_SENTIMIENTO = CType(parentRow.FindControl("ddl_sentimiento"), DropDownList).SelectedValue
        palabra.INTENSIDAD = CType(parentRow.FindControl("tx_intensidad"), TextBox).Text.Trim


        DT_Palabra.InsertPalabra(palabra, Session("Usuario"))
        DT_Palabra.PalabraNoEncontradaDelete(palabra, Session("Usuario"))

        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert('Palabra ingresada correctamente. Revisar mantenedor de palabra...');", True)

        CargarGrilla()


        'palabra.CODIGO_SENTIMIENTO = ddl_sentimiento.SelectedValue

    End Sub

    Protected Sub RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grilla.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim drop As DropDownList = DirectCast(e.Row.FindControl("ddl_e_IO"), DropDownList)
            'Dim droja As DropDownList = DirectCast(e.Row.FindControl("ddl_e_ja"), DropDownList)
            ComboSentimiento(DirectCast(e.Row.FindControl("ddl_sentimiento"), DropDownList))

        End If
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Grilla.PageIndex = e.NewPageIndex
        CargarGrilla()
    End Sub



End Class
