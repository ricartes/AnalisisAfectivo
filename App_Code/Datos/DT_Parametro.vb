Imports Microsoft.VisualBasic
Imports System.Data

Public Class DT_Parametro
    Public Shared Function SelectParametro(Optional ByVal vCon As Conexion = Nothing) As List(Of CL_Parametro)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion

        Try
            vTabla = vCon.ExecSP("PARAMETROS_SELECT", vParam)
            SelectParametro = New List(Of CL_Parametro)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New CL_Parametro
                If Not IsDBNull(vFila("DESPLAZAMIENTO")) Then vAtribu.VALOR_DESPLAZAMIENTO = vFila("DESPLAZAMIENTO")
                SelectParametro.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function

    Public Shared Function DesplazamientoUpdate(ByVal VALOR As Integer, ByVal usuario As String, Optional ByVal vCon As Conexion = Nothing) As String
        Try
            Dim vParam As New Dictionary(Of String, Object)
            If vCon Is Nothing Then vCon = New Conexion
            With vParam
                .Add("VALOR", VALOR)


            End With
            Return vCon.GenericoProcedure("DESPLAZAMIENTO_UPDATE", vParam)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function

End Class
