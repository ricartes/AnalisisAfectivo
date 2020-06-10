Imports Microsoft.VisualBasic
Imports System.Data

Public Class DT_Monosilabo
    Public Shared Function SelectMonosilabo(ByVal codigo_sentimiento As Integer, Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Monosilabo)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("CODIGO_SENTIMIENTO", codigo_sentimiento)
        End With
        Try
            vTabla = vCon.ExecSP("MONOSILABO_SELECT", vParam)
            SelectMonosilabo = New List(Of CL_Monosilabo)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Monosilabo
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("MONOSILABO")) Then vAtribu.TEXTO = vFila("MONOSILABO")
                If Not IsDBNull(vFila("INTENSIDAD")) Then vAtribu.INTENSIDAD = vFila("INTENSIDAD")

                If Not IsDBNull(vFila("ID_SENTIMIENTO")) Then vAtribu.CODIGO_SENTIMIENTO = vFila("ID_SENTIMIENTO")
                If Not IsDBNull(vFila("NOMBRE_SENTIMIENTO")) Then vAtribu.NOMBRE_SENTIMIENTO = vFila("NOMBRE_SENTIMIENTO")
                SelectMonosilabo.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectMonosilaboString(Optional ByVal vCon As Conexion = Nothing) As List(Of String)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam
            .Add("CODIGO_SENTIMIENTO", 0)
        End With
        Try
            vTabla = vCon.ExecSP("MONOSILABO_SELECT", vParam)
            SelectMonosilaboString = New List(Of String)
            For Each vFila As DataRow In vTabla.Rows
                If Not IsDBNull(vFila("MONOSILABO")) Then SelectMonosilaboString.Add(vFila("MONOSILABO").ToString.Trim.ToLower)

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function InsertMonosilabo(ByVal palabra As CL_Monosilabo, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("TEXTO", palabra.TEXTO)
                .Add("COD_SENTIMIENTO", palabra.CODIGO_SENTIMIENTO)
                .Add("INTENSIDAD", palabra.INTENSIDAD)

            End With
            Return vCon.GenericoProcedure_Res3("MONOSILABO_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function MonosilaboDelete(ByVal palabra As CL_Monosilabo, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("COD_PALABRA", palabra.CODIGO)

            End With
            Return vCon.GenericoProcedure("MONOSILABO_DELETE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function

End Class
