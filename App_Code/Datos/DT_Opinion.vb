Imports Microsoft.VisualBasic
Imports System.Data

Public Class DT_Opinion
    Public Shared Function InsertOpinion(ByVal opinion As CL_Opinion, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID_ANALISIS", opinion.ID_ANALISIS)
                .Add("TEXTO", opinion.TEXTO)
                .Add("ID_SENTIMIENTO", opinion.SENTIMIENTO_USUARIO)
                .Add("PREPROCESADO", opinion.PREPROCESADO)

            End With
            Return vCon.GenericoProcedure_Res3("OPINION_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectOpinionAnalisis(ByVal ID As Integer, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Opinion)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("ID_ANALISIS", ID)
        End With
        Try
            vTabla = vCon.ExecSP("OPINION_ANALISIS_SELECT", vParam)
            SelectOpinionAnalisis = New List(Of CL_Opinion)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Opinion
                If Not IsDBNull(vFila("ID_OPINION")) Then vAtribu.ID = vFila("ID_OPINION")
                If Not IsDBNull(vFila("ID_ANALISIS")) Then vAtribu.ID_ANALISIS = vFila("ID_ANALISIS")
                If Not IsDBNull(vFila("TEXTO")) Then vAtribu.TEXTO = vFila("TEXTO")
                If Not IsDBNull(vFila("TOTAL_PALABRAS")) Then vAtribu.TOTAL_PALABRAS = vFila("TOTAL_PALABRAS")
                If Not IsDBNull(vFila("TOTAL_NEGACIONES")) Then vAtribu.TOTAL_NEGACIONES = vFila("TOTAL_NEGACIONES")
                If Not IsDBNull(vFila("ID_SENTIMIENTO")) Then vAtribu.SENTIMIENTO_USUARIO = vFila("ID_SENTIMIENTO")
                If Not IsDBNull(vFila("INTENSIDAD_ANALISIS")) Then vAtribu.INTENSIDAD_ANALISIS = vFila("INTENSIDAD_ANALISIS")



               

                If vAtribu.INTENSIDAD_ANALISIS <> 0 Then
                    Dim sent As List(Of CL_Sentimiento) = DT_Sentimiento.SentimientoMayor(vAtribu)

                    If sent.Count > 0 Then

                        If sent.Count = 1 Then
                            vAtribu.NOMBRE_SENTIMIENTO_ANALISIS = sent.Item(0).NOMBRE
                        Else
                            Dim count As Integer = 0
                            For Each item As CL_Sentimiento In sent


                                If count < sent.Count - 1 Then
                                    vAtribu.NOMBRE_SENTIMIENTO_ANALISIS = vAtribu.NOMBRE_SENTIMIENTO_ANALISIS + item.NOMBRE + ", "
                                Else
                                    vAtribu.NOMBRE_SENTIMIENTO_ANALISIS = vAtribu.NOMBRE_SENTIMIENTO_ANALISIS + item.NOMBRE
                                End If


                                count = count + 1
                            Next

                            'vAtribu.NOMBRE_SENTIMIENTO_ANALISIS = vAtribu.NOMBRE_SENTIMIENTO_ANALISIS.Trim().Substring(0, vAtribu.NOMBRE_SENTIMIENTO_ANALISIS.Length - 1)
                            'vAtribu.NOMBRE_SENTIMIENTO_ANALISIS = vAtribu.NOMBRE_SENTIMIENTO_ANALISIS.Remove(vAtribu.NOMBRE_SENTIMIENTO_ANALISIS.Length - 1)
                        End If
                      
                    End If

                  

                    
                End If
                If Not IsDBNull(vFila("PREPROCESADO")) Then vAtribu.PREPROCESADO = vFila("PREPROCESADO")
                If Not IsDBNull(vFila("SENTIMIENTO_USUARIO")) Then vAtribu.NOMBRE_SENTIMIENTO_USUARIO = vFila("SENTIMIENTO_USUARIO")
                SelectOpinionAnalisis.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



    Public Shared Function SelectOpinionReport(ByVal ID As Integer, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Opinion)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("ID_ANALISIS", ID)
        End With
        Try
            vTabla = vCon.ExecSP("OPINION_ANALISIS_REPORT", vParam)
            SelectOpinionReport = New List(Of CL_Opinion)
            Dim actual = "-1"
            Dim actual1 = "-1"
            Dim actual2 = "-1"
            Dim actual4 = "-99"
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Opinion
                If Not IsDBNull(vFila("ID_OPINION")) Then vAtribu.ID = vFila("ID_OPINION")
                If Not IsDBNull(vFila("ID_ANALISIS")) Then vAtribu.ID_ANALISIS = vFila("ID_ANALISIS")


                If Not IsDBNull(vFila("TEXTO")) Then
                    If vFila("TEXTO").ToString <> actual Then
                        vAtribu.TEXTO = vFila("TEXTO")
                        If Not IsDBNull(vFila("PREPROCESADO")) Then vAtribu.PREPROCESADO = vFila("PREPROCESADO")
                        If Not IsDBNull(vFila("INTENSIDAD_PREDOMINANTE")) Then vAtribu.INTENSIDAD_ANALISIS = vFila("INTENSIDAD_PREDOMINANTE")
                        If Not IsDBNull(vFila("NOMBRE_SENTIMIENTO_EXPERTO")) Then vAtribu.NOMBRE_SENTIMIENTO_USUARIO = vFila("NOMBRE_SENTIMIENTO_EXPERTO")


                        If vAtribu.INTENSIDAD_ANALISIS > 0 Then
                            Dim sent As List(Of CL_Sentimiento) = DT_Sentimiento.SentimientoMayor(vAtribu)

                            If sent.Count > 0 Then

                                If sent.Count = 1 Then
                                    vAtribu.NOMBRE_SENTIMIENTO_PREDOMINANTE = sent.Item(0).NOMBRE
                                Else
                                    Dim count As Integer = 0
                                    For Each item As CL_Sentimiento In sent


                                        If count < sent.Count - 1 Then
                                            vAtribu.NOMBRE_SENTIMIENTO_PREDOMINANTE = vAtribu.NOMBRE_SENTIMIENTO_PREDOMINANTE + item.NOMBRE + ", "
                                        Else
                                            vAtribu.NOMBRE_SENTIMIENTO_PREDOMINANTE = vAtribu.NOMBRE_SENTIMIENTO_PREDOMINANTE + item.NOMBRE
                                        End If


                                        count = count + 1
                                    Next

                                    'Atribu.NOMBRE_SENTIMIENTO_ANALISIS = vAtribu.NOMBRE_SENTIMIENTO_ANALISIS.Trim().Substring(0, vAtribu.NOMBRE_SENTIMIENTO_ANALISIS.Length - 1)
                                    'vAtribu.NOMBRE_SENTIMIENTO_ANALISIS = vAtribu.NOMBRE_SENTIMIENTO_ANALISIS.Remove(vAtribu.NOMBRE_SENTIMIENTO_ANALISIS.Length - 1)
                                End If

                            End If
                        End If



                        actual = vAtribu.TEXTO
                        
                    Else
                        vAtribu.TEXTO = ""
                        vAtribu.PREPROCESADO = ""
                        vAtribu.INTENSIDAD_ANALISIS = Nothing
                        vAtribu.NOMBRE_SENTIMIENTO_USUARIO = ""

                    End If
                End If










                'If Not IsDBNull(vFila("PREPROCESADO")) Then vAtribu.PREPROCESADO = vFila("PREPROCESADO")
                If Not IsDBNull(vFila("ID_SENTIMIENTO_EXPERTO")) Then vAtribu.SENTIMIENTO_USUARIO = vFila("ID_SENTIMIENTO_EXPERTO")

                If Not IsDBNull(vFila("ID_SENTIMIENTO_ANALISIS")) Then vAtribu.ID_SENTIMIENTO_ANALISIS = vFila("ID_SENTIMIENTO_ANALISIS")
                If Not IsDBNull(vFila("nombre_sentimiento_analisis")) Then vAtribu.NOMBRE_SENTIMIENTO_ANALISIS = vFila("nombre_sentimiento_analisis")
                If Not IsDBNull(vFila("total_palabras_sentimiento")) Then vAtribu.TOTAL_PALABRAS_SENTIMIENTO = vFila("total_palabras_sentimiento")
                If Not IsDBNull(vFila("intensidad_sentimiento")) Then vAtribu.INTENSIDAD_OPINION_ANALISIS = vFila("intensidad_sentimiento")















                'If vAtribu.INTENSIDAD_ANALISIS <> 0 Then
                '    Dim sent As CL_Sentimiento = DT_Sentimiento.SentimientoMayor(vAtribu)
                '    vAtribu.ID_SENTIMIENTO_ANALISIS = sent.CODIGO
                '    vAtribu.NOMBRE_SENTIMIENTO_ANALISIS = sent.NOMBRE
                'End If

                SelectOpinionReport.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function

    Public Shared Function InsertOpinionSentimiento(ByVal opinion As CL_Opinion, ByVal sentimiento As CL_Sentimiento, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID_ANALISIS", opinion.ID_ANALISIS)
                .Add("ID_OPINION", opinion.ID)
                .Add("ID_SENTIMIENTO", sentimiento.CODIGO)

            End With
            Return vCon.GenericoProcedure("OPINION_SENTIMIENTO_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION(ByVal opinion As CL_Opinion, ByVal palabra As CL_Palabra, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID_ANALISIS", opinion.ID_ANALISIS)
                .Add("ID_OPINION", opinion.ID)
                .Add("ID_SENTIMIENTO", palabra.CODIGO_SENTIMIENTO)
                .Add("INTENSIDAD_ENCONTRADA", palabra.INTENSIDAD)

            End With
            Return vCon.GenericoProcedure("AUMENTAR_INTENSIDAD_SENTIMIENTO_OPINION", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function AUMENTAR_CONTADOR_SENTIMIENTO_OPINION(ByVal opinion As CL_Opinion, ByVal palabra As CL_Palabra, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID_ANALISIS", opinion.ID_ANALISIS)
                .Add("ID_OPINION", opinion.ID)
                .Add("ID_SENTIMIENTO", palabra.CODIGO_SENTIMIENTO)

            End With
            Return vCon.GenericoProcedure("AUMENTAR_REGISTROS_SENTIMIENTO_OPINION", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



    Public Shared Function AUMENTAR_CONTADOR_OPINION(ByVal opinion As CL_Opinion, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID_ANALISIS", opinion.ID_ANALISIS)
                .Add("ID_OPINION", opinion.ID)


            End With
            Return vCon.GenericoProcedure("AUMENTAR_REGISTROS_OPINION", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



    Public Shared Function AUMENTAR_CONTADOR_NEGADOR(ByVal opinion As CL_Opinion, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID_ANALISIS", opinion.ID_ANALISIS)
                .Add("ID_OPINION", opinion.ID)


            End With
            Return vCon.GenericoProcedure("AUMENTAR_REGISTROS_NEGACION_OPINION", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectDetalleOpinion(ByVal ID As Integer, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_DetalleOpinion)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("ID_OPINION", ID)
        End With
        Try
            vTabla = vCon.ExecSP("DETALLE_OPINION_SELECT", vParam)
            SelectDetalleOpinion = New List(Of CL_DetalleOpinion)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_DetalleOpinion
                If Not IsDBNull(vFila("ID_ANALISIS")) Then vAtribu.ID_ANALISIS = vFila("ID_ANALISIS")
                If Not IsDBNull(vFila("ID_OPINION")) Then vAtribu.ID_OPINION = vFila("ID_OPINION")
                If Not IsDBNull(vFila("ID_SENTIMIENTO")) Then vAtribu.ID_SENTIMIENTO = vFila("ID_SENTIMIENTO")
                If Not IsDBNull(vFila("intensidad_sentimiento")) Then vAtribu.INTENSIDAD_SENTIMIENTO = vFila("intensidad_sentimiento")
                If Not IsDBNull(vFila("REGISTROS_ENCONTRADOS")) Then vAtribu.REGISTROS_ENCONTRADOS = vFila("REGISTROS_ENCONTRADOS")
                If Not IsDBNull(vFila("NOMBRE_SENTIMIENTO")) Then vAtribu.NOMBRE_SENTIMIENTO = vFila("NOMBRE_SENTIMIENTO")
              
                SelectDetalleOpinion.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function INGRESA_PALABRA_SENTIMIENTO(ByVal opinion As CL_Opinion, ByVal analisis As CL_Analisis, ByVal palabra As CL_Palabra, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID_ANALISIS", opinion.ID_ANALISIS)
                .Add("ID_OPINION", opinion.ID)
                .Add("ID_SENTIMIENTO", palabra.CODIGO_SENTIMIENTO)
                .Add("ID_PALABRA", palabra.CODIGO)
                .Add("TEXTO", palabra.TEXTO)
                .Add("INTENSIDAD", palabra.INTENSIDAD)


            End With
            Return vCon.GenericoProcedure("PALABRA_SENTIMIENTO_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectPalabraSentimiento(ByVal ID_OPINION As Integer, ID_ANALISIS As Integer, ID_SENTIMIENTO As Integer, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_PalabraSentimiento)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("ID_ANALISIS", ID_ANALISIS)
            .Add("ID_OPINION", ID_OPINION)
            .Add("ID_SENTIMIENTO", ID_SENTIMIENTO)
        End With
        Try
            vTabla = vCon.ExecSP("PALABRA_SENTIMIENTO_SELECT", vParam)
            SelectPalabraSentimiento = New List(Of CL_PalabraSentimiento)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_PalabraSentimiento
                If Not IsDBNull(vFila("texto")) Then vAtribu.TEXTO = vFila("texto")
                If Not IsDBNull(vFila("raiz_lexica")) Then vAtribu.RAIZ_LEXICA = vFila("raiz_lexica")
                If Not IsDBNull(vFila("intensidad")) Then vAtribu.INTENSIDAD = vFila("intensidad")
                If Not IsDBNull(vFila("nombre_sentimiento")) Then vAtribu.NOMBRE_SENTIMIENTO = vFila("nombre_sentimiento")


                SelectPalabraSentimiento.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


End Class
