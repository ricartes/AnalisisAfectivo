Imports Microsoft.VisualBasic

Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class Conexion
    Public Class ParametroCursor
    End Class
    Private Shared vStrConComun As String = ConfigurationManager.ConnectionStrings("ApplicationServices").ToString

    Private _Con As SqlConnection
    Private _Tra As IDbTransaction
    Private Const TimeoutConexion As Integer = 90

    ''' <summary>
    ''' OBTIENE EL OBJETO CONEXION
    ''' </summary>
    Public ReadOnly Property Conexion() As SqlConnection
        Get
            Return _Con
        End Get
    End Property
    Public Shared Property CadenaConexion() As String
        Get
            CadenaConexion = vStrConComun
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value) Then
                Throw New Exception("No se puede establecer la cadena de conexion a una cadena vacía o nula.")
            End If
            vStrConComun = value
        End Set
    End Property
    Public Shared Property ConnectionString() As String
        Get
            'Try
            If String.IsNullOrEmpty(CadenaConexion) Then
                'Return My.Settings.ConnectionString
                Throw New ApplicationException("No se ha establecido un String de conexión válido para la conexión.")
            Else
                Return CadenaConexion
            End If

            ' Catch ex As Exception
            'Return ""
            'End Try

        End Get
        Set(ByVal value As String)
            CadenaConexion = value
        End Set
    End Property

    ''' <summary>
    ''' Constructor de la conexion.
    ''' </summary>
    ''' <param name="vStrCon">String de conexi�n.</param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal vStrCon As String = "")
        If String.IsNullOrEmpty(vStrCon) Then
            If String.IsNullOrEmpty(ConnectionString) Then
                Throw New Exception("No se puede crear la conexión porque no se ha provisto una cadena de conexión válida.")
            End If
            vStrCon = ConnectionString
        End If
        _Con = New SqlConnection(vStrCon)
    End Sub

    Public Function ExecSP(ByVal vProcAlmacenado As String, _
                           Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing, _
                           Optional ByVal vTimeoutConexion As Integer = TimeoutConexion, _
                           Optional ByVal vLeerCursor As Boolean = True, _
                           Optional ByVal vLeerError As Boolean = False) As DataTable



        Dim cmdTmp As SqlCommand
        Dim dt As New DataTable

        cmdTmp = New SqlCommand(vProcAlmacenado)
        cmdTmp.CommandType = CommandType.StoredProcedure
        cmdTmp.Connection = _Con
        cmdTmp.CommandTimeout = vTimeoutConexion
        'cmdTmp.BindByName = True
        If _Tra IsNot Nothing And _Con.State = ConnectionState.Open Then
            'cmdTmp.Transaction = _Tra
        End If
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                If TypeOf vParaMetros(vClave) Is ParametroCursor Then
                    Dim vP As New SqlParameter
                    vP.ParameterName = vClave.ToUpper.Trim
                    vP.Direction = ParameterDirection.Output
                    vP.SqlDbType = SqlDbType.Variant
                    cmdTmp.Parameters.Add(vP)
                Else
                    cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
                End If
            Next
        End If
        Try
            Dim dtd As New SqlDataAdapter(cmdTmp)
            dtd.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try


    End Function

    Public Function ExecSP_DS(ByVal vProcAlmacenado As String, _
                           Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing, _
                           Optional ByVal vTimeoutConexion As Integer = TimeoutConexion) As DataSet
        Dim cmdTmp As SqlCommand
        Dim dt As New DataSet

        cmdTmp = New SqlCommand(vProcAlmacenado)
        cmdTmp.CommandType = CommandType.StoredProcedure
        cmdTmp.Connection = _Con
        cmdTmp.CommandTimeout = vTimeoutConexion
        If _Tra IsNot Nothing And _Con.State = ConnectionState.Open Then
            'cmdTmp.Transaction = _Tra
        End If
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
            Next
        End If
        Try
            Dim dtd As New SqlDataAdapter(cmdTmp)
            dtd.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Function

    Public Function ExecSP2(ByVal vProcAlmacenado As String, _
                         Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing, _
                         Optional ByVal vTimeoutConexion As Integer = TimeoutConexion, _
                         Optional ByVal vLeerCursor As Boolean = True, _
                         Optional ByVal vLeerError As Boolean = False) As DataTable

        Dim cmdTmp As SqlCommand
        Dim dt As New DataTable

        cmdTmp = New SqlCommand(vProcAlmacenado)
        cmdTmp.CommandType = CommandType.StoredProcedure
        cmdTmp.Connection = _Con
        cmdTmp.CommandTimeout = vTimeoutConexion
        If _Tra IsNot Nothing And _Con.State = ConnectionState.Open Then
            ' cmdTmp.Transaction = _Tra
        End If
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                If TypeOf vParaMetros(vClave) Is ParametroCursor Then
                    Dim vP As New SqlParameter
                    vP.ParameterName = vClave.ToUpper
                    vP.Direction = ParameterDirection.Output
                    vP.SqlDbType = SqlDbType.NVarChar
                    cmdTmp.Parameters.Add(vP)
                Else
                    cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
                End If
            Next
        End If
        Try
            Dim dtd As New SqlDataAdapter(cmdTmp)
            dtd.Fill(dt)
            Return dt
        Catch ex As Exception

            If ex.Message.Contains("") Then

                Throw New Exception("MENSAJE")

            ElseIf ex.Message.Contains("") Then

            End If
            Throw New ApplicationException(ex.Message)
        End Try

    End Function

    Public Function GenericoProcedure(ByVal procedimiento As String, Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing) As String
        Dim respuesta As String
        Try
            Dim Resp_Ret As String = String.Empty
            Dim cmdTmp As SqlCommand
            cmdTmp = New SqlCommand(procedimiento)
            cmdTmp.CommandTimeout = 200
            cmdTmp.CommandType = System.Data.CommandType.StoredProcedure
            cmdTmp.Connection = Conexion
            If vParaMetros IsNot Nothing Then
                'tenemos argumentos que preparar para el sp.
                For Each vClave As String In vParaMetros.Keys
                    If TypeOf vParaMetros(vClave) Is ParametroCursor Then
                        Dim vP As New SqlParameter
                        vP.ParameterName = vClave.ToUpper
                        vP.Direction = ParameterDirection.Output
                        vP.SqlDbType = SqlDbType.NVarChar
                        cmdTmp.Parameters.Add(vP)
                    Else
                        cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
                    End If
                Next
            End If

            cmdTmp.Connection.Open()
            respuesta = cmdTmp.ExecuteNonQuery()
            cmdTmp.Connection.Close()
            cmdTmp.Dispose()
            Return respuesta
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString)
        End Try

    End Function

    Public Function GenericoProcedure_Msg(ByVal procedimiento As String, Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing) As String

        Dim Resp_Ret As String = String.Empty
        Dim cmdTmp As SqlCommand
        cmdTmp = New SqlCommand(procedimiento)
        cmdTmp.CommandType = System.Data.CommandType.StoredProcedure
        cmdTmp.Connection = Conexion
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                If TypeOf vParaMetros(vClave) Is ParametroCursor Then
                    Dim vP As New SqlParameter
                    vP.ParameterName = vClave.ToUpper
                    vP.Direction = ParameterDirection.Output
                    vP.SqlDbType = SqlDbType.NVarChar
                    cmdTmp.Parameters.Add(vP)
                Else
                    cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
                End If
            Next
        End If

        cmdTmp.Parameters.Add(New SqlParameter("PN_COD_ERROR", SqlDbType.Int)).Direction = ParameterDirection.Output
        cmdTmp.Parameters.Add(New SqlParameter("PV_DES_ERROR", SqlDbType.VarChar, 1024)).Direction = ParameterDirection.Output
        cmdTmp.Connection.Open()
        cmdTmp.ExecuteNonQuery()
        If cmdTmp.Parameters("PN_COD_ERROR").Value <> 0 Then
            If cmdTmp.Parameters("PV_DES_ERROR").Value.ToString.ToUpper.Contains("ORA-02292") Then
                Resp_Ret = "No es posible alterar el registro especificado. Posee dependencias asociadas."
            ElseIf cmdTmp.Parameters("PV_DES_ERROR").Value.ToString.ToUpper.Contains("ORA-00001") Then
                Resp_Ret = "El elemento especificado ya se encuentra registrado."
            Else
                Resp_Ret = cmdTmp.Parameters("PV_DES_ERROR").Value
            End If

        Else
            If IsDBNull(cmdTmp.Parameters("PN_COD_ERROR").Value) = 0 Then
                Resp_Ret = cmdTmp.Parameters("PV_DES_ERROR").Value
            End If

        End If

        cmdTmp.Connection.Close()
        cmdTmp.Dispose()
        Return Resp_Ret
    End Function





    Public Function GenericoProcedure_Res2(ByVal procedimiento As String, Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing) As String

        Dim Resp_Ret As String = String.Empty
        Dim cmdTmp As SqlCommand
        cmdTmp = New SqlCommand(procedimiento)
        cmdTmp.CommandType = System.Data.CommandType.StoredProcedure
        cmdTmp.Connection = Conexion
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                If TypeOf vParaMetros(vClave) Is ParametroCursor Then
                    Dim vP As New SqlParameter
                    vP.ParameterName = vClave.ToUpper
                    vP.Direction = ParameterDirection.Output
                    vP.SqlDbType = SqlDbType.NVarChar
                    cmdTmp.Parameters.Add(vP)
                Else
                    cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
                End If
            Next
        End If

        cmdTmp.Parameters.Add(New SqlParameter("PC_NOMBRE", SqlDbType.VarChar, 100)).Direction = ParameterDirection.Output
        cmdTmp.Connection.Open()
        cmdTmp.ExecuteNonQuery()

        If IsDBNull(cmdTmp.Parameters("PC_NOMBRE").Value) Then
            Resp_Ret = String.Empty
        Else
            Resp_Ret = cmdTmp.Parameters("PC_NOMBRE").Value
        End If




        cmdTmp.Connection.Close()
        cmdTmp.Dispose()


        GenericoProcedure_Res2 = Resp_Ret


    End Function

    Public Function GenericoProcedure_Res3(ByVal procedimiento As String, Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing) As Double

        Dim Resp_Ret As Double = 0
        Dim cmdTmp As SqlCommand
        cmdTmp = New SqlCommand(procedimiento)
        cmdTmp.CommandType = System.Data.CommandType.StoredProcedure
        cmdTmp.Connection = Conexion
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                If TypeOf vParaMetros(vClave) Is ParametroCursor Then
                    Dim vP As New SqlParameter
                    vP.ParameterName = vClave.ToUpper
                    vP.Direction = ParameterDirection.Output
                    vP.SqlDbType = SqlDbType.NVarChar
                    cmdTmp.Parameters.Add(vP)
                Else
                    cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
                End If
            Next
        End If

        cmdTmp.Parameters.Add(New SqlParameter("pn_resultado", SqlDbType.Decimal)).Direction = ParameterDirection.Output
        cmdTmp.Connection.Open()
        cmdTmp.ExecuteNonQuery()

        Resp_Ret = cmdTmp.Parameters("pn_resultado").Value



        cmdTmp.Connection.Close()
        cmdTmp.Dispose()


        GenericoProcedure_Res3 = Resp_Ret


    End Function


    Public Function GenericoProcedureWithConexion(ByVal procedimiento As String, ByVal vCon As SqlConnection, Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing) As String

        Dim Resp_Ret As String = String.Empty
        Dim cmdTmp As SqlCommand
        cmdTmp = New SqlCommand(procedimiento)
        cmdTmp.CommandType = System.Data.CommandType.StoredProcedure
        cmdTmp.Connection = vCon
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                If TypeOf vParaMetros(vClave) Is ParametroCursor Then
                    Dim vP As New SqlParameter
                    vP.ParameterName = vClave.ToUpper
                    vP.Direction = ParameterDirection.Output
                    vP.SqlDbType = SqlDbType.NVarChar
                    cmdTmp.Parameters.Add(vP)
                Else
                    cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
                End If
            Next
        End If

        cmdTmp.Parameters.Add(New SqlParameter("PN_COD_ERROR", SqlDbType.Int)).Direction = ParameterDirection.Output
        cmdTmp.Parameters.Add(New SqlParameter("PV_DES_ERROR", SqlDbType.VarChar, 1024)).Direction = ParameterDirection.Output
        'cmdTmp.Connection.Open()
        cmdTmp.ExecuteNonQuery()
        If cmdTmp.Parameters("PN_COD_ERROR").Value <> 0 Then
            If cmdTmp.Parameters("PV_DES_ERROR").Value.ToString.ToUpper.Contains("ORA-02292") Then
                Resp_Ret = "No es posible alterar el registro especificado. Posee dependencias asociadas."
            ElseIf cmdTmp.Parameters("PV_DES_ERROR").Value.ToString.ToUpper.Contains("ORA-00001") Then
                Resp_Ret = "El elemento especificado ya se encuentra registrado."
            Else
                Resp_Ret = cmdTmp.Parameters("PV_DES_ERROR").Value
            End If

        Else
            'If IsDBNull(cmdTmp.Parameters("PN_COD_ERROR").Value) = 0 Then
            '    Resp_Ret = cmdTmp.Parameters("PV_DES_ERROR").Value
            'End If
            Resp_Ret = String.Empty
        End If
        ' cmdTmp.Connection.Close()
        cmdTmp.Dispose()
        Return Resp_Ret
    End Function



    Public Function ExecuteFunctionQuery(ByVal stringSQL As String, Optional ByVal vTimeoutConexion As Integer = TimeoutConexion) As String

        Dim OraComando As SqlCommand
        Dim resultado As String
        OraComando = Nothing
        OraComando = New SqlCommand()
        OraComando.Connection = _Con
        OraComando.CommandTimeout = vTimeoutConexion
        OraComando.CommandType = CommandType.Text
        OraComando.CommandText = stringSQL
        If _Tra IsNot Nothing And _Con.State = ConnectionState.Open Then
            'cmdTmp.Transaction = _Tra
        End If
        Try
            resultado = Convert.ToString(OraComando.ExecuteScalar())
        Catch ex As SqlException
            Throw New ApplicationException(ex.Message)
        Finally
            OraComando.Dispose()
            OraComando = Nothing
        End Try
        Return resultado
    End Function



    Public Sub Confirmar()
        If _Con.State = ConnectionState.Closed Then
            Throw New Exception("La conexión está cerrada, no se puede confirmar la transacción.")
        Else
            _Tra.Commit()
            _Con.Close()
        End If
    End Sub

    Public Sub Deshacer()
        If _Con.State = ConnectionState.Closed Then
            Throw New Exception("La conexión está cerrada, no se puede deshacer la transacción.")
        Else
            _Tra.Rollback()
            _Con.Close()
        End If
    End Sub

    Public Sub IniciarTransaccion()
        If _Con.State = ConnectionState.Open And _Tra IsNot Nothing Then
            Throw New Exception("La conexión ya está establecida y la transacción iniciada. Cierre la transacción actual con el método Deshacer o con el método Confirmar.")
        Else
            _Con.Open()
            _Tra = _Con.BeginTransaction()
        End If
    End Sub

    Protected Overloads Overrides Sub Finalize()
        If _Con IsNot Nothing AndAlso _Con.State = ConnectionState.Open Then
            If _Tra IsNot Nothing Then
                Try
                    _Tra.Rollback()
                    _Tra.Dispose()
                Catch ex As Exception

                End Try
            End If
            Try
                _Con.Close()
                _Con.Dispose()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Public ReadOnly Property Estado() As Data.ConnectionState
        Get
            Estado = _Con.State
        End Get
    End Property

    Public Enum EstadoEjecucion
        EstadoOk = 0
        EstadoError = 1
    End Enum






End Class
