Imports Microsoft.VisualBasic
Imports System.Data

Public Class DT_Stopword
    Public Shared Function SelectStopword(Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Stopword)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam

        End With
        Try
            vTabla = vCon.ExecSP("STOPWORD_SELECT", vParam)
            SelectStopword = New List(Of CL_Stopword)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Stopword
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("TEXTO")) Then vAtribu.TEXTO = vFila("TEXTO")
                SelectStopword.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function InsertStopword(ByVal stopword As CL_Stopword, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("TEXTO", stopword.TEXTO)


            End With
            Return vCon.GenericoProcedure("STOPWORD_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function UpdateStopword(ByVal stopword As CL_Stopword, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("CODIGO", stopword.CODIGO)
                .Add("TEXTO", stopword.TEXTO)


            End With
            Return vCon.GenericoProcedure("STOPWORD_UPDATE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function DeleteStopword(ByVal stopword As CL_Stopword, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("CODIGO", stopword.CODIGO)

            End With
            Return vCon.GenericoProcedure("STOPWORD_DELETE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function DeleteStopwordALL(ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam


            End With
            Return vCon.GenericoProcedure("STOPWORD_DELETE_ALL", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


End Class
