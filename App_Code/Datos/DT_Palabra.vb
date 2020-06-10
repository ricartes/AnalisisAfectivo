Imports Microsoft.VisualBasic
Imports System.Data

Public Class DT_Palabra



    Public Shared Function SelectPalabra(ByVal codigo_sentimiento As Integer, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Palabra)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("CODIGO_SENTIMIENTO", codigo_sentimiento)
        End With
        Try
            vTabla = vCon.ExecSP("PALABRA_SELECT", vParam)
            SelectPalabra = New List(Of CL_Palabra)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Palabra
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("TEXTO")) Then vAtribu.TEXTO = vFila("TEXTO")
                If Not IsDBNull(vFila("INTENSIDAD")) Then vAtribu.INTENSIDAD = vFila("INTENSIDAD")

                If Not IsDBNull(vFila("ID_SENTIMIENTO")) Then vAtribu.CODIGO_SENTIMIENTO = vFila("ID_SENTIMIENTO")
                If Not IsDBNull(vFila("NOMBRE_SENTIMIENTO")) Then vAtribu.NOMBRE_SENTIMIENTO = vFila("NOMBRE_SENTIMIENTO")
                SelectPalabra.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectPalabraTODAS(Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Palabra)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam

        End With
        Try
            vTabla = vCon.ExecSP("PALABRA_SELECT_ALL", vParam)
            SelectPalabraTODAS = New List(Of CL_Palabra)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Palabra
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("TEXTO")) Then vAtribu.TEXTO = vFila("TEXTO")
                If Not IsDBNull(vFila("INTENSIDAD")) Then vAtribu.INTENSIDAD = vFila("INTENSIDAD")

                If Not IsDBNull(vFila("ID_SENTIMIENTO")) Then vAtribu.CODIGO_SENTIMIENTO = vFila("ID_SENTIMIENTO")
                If Not IsDBNull(vFila("NOMBRE_SENTIMIENTO")) Then vAtribu.NOMBRE_SENTIMIENTO = vFila("NOMBRE_SENTIMIENTO")
                SelectPalabraTODAS.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function

    Public Shared Function EliminaPlabraTodas(ByVal codigo_sentimiento As Integer, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam

                .Add("CODIGO_SENTIMIENTO", codigo_sentimiento)
            End With
            Return vCon.GenericoProcedure("PALABRA_DELETE_ALL", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function

    Public Shared Function InsertPalabra(ByVal palabra As CL_Palabra, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("TEXTO", palabra.TEXTO)
                .Add("COD_SENTIMIENTO", palabra.CODIGO_SENTIMIENTO)
                .Add("INTENSIDAD", palabra.INTENSIDAD)

            End With
            Return vCon.GenericoProcedure_Res3("PALABRA_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function InsertPalabraNoEncontrada(ByVal palabra As String, ByVal opinion As CL_Opinion, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("TEXTO", palabra)
                .Add("ID_OPINION", opinion.ID)


            End With
            Return vCon.GenericoProcedure("PALABRA_NO_ENCOTRADA_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



    Public Shared Function ExistePalabra(ByVal palabra As String, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As Integer
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("TEXTO", palabra)

            End With
            Return Integer.Parse(vCon.GenericoProcedure_Res3("PALABRA_NO_ENCONTRADA_EXISTE", vParam))
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function




    Public Shared Function PalabraUpdate(ByVal palabra As CL_Palabra, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("COD_PALABRA", palabra.CODIGO)
                .Add("INTENSIDAD", palabra.INTENSIDAD)
                .Add("COD_SENTIMIENTO", palabra.CODIGO_SENTIMIENTO)

            End With
            Return vCon.GenericoProcedure("PALABRA_UPDATE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



    Public Shared Function PalabraDelete(ByVal palabra As CL_Palabra, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("COD_PALABRA", palabra.CODIGO)

            End With
            Return vCon.GenericoProcedure("PALABRA_DELETE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectPlabraString(Optional ByVal vCon As Conexion = Nothing) As List(Of String)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam

        End With
        Try
            vTabla = vCon.ExecSP("PALABRA_SELECT_ALL", vParam)
            SelectPlabraString = New List(Of String)
            For Each vFila As DataRow In vTabla.Rows
                If Not IsDBNull(vFila("TEXTO")) Then SelectPlabraString.Add(vFila("TEXTO").ToString.Trim.ToLower)

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectStemizado(ByVal texto As String, Optional ByVal vCon As Conexion = Nothing) As String
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable

        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("TEXTO", texto)
        End With
        Try
            vTabla = vCon.ExecSP("STEMIZADO_SELECT", vParam)
            For Each vFila As DataRow In vTabla.Rows
                If Not IsDBNull(vFila("RAIZ")) Then SelectStemizado = vFila("RAIZ")
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



    Public Shared Function SelectPalabraPorTexto(ByVal texto As String, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Palabra)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("TEXTO", texto)
        End With
        Try
            vTabla = vCon.ExecSP("PALABRA_SELECT_POR_TEXTO", vParam)
            SelectPalabraPorTexto = New List(Of CL_Palabra)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Palabra
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("TEXTO")) Then vAtribu.TEXTO = vFila("TEXTO")
                If Not IsDBNull(vFila("INTENSIDAD")) Then vAtribu.INTENSIDAD = vFila("INTENSIDAD")

                If Not IsDBNull(vFila("ID_SENTIMIENTO")) Then vAtribu.CODIGO_SENTIMIENTO = vFila("ID_SENTIMIENTO")
                If Not IsDBNull(vFila("NOMBRE_SENTIMIENTO")) Then vAtribu.NOMBRE_SENTIMIENTO = vFila("NOMBRE_SENTIMIENTO")
                SelectPalabraPorTexto.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



    Public Shared Function SelectPalabraPorRaiz(ByVal texto As String, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Palabra)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        Dim id_actual As Integer = -1
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("TEXTO", texto)
        End With
        Try
            vTabla = vCon.ExecSP("BUSQUEDA_PALABRA_RAIZ", vParam)
            SelectPalabraPorRaiz = New List(Of CL_Palabra)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Palabra
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("TEXTO")) Then vAtribu.TEXTO = vFila("TEXTO")
                If Not IsDBNull(vFila("INTENSIDAD")) Then vAtribu.INTENSIDAD = vFila("INTENSIDAD")

                If Not IsDBNull(vFila("ID_SENTIMIENTO")) Then vAtribu.CODIGO_SENTIMIENTO = vFila("ID_SENTIMIENTO")
                If Not IsDBNull(vFila("NOMBRE_SENTIMIENTO")) Then vAtribu.NOMBRE_SENTIMIENTO = vFila("NOMBRE_SENTIMIENTO")

                If id_actual <> vAtribu.CODIGO_SENTIMIENTO Then
                    SelectPalabraPorRaiz.Add(vAtribu)
                    id_actual = vAtribu.CODIGO_SENTIMIENTO
                End If


            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectPalabraNoEncontrada(Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Palabra)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion

        Try
            vTabla = vCon.ExecSP("PALABRA_NO_ENCONTRADA_SELECT", vParam)
            SelectPalabraNoEncontrada = New List(Of CL_Palabra)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Palabra
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("TEXTO")) Then vAtribu.TEXTO = vFila("TEXTO")
                SelectPalabraNoEncontrada.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function PalabraNoEncontradaDelete(ByVal palabra As CL_Palabra, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID", palabra.CODIGO)

            End With
            Return vCon.GenericoProcedure("PALABRA_NO_ENCONTRADA_DELETE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function PalabraRaizLexica(ByVal palabra As CL_Palabra, ByVal raiz As String, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID_PALABRA", palabra.CODIGO)
                .Add("RAIZ_LEXICA", raiz)

            End With
            Return vCon.GenericoProcedure("PALABRA_RAIZ_LEXICA", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



End Class




