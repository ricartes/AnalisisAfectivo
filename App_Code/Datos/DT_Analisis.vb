Imports Microsoft.VisualBasic
Imports System.Data

Public Class DT_Analisis
    Public Shared Function InsertAnalisis(ByVal analisis As CL_Analisis, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("NOMBRE", analisis.NOMBRE)
                .Add("FECHA", analisis.FECHA)
                .Add("CANTIDAD_DESPLAZAMIENTO", analisis.DESPLAZAMIENTO)
                .Add("CANTIDAD_OPINIONES", analisis.CANTIDAD_OPINIONES)

            End With
            Return vCon.GenericoProcedure_Res3("ANALISIS_INSERT", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function



    Public Shared Function PromedioAnalisis(ByVal analisis As Integer, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("ID_ANALISIS", analisis)

            End With
            Return vCon.GenericoProcedure("PROMEDIO_TOTAL", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    Public Shared Function SelectAnalisis(Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Analisis)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam

        End With
        Try
            vTabla = vCon.ExecSP("ANALISIS_SELECT", vParam)
            SelectAnalisis = New List(Of CL_Analisis)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Analisis
                If Not IsDBNull(vFila("id_analisis")) Then vAtribu.ID = vFila("id_analisis")
                If Not IsDBNull(vFila("nombre_analisis")) Then vAtribu.NOMBRE = vFila("nombre_analisis")
                If Not IsDBNull(vFila("fecha_analisis")) Then vAtribu.FECHA = vFila("fecha_analisis")
                If Not IsDBNull(vFila("cantidad_desplazamiento")) Then vAtribu.DESPLAZAMIENTO = vFila("cantidad_desplazamiento")
                If Not IsDBNull(vFila("cantidad_opiniones")) Then vAtribu.CANTIDAD_OPINIONES = vFila("cantidad_opiniones")
                SelectAnalisis.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function


    
End Class
