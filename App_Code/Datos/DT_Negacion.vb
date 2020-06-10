Imports Microsoft.VisualBasic
Imports System.Data

Public Class DT_Negacion
    Public Shared Function SelectNegacion(Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Negacion)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam

        End With
        Try
            vTabla = vCon.ExecSP("NEGACION_SELECT", vParam)
            SelectNegacion = New List(Of CL_Negacion)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Negacion
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("TEXTO")) Then vAtribu.TEXTO = vFila("TEXTO")
                SelectNegacion.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectNegacionString(Optional ByVal vCon As Conexion = Nothing) As List(Of String)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam

        End With
        Try
            vTabla = vCon.ExecSP("NEGACION_SELECT", vParam)
            SelectNegacionString = New List(Of String)
            For Each vFila As DataRow In vTabla.Rows
                If Not IsDBNull(vFila("TEXTO")) Then SelectNegacionString.Add(vFila("TEXTO").ToString.Trim.ToLower)

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function

    Public Shared Function InsertNegacion(ByVal negacion As CL_Negacion, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("TEXTO", negacion.TEXTO)


            End With
            Return vCon.GenericoProcedure("NEGACION_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function UpdateNegacion(ByVal negacion As CL_Negacion, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("CODIGO", negacion.CODIGO)
                .Add("TEXTO", negacion.TEXTO)


            End With
            Return vCon.GenericoProcedure("NEGACION_UPDATE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function DeleteNegacion(ByVal negacion As CL_Negacion, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("CODIGO", negacion.CODIGO)

            End With
            Return vCon.GenericoProcedure("NEGACION_DELETE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


End Class
