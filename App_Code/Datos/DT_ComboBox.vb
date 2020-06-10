Imports Microsoft.VisualBasic
Imports System.Data

Public Class DT_ComboBox

    Public Shared Function Sentimiento(Optional ByVal vCon As Conexion = Nothing) As List(Of ComboBox)
        Dim vParam As New Dictionary(Of String, Object)
        Dim vTabla As New DataTable
        If vCon Is Nothing Then vCon = New Conexion
        With vParam

        End With
        Try
            vTabla = vCon.ExecSP("SENTIMIENTO_SELECT", vParam)
            Sentimiento = New List(Of ComboBox)
            For Each vFila As DataRow In vTabla.Rows
                Dim vAtribu As New ComboBox
                If Not IsDBNull(vFila("ID")) Then vAtribu.CODIGO = vFila("ID")
                If Not IsDBNull(vFila("NOMBRE")) Then vAtribu.DESCRIPCION = vFila("NOMBRE")
                Sentimiento.Add(vAtribu)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try
    End Function

End Class
