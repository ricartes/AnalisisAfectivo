Imports System.Data.OleDb
Imports System.Data
Imports System.IO

Partial Class Analisis_CargaAnalisis
    Inherits System.Web.UI.Page
    Dim separador As New CL_SeparaSilava
    Dim stemmer As New SpanishStemmer.Stemmer
    Dim stemmer2 As Snowball.Stemmer = GetType(Snowball.Stemmer).Assembly.GetTypes().Where(Function(t) t.IsSubclassOf(GetType(Snowball.Stemmer)) AndAlso Not t.IsAbstract).Where(Function(t) match(t.Name, "spanish")).[Select](Function(t) CType(Activator.CreateInstance(t), Snowball.Stemmer)).FirstOrDefault()







    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then


           
            'If Not stemmer2 Is Nothing Then
            '    Dim asi As String = stemmer2.Stem("odie")
            'End If

            'Dim LISTA_PALABRAS_SENTIMIENTO As List(Of CL_Palabra) = DT_Palabra.SelectPalabraTODAS()
            'Dim stemmer As New SpanishStemmer.Stemmer



            'For Each item As CL_Palabra In LISTA_PALABRAS_SENTIMIENTO
            '    Dim a As String = stemmer2.Stem(item.TEXTO)
            '    DT_Palabra.PalabraRaizLexica(item, a, "")
            'Next






            'OBTENER_SENTIMIENTO_V2("no parece profesor preparado clases no explica materias, monotono, no ejerce interaccion estudiantes, lee textos. no preocupa alumnos esten aprendiendo solo preocupa cumplir hora clases.", LISTA_NEGACION, LISTA_PALABRAS_SENTIMIENTO)
        End If
    End Sub


    ' AGREGAR RAIZ LEXICAS A CADA PALABRA
    ' HACER UN REMOVE DE PUNTOS, COMA A CADA FRASE 
    Private Function OBTENER_SENTIMIENTO_V2(ByVal opinion As CL_Opinion, ByVal ANALISIS As CL_Analisis, ByVal LISTA_NEGACION As List(Of String), ByVal LISTA_PALABRAS_SENTIMIENTO As List(Of String)) As Integer
        'SEPARO EN FRASES, PUNTOS Y COMMAS
        Dim TEMPORAL As String() = opinion.PREPROCESADO.Split(New Char() {"."c, ","c}, StringSplitOptions.RemoveEmptyEntries)
        Dim LISTA_FRASES As List(Of String) = New List(Of String)(TEMPORAL)


        'VEO SI HAY FRASES QUE TERMINAN EN . Y ,
        If LISTA_FRASES.Count > 1 Then
            ' RECORRO CADA FRASE
            For Each item As String In LISTA_FRASES
                If item <> "" Then
                    item = item.Replace(".", "")
                    item = item.Replace(",", "")
                    'OBTENGO LA LISTA DE PALABRAS PARA ESA FRASE
                    Dim TEMPORAL_PALABRAS As String() = item.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
                    Dim LISTA_PALABRAS As List(Of String) = New List(Of String)(TEMPORAL_PALABRAS)
                    'INDICADOR SI LA FRASE TIENE NEGADOR, COMIENZA EN 0
                    Dim ALERTA_NEGADOR = 0
                    'RECORRER LA LISTA DE PALABRAS
                    For I As Integer = 0 To LISTA_PALABRAS.Count - 1

                        If LISTA_PALABRAS.Item(I) <> "" Then
                            'VER SI HAY UN NEGADOR Y NO ESTA ACTIVA LA LISTA DE NEGADORES
                            If LISTA_NEGACION.Contains(LISTA_PALABRAS.Item(I)) And ALERTA_NEGADOR = 0 Then
                                'ACTIVO LA ALERTA NEGADOR
                                DT_Opinion.AUMENTAR_CONTADOR_NEGADOR(opinion, Session("Usuario"))
                                ALERTA_NEGADOR = 1
                            Else

                                'SI ESTA LA PALABRA EN EL DICCIONARIO, CONSIDERARLA
                                If LISTA_PALABRAS_SENTIMIENTO.Contains(LISTA_PALABRAS.Item(I)) Then
                                    DT_Opinion.AUMENTAR_CONTADOR_OPINION(opinion, Session("Usuario"))
                                    Dim palabra_encontradas As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorTexto(LISTA_PALABRAS.Item(I))

                                    For Each palabra_encontrada In palabra_encontradas

                                        If ALERTA_NEGADOR = 1 Then
                                            Dim A = "AUMENTA CON NEGADOR"

                                            DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                            palabra_encontrada.INTENSIDAD = DT_Sentimiento.MinimaIntensidad(palabra_encontrada, Session("Usuario"))
                                            DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                            DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, palabra_encontrada, Session("Usuario"))
                                            'ALERTA_NEGADOR = 0
                                        Else

                                            Dim A = "AUMENTA SIN NEGADOR"
                                            DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                            DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                            DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, palabra_encontrada, Session("Usuario"))
                                        End If
                                    Next


                                Else
                                    Dim STEMIZADO As String = stemmer2.Stem(LISTA_PALABRAS.Item(I))
                                    'Dim STEMIZADO As String = DT_Palabra.SelectStemizado(LISTA_PALABRAS.Item(I))
                                    If STEMIZADO <> "" Then


                                        If LISTA_PALABRAS_SENTIMIENTO.Contains(STEMIZADO) Then
                                            DT_Opinion.AUMENTAR_CONTADOR_OPINION(opinion, Session("Usuario"))
                                            Dim palabra_encontradas As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorTexto(STEMIZADO)

                                            For Each palabra_encontrada In palabra_encontradas
                                                If ALERTA_NEGADOR = 1 Then
                                                    Dim A = "AUMENTA CON NEGADOR"
                                                    DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                                    palabra_encontrada.INTENSIDAD = DT_Sentimiento.MinimaIntensidad(palabra_encontrada, Session("Usuario"))
                                                    DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                                    DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, palabra_encontrada, Session("Usuario"))
                                                    'ALERTA_NEGADOR = 0
                                                Else

                                                    Dim A = "AUMENTA SIN NEGADOR"
                                                    DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                                    DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                                    DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, palabra_encontrada, Session("Usuario"))
                                                End If
                                            Next

                                        Else
                                            Dim COINCIDENCIAS As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorRaiz(STEMIZADO)
                                            If COINCIDENCIAS.Count > 0 Then
                                                DT_Opinion.AUMENTAR_CONTADOR_OPINION(opinion, Session("Usuario"))
                                                For Each COINCIDENCIA In COINCIDENCIAS
                                                    If ALERTA_NEGADOR = 1 Then
                                                        DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, COINCIDENCIA, Session("Usuario"))
                                                        COINCIDENCIA.INTENSIDAD = DT_Sentimiento.MinimaIntensidad(COINCIDENCIA, Session("Usuario"))
                                                        DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, COINCIDENCIA, Session("Usuario"))
                                                        DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, COINCIDENCIA, Session("Usuario"))
                                                        Dim A = "AUMENTA CON NEGADOR"
                                                        'ALERTA_NEGADOR = 0
                                                    Else

                                                        Dim A = "AUMENTA SIN NEGADOR"
                                                        DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, COINCIDENCIA, Session("Usuario"))
                                                        DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, COINCIDENCIA, Session("Usuario"))
                                                        DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, COINCIDENCIA, Session("Usuario"))
                                                    End If
                                                Next
                                            Else
                                                If DT_Palabra.ExistePalabra(LISTA_PALABRAS.Item(I), Session("Usuario")) = 0 Then
                                                    DT_Palabra.InsertPalabraNoEncontrada(LISTA_PALABRAS.Item(I), opinion, Session("Usuario"))

                                                End If




                                                'DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, COINCIDENCIA.Item(0), Session("Usuario"))
                                                'DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, COINCIDENCIA.Item(0), Session("Usuario"))
                                                'encontrado = 1
                                                ' ALERTA_NEGADOR = 0
                                                ' DESPLAZADO = K
                                                ' GoTo encontrado_con_negador
                                            End If
                                        End If



                                    Else
                                        If DT_Palabra.ExistePalabra(LISTA_PALABRAS.Item(I), Session("Usuario")) = 0 Then
                                            DT_Palabra.InsertPalabraNoEncontrada(LISTA_PALABRAS.Item(I), opinion, Session("Usuario"))

                                        End If

                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            Next

        Else
            opinion.PREPROCESADO = opinion.PREPROCESADO.Replace(".", "")
            opinion.PREPROCESADO = opinion.PREPROCESADO.Replace(",", "")
            'HACER UN REMOVE DE PUNTOS, COMA A LA FRASE ENTERA 
            Dim TEMPORAL_PALABRAS As String() = opinion.PREPROCESADO.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
            Dim LISTA_PALABRAS As List(Of String) = New List(Of String)(TEMPORAL_PALABRAS)
            Dim ALERTA_NEGADOR = 0
            Dim DESPLAZADO As Integer
            'RECORRER LA LISTA DE PALABRAS
            For I As Integer = 0 To LISTA_PALABRAS.Count - 1

                If LISTA_PALABRAS.Item(I) <> "" Then
                    'VER SI HAY UN NEGADOR
                    If LISTA_NEGACION.Contains(LISTA_PALABRAS.Item(I)) And ALERTA_NEGADOR = 0 Then
                        DT_Opinion.AUMENTAR_CONTADOR_NEGADOR(opinion, Session("Usuario"))
                        ALERTA_NEGADOR = 1
                        DESPLAZADO = 0
                    Else

                        'SI MI ALERTA DE NEGADOR ESTA ACTIVO, A BUSCAR LAS PALABRAS EN EL DESPLAZAMIENTO
                        If ALERTA_NEGADOR = 1 Then
                            'REEMPLAZAR EL 4 POR LA CANTIDAD DE DESPLAZAMIENTO DEL NEGADOR
                            If (DESPLAZADO < ANALISIS.DESPLAZAMIENTO) Then

                                If LISTA_PALABRAS_SENTIMIENTO.Contains(LISTA_PALABRAS.Item(I)) Then
                                    DT_Opinion.AUMENTAR_CONTADOR_OPINION(opinion, Session("Usuario"))
                                    Dim palabra_encontradas As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorTexto(LISTA_PALABRAS.Item(I))

                                    For Each palabra_encontrada In palabra_encontradas
                                        Dim A = "AUMENTA CON NEGADOR"
                                        palabra_encontrada.INTENSIDAD = DT_Sentimiento.MinimaIntensidad(palabra_encontrada, Session("Usuario"))
                                        DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                        DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                        DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, palabra_encontrada, Session("Usuario"))

                                    Next



                                    ' OJO, ESTE CASO ES CON NEGADOR!!!! SE DEBE CONSIDERAR EL MINIMO PARA LA POLARIDAD ENCONTRADA

                                    'DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, LISTA_PALABRAS.Item(I), Session("Usuario"))
                                    'DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, COINCIDENCIA.Item(0), Session("Usuario"))

                                Else
                                    Dim STEMIZADO As String = stemmer2.Stem(LISTA_PALABRAS.Item(I))
                                    'Dim STEMIZADO As String = DT_Palabra.SelectStemizado(LISTA_PALABRAS.Item(I))
                                    If STEMIZADO <> "" Then


                                        If LISTA_PALABRAS_SENTIMIENTO.Contains(STEMIZADO) Then
                                            DT_Opinion.AUMENTAR_CONTADOR_OPINION(opinion, Session("Usuario"))
                                            Dim palabra_encontradas As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorTexto(STEMIZADO)

                                            For Each palabra_encontrada In palabra_encontradas
                                                Dim A = "AUMENTA CON NEGADOR"
                                                palabra_encontrada.INTENSIDAD = DT_Sentimiento.MinimaIntensidad(palabra_encontrada, Session("Usuario"))
                                                DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                                DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                                DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, palabra_encontrada, Session("Usuario"))

                                            Next

                                        Else

                                            Dim COINCIDENCIAS As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorRaiz(STEMIZADO)
                                            If COINCIDENCIAS.Count > 0 Then
                                                DT_Opinion.AUMENTAR_CONTADOR_OPINION(opinion, Session("Usuario"))
                                                For Each COINCIDENCIA In COINCIDENCIAS
                                                    Dim A = "AUMENTA CON NEGADOR"
                                                    DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, COINCIDENCIA, Session("Usuario"))
                                                    COINCIDENCIA.INTENSIDAD = DT_Sentimiento.MinimaIntensidad(COINCIDENCIA, Session("Usuario"))
                                                    DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, COINCIDENCIA, Session("Usuario"))
                                                    DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, COINCIDENCIA, Session("Usuario"))
                                                Next
                                            Else
                                                If DT_Palabra.ExistePalabra(LISTA_PALABRAS.Item(I), Session("Usuario")) = 0 Then
                                                    DT_Palabra.InsertPalabraNoEncontrada(LISTA_PALABRAS.Item(I), opinion, Session("Usuario"))

                                                End If
                                            End If






                                            'DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, COINCIDENCIA.Item(0), Session("Usuario"))

                                            ' OJO, ESTE CASO ES CON NEGADOR!!!! SE DEBE CONSIDERAR EL MINIMO PARA LA POLARIDAD ENCONTRADA


                                            'DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, COINCIDENCIA.Item(0), Session("Usuario"))
                                            'DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, COINCIDENCIA.Item(0), Session("Usuario"))
                                            'encontrado = 1
                                            ' ALERTA_NEGADOR = 0
                                            ' DESPLAZADO = K
                                            ' GoTo encontrado_con_negador
                                        End If
                                    Else
                                        If DT_Palabra.ExistePalabra(LISTA_PALABRAS.Item(I), Session("Usuario")) = 0 Then
                                            DT_Palabra.InsertPalabraNoEncontrada(LISTA_PALABRAS.Item(I), opinion, Session("Usuario"))

                                        End If

                                    End If

                                End If

                                DESPLAZADO = DESPLAZADO + 1
                            Else
                                'SI YA ME DESPLAZE TODO LO POSIBLE, CAMBIAR EL ALERTA DE NEGADOR A 0
                                ALERTA_NEGADOR = 0

                            End If
                        Else
                            'SI EL ALERTA DE NEGADOR ES 0, REALIZAR BUSQUEDA NORMAL
                            'SI ESTA LA PALABRA EN EL DICCIONARIO, CONSIDERARLA
                            If LISTA_PALABRAS_SENTIMIENTO.Contains(LISTA_PALABRAS.Item(I)) Then
                                DT_Opinion.AUMENTAR_CONTADOR_OPINION(opinion, Session("Usuario"))
                                Dim palabra_encontradas As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorTexto(LISTA_PALABRAS.Item(I))

                                Dim A = "AUMENTA SIN NEGADOR"

                                For Each palabra_encontrada In palabra_encontradas

                                    DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                    DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                    DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, palabra_encontrada, Session("Usuario"))
                                Next
                            Else
                                Dim STEMIZADO As String = stemmer2.Stem(LISTA_PALABRAS.Item(I))
                                'Dim STEMIZADO As String = DT_Palabra.SelectStemizado(LISTA_PALABRAS.Item(I))
                                If STEMIZADO <> "" Then


                                    If LISTA_PALABRAS_SENTIMIENTO.Contains(STEMIZADO) Then
                                        DT_Opinion.AUMENTAR_CONTADOR_OPINION(opinion, Session("Usuario"))
                                        Dim palabra_encontradas As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorTexto(STEMIZADO)

                                        For Each palabra_encontrada In palabra_encontradas
                                            Dim A = "AUMENTA CON NEGADOR"
                                            palabra_encontrada.INTENSIDAD = DT_Sentimiento.MinimaIntensidad(palabra_encontrada, Session("Usuario"))
                                            DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                            DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, palabra_encontrada, Session("Usuario"))
                                            DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, palabra_encontrada, Session("Usuario"))

                                        Next

                                    Else
                                        Dim COINCIDENCIAS As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorRaiz(STEMIZADO)
                                        If COINCIDENCIAS.Count > 0 Then
                                            DT_Opinion.AUMENTAR_CONTADOR_OPINION(opinion, Session("Usuario"))
                                            For Each COINCIDENCIA In COINCIDENCIAS
                                                Dim A = "AUMENTA SIN NEGADOR"

                                                DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(opinion, COINCIDENCIA, Session("Usuario"))
                                                DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(opinion, COINCIDENCIA, Session("Usuario"))
                                                DT_Opinion.INGRESA_PALABRA_SENTIMIENTO(opinion, ANALISIS, COINCIDENCIA, Session("Usuario"))

                                            Next
                                        Else
                                            If DT_Palabra.ExistePalabra(LISTA_PALABRAS.Item(I), Session("Usuario")) = 0 Then
                                                DT_Palabra.InsertPalabraNoEncontrada(LISTA_PALABRAS.Item(I), opinion, Session("Usuario"))
                                            End If
                                        End If

                                        If DT_Palabra.ExistePalabra(LISTA_PALABRAS.Item(I), Session("Usuario")) = 0 Then
                                            DT_Palabra.InsertPalabraNoEncontrada(LISTA_PALABRAS.Item(I), opinion, Session("Usuario"))
                                        End If
                                    End If
                                End If




                            End If

                        End If

                    End If
                End If


            Next

        End If





    End Function

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
            Dim lstPE As New List(Of CL_Opinion)


            Try

                Arch = txtArchivo.PostedFile
                Arch.SaveAs(path)
                'Si el archivo existe...
                If File.Exists(path) Then

                    'Atención: Esta es la cadena de conexión. La misma lee el archivo especificado en el path
                    oConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
                    path & "; Extended Properties='Excel 12.0 Xml;IMEX=1;HDR=NO'"
                    ' Abrir la conexión, y leer [Hoja 1] del archivo Excel
                    oConn.Open()

                    Dim dtSchema As DataTable = oConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
                    Dim Sheet1 As String = dtSchema.Rows(0).Item("TABLE_NAME").ToString

                    If Sheet1 <> "opiniones$" Then
                        ScriptManager.RegisterStartupScript(Page, Me.GetType, "", "alert(""El nombre de la HOJA del LIBRO ingresado no corresponde a opiniones. Recuerde que HOJA del LIBRO debe llamarse opiniones"");", True)
                        If File.Exists(path) Then
                            If Not oConn Is Nothing And oConn.State = ConnectionState.Open Then
                                oConn.Close()
                            End If
                            System.IO.File.Delete(path)
                        End If
                        Exit Sub
                    End If

                    oCmd.CommandText = "SELECT * FROM [opiniones$]"

                    oCmd.Connection = oConn
                    oDa.SelectCommand = oCmd
                    'Llenar el DataSet
                    oDa.Fill(oDs, "opiniones")
                    'Comenzamos el Ciclo
                    columnas = oDs.Tables(0).Rows.Count - 1
                    'DESPUES BORRAR!!!!!
                    'columnas = 10
                    For i = 0 To columnas
                        If Not oDs.Tables(0).Rows(contFila).Item(0).ToString.Trim = "" Then
                            Dim Acargar As New CL_Opinion
                            Acargar.TEXTO = oDs.Tables(0).Rows(contFila).Item(0).ToString.Trim


                            Acargar.NOMBRE_SENTIMIENTO_USUARIO = oDs.Tables(0).Rows(contFila).Item(1).ToString.Trim
                            If Acargar.NOMBRE_SENTIMIENTO_USUARIO <> "" Then
                                Acargar.SENTIMIENTO_USUARIO = DT_Sentimiento.SentimientoPorTexto(Acargar.NOMBRE_SENTIMIENTO_USUARIO).Item(0).CODIGO
 
                            End If

                            lstPE.Add(Acargar)
                        End If
                        contFila = contFila + 1
                    Next

                    Dim analisis As New CL_Analisis()
                    analisis.NOMBRE = tx_nombre.Text.ToString.Trim
                    analisis.DESPLAZAMIENTO = Integer.Parse(DT_Parametro.SelectParametro().Item(0).VALOR_DESPLAZAMIENTO)
                    analisis.FECHA = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                    analisis.CANTIDAD_OPINIONES = contFila

                    Dim ID_ANALISIS As Integer = INGRESA_ANALISIS(analisis)



                    Dim hay_error As Boolean = False
                    Dim cont As Integer = 0

                    Dim LISTA_NEGACION As List(Of String) = DT_Negacion.SelectNegacionString()
                    Dim LISTA_MONOSILABOS As List(Of String) = DT_Monosilabo.SelectMonosilaboString()
                    Dim LISTA_PALABRAS_SENTIMIENTO As List(Of String) = DT_Palabra.SelectPlabraString()

                    For Each pe In lstPE
                        pe = PREPROCESAMIENTO(pe)
                        pe.ID_ANALISIS = ID_ANALISIS
                        pe.ID = INGRESA_OPINION(pe)
                        CREAR_SENTIMIENTO_OPINION(pe)
                        'pe.ID_SENTIMIENTO_ANALISIS = OBTENER_SENTIMIENTO(pe, analisis, LISTA_NEGACION, LISTA_MONOSILABOS, LISTA_PALABRAS_SENTIMIENTO)

                        pe.ID_SENTIMIENTO_ANALISIS = OBTENER_SENTIMIENTO_V2(pe, analisis, LISTA_NEGACION, LISTA_PALABRAS_SENTIMIENTO)


                        cont = cont + 1
                    Next


                    Dim RESPUESTA As String = DT_Analisis.PromedioAnalisis(ID_ANALISIS, "")
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


    Private Function OBTENER_SENTIMIENTO(ByVal OPINION As CL_Opinion, ByVal ANALISIS As CL_Analisis, ByVal LISTA_NEGACION As List(Of String), ByVal LISTA_MONOSILABOS As List(Of String), ByVal LISTA_PALABRAS_SENTIMIENTO As List(Of String)) As Integer


        Dim puntuaciones As New List(Of String)
        puntuaciones.Add(",")
        puntuaciones.Add(".")
        puntuaciones.Add(";")


        Dim TEMPORAL As String() = OPINION.PREPROCESADO.Split(New Char() {" "c})
        Dim LISTA_PALABRAS As List(Of String) = New List(Of String)(TEMPORAL)
        Dim FRASE_NEGACION As List(Of String) = New List(Of String)
        Dim ALERTA_NEGADOR As Integer = 0
        Dim DESPLAZADO As Integer = 0
        Dim POSICION_NEGADOR As Integer
        Dim STEMIZADO As String

        For I As Integer = 0 To LISTA_PALABRAS.Count - 1
            Dim encontrado = 0
            If LISTA_NEGACION.Contains(LISTA_PALABRAS.Item(I)) And ALERTA_NEGADOR = 0 Then
                ALERTA_NEGADOR = 1
                POSICION_NEGADOR = I

                DT_Opinion.AUMENTAR_CONTADOR_NEGADOR(OPINION, Session("Usuario"))


                For K As Integer = I + 1 To I + ANALISIS.DESPLAZAMIENTO


                    If K < LISTA_PALABRAS.Count - 1 Then
                        If LISTA_PALABRAS_SENTIMIENTO.Contains(LISTA_PALABRAS.Item(K)) Then
                            Dim palabra_encontrada As CL_Palabra = DT_Palabra.SelectPalabraPorTexto(LISTA_PALABRAS.Item(K)).Item(0)
                            encontrado = 1
                            DESPLAZADO = K
                            ALERTA_NEGADOR = 0
                            GoTo encontrado_con_negador

                        Else
                            STEMIZADO = DT_Palabra.SelectStemizado(LISTA_PALABRAS.Item(K))
                            If STEMIZADO <> "" Then
                                Dim COINCIDENCIA As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorRaiz(STEMIZADO)
                                If COINCIDENCIA.Count > 0 Then
                                    DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(OPINION, COINCIDENCIA.Item(0), Session("Usuario"))
                                    DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(OPINION, COINCIDENCIA.Item(0), Session("Usuario"))
                                    encontrado = 1
                                    ALERTA_NEGADOR = 0
                                    DESPLAZADO = K
                                    GoTo encontrado_con_negador
                                End If

                            End If

                        End If


                    End If


                Next

