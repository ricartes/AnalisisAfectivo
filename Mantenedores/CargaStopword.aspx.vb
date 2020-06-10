Imports System.Data.OleDb
Imports System.IO
Imports System.Data

Partial Class Mantenedores_CargaStopword
    Inherits System.Web.UI.Page

    Protected Sub buscar() Handles btnBuscar_carga.Click
        carga_excel()
    End Sub



    Protected Sub carga_excel()
        If System.IO.Path.GetExtension(txtArchivo.FileName) = ".xlsx" Or System.IO.Path.GetExtension(txtArchivo.FileName) = ".xls" And txtArchivo.FileName <> "" Then
            Dim NombreFinalArchivo As String = Now.ToFileTimeUtc
            Dim path As String = Server.MapPath("~/tmp/" & NombreFinalArchivo & ".xlsx")
            Dim Arch As HttpPostedFile
            Dim oConn As New OleDbConnection
            Dim oCmd As New OleDbCommand
            Dim oDa As New OleDbDataAdapter
            Dim oDs As New DataSet
            Dim contFila As Integer
            Dim columnas As Integer
            Dim lstPE As New List(Of CL_Stopword)


            Try

                Arch = txtArchivo.PostedFile
                Arch.SaveAs(path)
                'Si el archivo existe...
                If File.Exists(path) Then

                    'Atención: Esta es la cadena de conexión. La misma lee el archivo especificado en el path
                    oConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
                    path & "; Extended Properties='Excel 12.0 Xml;IMEX=1'"
                    ' Abrir la conexión, y leer [Hoja 1] del archivo Excel
                    oConn.Open()

                    Dim dtSchema As DataTable = oConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
                    Dim Sheet1 As String = dtSchema.Rows(0).Item("TABLE_NAME").ToString

                    If Sheet1 <> "stopword$" Then
                        ScriptManager.RegisterStartupScript(Page, Me.GetType, "", "alert(""El nombre de la HOJA del LIBRO ingresado no corresponde a stopword. Recuerde que la HOJA del LIBRO debe llamarse stopword"");", True)
                        If File.Exists(path) Then
                            If Not oConn Is Nothing And oConn.State = ConnectionState.Open Then
                                oConn.Close()
                            End If
                            System.IO.File.Delete(path)
                        End If
                        Exit Sub
                    End If

                    oCmd.CommandText = "SELECT * FROM [stopword$]"

                    oCmd.Connection = oConn
                    oDa.SelectCommand = oCmd
                    'Llenar el DataSet
                    oDa.Fill(oDs, "TrozosPlantas")
                    'Comenzamos el Ciclo
                    columnas = oDs.Tables(0).Rows.Count - 1
                    For i = 0 To columnas
                        If Not oDs.Tables(0).Rows(contFila).Item(0).ToString.Trim = "" Then
                            Dim Acargar As New CL_Stopword
                            Acargar.TEXTO = oDs.Tables(0).Rows(contFila).Item(0).ToString.Trim

                            lstPE.Add(Acargar)
                        End If
                        contFila = contFila + 1
                    Next

                    'Ciclo de Guardado
                    Dim hay_error As Boolean = False
                    Dim cont As Integer = 0
                    Dim msjesd As String = DT_Stopword.DeleteStopwordALL(Session("Usuario"))
                    For Each pe In lstPE

                        Dim msj As String = DT_Stopword.InsertStopword(pe, Session("Usuario"))

                        If msj <> "" Then
                            'hay_error = True
                        End If
                        cont = cont + 1
                    Next
                    'Asignar al valor de la combo el código del cliente
                    'ACA PUEDE MANDAR EL ERROR
                    oConn.Close()
                    'Borramos el archivo
                    'Borramos el archivo
                    System.IO.File.Delete(path)

                    If hay_error = True Then
                        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error al cargar algún registro de la planilla."");", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Planilla cargada correctamente. " & cont & " elementos cargados"");", True)
                    End If

                Else
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Archivo no se ha creado en el directorio."");", True)
                End If

            Catch ex As Exception
                Dim mensaje As String = ex.Message
                mensaje = mensaje.Replace(Chr(34), "'")
                mensaje = mensaje.Replace(Chr(13), "'")
                mensaje = mensaje.Replace(Chr(10), "'")
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert(""Se presento un error, comuniquese con el administrador.<br><br>Mensaje de Error: " & mensaje & """);", True)
                If File.Exists(path) Then
                    If Not oConn Is Nothing And oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If
                    System.IO.File.Delete(path)
                End If
            End Try
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Error", "alert('Formato del archivo invalido');", True)
        End If
    End Sub

End Class
