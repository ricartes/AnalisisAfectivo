Imports System.Data

Partial Class Mantenedores_ParametroGeneral
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
            Dim vLista As List(Of CL_Parametro) = DT_Parametro.SelectParametro()
            If vLista.Count > 0 Then

                tx_desplazamiento.Text = vLista.Item(0).VALOR_DESPLAZAMIENTO

            Else

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


    Public Sub cambia_desplazamiento()

        Dim msgError As New StringBuilder
        Dim dt As New DataTable
        Try

            DT_Parametro.DesplazamientoUpdate(tx_desplazamiento.Text.Trim, Session("Usuario"))
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert('Desplazamiento editado correctamente');", True)
        Catch ex As Exception
            Dim mensaje As String = ex.Message
            mensaje = mensaje.Replace(Chr(34), "'")
            mensaje = mensaje.Replace(Chr(13), "'")
            mensaje = mensaje.Replace(Chr(10), "'")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error1, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
        End Try




    End Sub





End Class