encontrado_con_negador:

                If encontrado = 0 Then
                    Dim palabra1 As String = LISTA_PALABRAS.Item(I).Replace(",", "")
                    palabra1 = palabra1.Replace(".", "")
                    If DT_Palabra.ExistePalabra(palabra1, Session("Usuario")) = 0 Then
                        DT_Palabra.InsertPalabraNoEncontrada(palabra1, OPINION, Session("Usuario"))

                    End If
                Else

                    I = DESPLAZADO

                End If



            Else



                If LISTA_PALABRAS_SENTIMIENTO.Contains(LISTA_PALABRAS.Item(I)) Then
                    Dim palabra_encontrada As CL_Palabra = DT_Palabra.SelectPalabraPorTexto(LISTA_PALABRAS.Item(I)).Item(0)
                Else
                    STEMIZADO = DT_Palabra.SelectStemizado(LISTA_PALABRAS.Item(I))
                    If STEMIZADO <> "" Then
                        Dim COINCIDENCIA As List(Of CL_Palabra) = DT_Palabra.SelectPalabraPorRaiz(STEMIZADO)
                        If COINCIDENCIA.Count > 0 Then
                            DT_Opinion.AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(OPINION, COINCIDENCIA.Item(0), Session("Usuario"))
                            DT_Opinion.AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(OPINION, COINCIDENCIA.Item(0), Session("Usuario"))
                            encontrado = 1
                        Else
                            Dim palabra1 As String = LISTA_PALABRAS.Item(I).Replace(",", "")
                            palabra1 = palabra1.Replace(".", "")
                            If DT_Palabra.ExistePalabra(palabra1, Session("Usuario")) = 0 Then
                                DT_Palabra.InsertPalabraNoEncontrada(palabra1, OPINION, Session("Usuario"))
                            End If
                        End If
                    Else
                        Dim palabra1 As String = LISTA_PALABRAS.Item(I).Replace(",", "")
                        palabra1 = palabra1.Replace(".", "")
                        If DT_Palabra.ExistePalabra(palabra1, Session("Usuario")) = 0 Then
                            DT_Palabra.InsertPalabraNoEncontrada(palabra1, OPINION, Session("Usuario"))
                        End If
                    End If
                End If

            End If

        Next

    End Function

    Private Function INGRESA_OPINION(ByVal OPINION As CL_Opinion) As Integer
        Dim RES As String = DT_Opinion.InsertOpinion(OPINION, Session("Usuario"))
        Return Integer.Parse(RES)
    End Function


    Private Function INGRESA_ANALISIS(ByVal ANALISIS As CL_Analisis) As Integer
        Dim RES As String = DT_Analisis.InsertAnalisis(ANALISIS, Session("Usuario"))
        Return Integer.Parse(RES)
    End Function


    Private Sub CREAR_SENTIMIENTO_OPINION(ByVal OPINION As CL_Opinion)

        Dim LISTA_SENTIMIENTOS As List(Of CL_Sentimiento) = DT_Sentimiento.SelectSentimiento()

        For Each ITEM As CL_Sentimiento In LISTA_SENTIMIENTOS
            DT_Opinion.InsertOpinionSentimiento(OPINION, ITEM, Session("Usuario"))
        Next

    End Sub

    Private Function PREPROCESAMIENTO(ByVal OPINION As CL_Opinion) As CL_Opinion
        OPINION.PREPROCESADO = OPINION.TEXTO.ToLower
        OPINION.PREPROCESADO = ELIMINAR_STOPWORD(OPINION.PREPROCESADO)
        OPINION.PREPROCESADO = ELIMINAR_MONOSILAVO(OPINION.PREPROCESADO)
        OPINION.PREPROCESADO = REMOVER_TILDES(OPINION.PREPROCESADO)
        OPINION.PREPROCESADO = REMOVER_CARACTERES_ESPECIALES(OPINION.PREPROCESADO)
        Return OPINION
    End Function


    Private Function REMOVER_TILDES(ByVal TEXTO As String) As String

        Dim tempBytes As Byte()
        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(TEXTO)
        Return System.Text.Encoding.UTF8.GetString(tempBytes)
    End Function


    Private Function REMOVER_CARACTERES_ESPECIALES(ByVal TEXTO As String)
        TEXTO = TEXTO.Replace("""", "")
        Return TEXTO
    End Function



    Private Function ELIMINAR_STOPWORD(ByVal TEXTO As String) As String
        Dim RES As String = ""

        Dim LISTA_STOPWORD As List(Of CL_Stopword) = DT_Stopword.SelectStopword()

        Dim PALABRAS As String() = TEXTO.Split(New Char() {" "c})
        Dim LISTA_PALABRAS As List(Of String) = New List(Of String)(PALABRAS)




        For Each item As CL_Stopword In LISTA_STOPWORD
            If LISTA_PALABRAS.Contains(item.TEXTO.ToLower) Then
                LISTA_PALABRAS.Remove(item.TEXTO.ToLower)
            End If
        Next


        For Each item As String In LISTA_PALABRAS
            RES = RES + item + " "
        Next
        Return RES.Trim
    End Function


    Private Function ELIMINAR_MONOSILAVO(ByVal TEXTO As String) As String
        Dim RES As String = ""
        Dim TEMPORAL As String()

        Dim LISTA_NEGACION As List(Of String) = DT_Negacion.SelectNegacionString()
        Dim LISTA_MONOSILABOS As List(Of String) = DT_Monosilabo.SelectMonosilaboString()
        Dim LISTA_SIMBOLOS As New List(Of String)



        TEMPORAL = TEXTO.Split(New Char() {" "c})
        Dim LISTA_PALABRAS As List(Of String) = New List(Of String)(TEMPORAL)


        Dim puntuaciones As New List(Of String)
        puntuaciones.Add(",")
        puntuaciones.Add(".")
        puntuaciones.Add(";")
        'puntuaciones.Add(":")


        For Each PALABRA As String In LISTA_PALABRAS.ToList
            If separador.CONTAR_SILABEAR(PALABRA) = 1 And Not LISTA_NEGACION.Contains(PALABRA) And Not puntuaciones.Contains(PALABRA) And Not LISTA_MONOSILABOS.Contains(PALABRA) Then
                LISTA_PALABRAS.Remove(PALABRA)
            End If
        Next

        For Each item As String In LISTA_PALABRAS
            RES = RES + item + " "
        Next
        Return RES.Trim
    End Function


    Public Sub BtDescargar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim x As String

        'My.Computer.Network.DownloadFile("~/Ejemplo_Planilla/plantilla.xlsx", "plantilla.xlsx")


        Try
            Dim path As String = "plantilla_opinion.xlsx"
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            Else
                ScriptManager.RegisterStartupScript(Page, Me.GetType, "", "alert(""Archivo no existe en " & path & " ."");", True)
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Shared Function match(ByVal stemmerName As String, ByVal language As String) As Boolean
        Dim expectedName As String = language.Replace("_", "") & "Stemmer"
        Return stemmerName.StartsWith(expectedName, StringComparison.CurrentCultureIgnoreCase)
    End Function

End Class
