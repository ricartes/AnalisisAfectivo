Imports Microsoft.VisualBasic
Imports System.Data

Public Class DT_Sentimiento
    Public Shared Function SelectSentimiento(Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Sentimiento)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam

        End With
        Try
            vTabla = vCon.ExecSP("SENTIMIENTO_SELECT", vParam)
            SelectSentimiento = New List(Of CL_Sentimiento)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Sentimiento
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("NOMBRE")) Then vAtribu.NOMBRE = vFila("NOMBRE")      
                SelectSentimiento.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function

    Public Shared Function InsertSentimiento(ByVal sentimiento As CL_Sentimiento, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("NOMBRE", sentimiento.NOMBRE)


            End With
            Return vCon.GenericoProcedure("SENTIMIENTO_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function UpdateSentimiento(ByVal sentimiento As CL_Sentimiento, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("CODIGO_SENTIMIENTO", sentimiento.CODIGO)
                .Add("NOMBRE", sentimiento.NOMBRE)


            End With
            Return vCon.GenericoProcedure("SENTIMIENTO_UPDATE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



    Public Shared Function SentimientoPorTexto(ByVal texto As String, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Sentimiento)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("TEXTO", texto)
        End With
        Try
            vTabla = vCon.ExecSP("SENTIMIENTO_POR_TEXTO", vParam)
            SentimientoPorTexto = New List(Of CL_Sentimiento)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Sentimiento
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("NOMBRE")) Then vAtribu.NOMBRE = vFila("NOMBRE")
                SentimientoPorTexto.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function MinimaIntensidad(ByVal PALABRA As CL_Palabra, Optional ByVal vCon As Conexion = Nothing) As Integer
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        Dim res As Integer
        If vCon Is Nothing Then vCon = New Conexion

        With vParam
            .Add("ID_SENTIMIENTO ", PALABRA.CODIGO_SENTIMIENTO)
        End With

        Try
            vTabla = vCon.ExecSP("MINIMA_INTENSIDAD_POR_SENTIMIENTO", vParam)

            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Parametro
                If Not IsDBNull(vFila("intensidad")) Then res = vFila("intensidad")

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try

        Return res
    End Function


    Public Shared Function SentimientoMayor(ByVal opinion As CL_Opinion, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Sentimiento)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        Dim res As Integer
        If vCon Is Nothing Then vCon = New Conexion

        With vParam
            .Add("ID_OPINION ", opinion.ID)
            .Add("ID_ANALISIS ", opinion.ID_ANALISIS)
        End With

        Try
            vTabla = vCon.ExecSP("SENTIMIENTO_MAXIMO2", vParam)
            SentimientoMayor = New List(Of CL_Sentimiento)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Sentimiento
                If Not IsDBNull(vFila("id_sentimiento")) Then vAtribu.CODIGO = vFila("id_sentimiento")
                If Not IsDBNull(vFila("nombre")) Then vAtribu.NOMBRE = vFila("nombre")
                SentimientoMayor.Add(vAtribu)

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try

    End Function

End Class
