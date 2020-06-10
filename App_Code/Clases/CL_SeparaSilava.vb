Imports Microsoft.VisualBasic

Public Class CL_SeparaSilava

    Public Property CADENA As String




    Public Function SILABA(ByVal str As String) As String
        Dim temp As String = ""
        Dim s As String = ""
        Dim x, y, z As Char

        If Len(str) < 3 Then
            If Len(str) = 2 Then
                x = str(0)
                y = str(1)
                If LETRA(x) < 6 And LETRA(y) < 6 Then
                    If HIATO(x, y) Then
                        s = str.Substring(0, 1)
                    Else
                        s = str
                    End If
                Else
                    s = str
                End If
            Else
                s = str
            End If

        Else

            x = str(0)
            y = str(1)
            z = str(2)
            If LETRA(x) < 6 Then
                If LETRA(y) < 6 Then
                    If LETRA(z) < 6 Then
                        If HIATO(x, y) Then
                            s = str.Substring(0, 1)
                        Else
                            If HIATO(y, z) Then
                                s = str.Substring(0, 2)
                            Else
                                s = str.Substring(0, 3)
                            End If
                        End If
                    Else
                        If HIATO(x, y) Then
                            s = str.Substring(0, 1)
                        Else
                            s = str.Substring(0, 2)
                        End If
                    End If

                Else

                    If LETRA(z) < 6 Then
                        If LETRA(y) = 6 Then
                            If HIATO(x, z) Then
                                s = str.Substring(0, 1)
                            Else
                                s = str.Substring(0, 3)
                            End If
                        Else
                            s = str.Substring(0, 1)
                        End If
                    Else
                        If CONSONANTES1(y, z) Then
                            s = str.Substring(0, 1)
                        Else
                            s = str.Substring(0, 2)
                        End If

                    End If

                End If
            Else

                If LETRA(y) < 6 Then
                    If LETRA(z) < 6 Then
                        temp = str.Substring(0, 3)
                        If temp = "que" Or temp = "qui" Or temp = "gue" Or temp = "gui" Then
                            s = str.Substring(0, 3)
                        Else
                            If HIATO(y, z) Then
                                s = str.Substring(0, 2)
                            Else
                                s = str.Substring(0, 3)
                            End If
                        End If

                    Else
                        s = str.Substring(0, 2)

                    End If

                Else

                    If LETRA(z) < 6 Then
                        If CONSONANTES1(x, y) Then
                            s = str.Substring(0, 3)
                        Else
                            s = str.Substring(0, 1)
                        End If
                    Else
                        If CONSONANTES1(y, z) Then
                            s = str.Substring(0, 1)
                        Else
                            s = str.Substring(0, 1)
                        End If

                    End If

                End If
            End If

        End If
        Return s
    End Function


    Private Function STRCONSONANTES(ByVal STR As String) As String
        Dim CER As Boolean
        Dim K As Integer
        Dim C As Char()
        CER = False
        K = 0
        C = STR.ToCharArray()

        For I As Integer = 0 To STR.Length - 1
            If LETRA(C(I)) > 5 Then
                K = K + 1
            End If
        Next
        If K = STR.Length Then
            CER = True
        End If
        Return CER
    End Function


    Private Function SILABAREST(ByVal STR As String) As String
        Dim S2 As String
        S2 = SILABA(STR)
        Return STR.Substring(S2.Length)
    End Function



    Private Function CONSONANTES1(ByVal A As Char, ByVal B As Char) As Boolean
        Dim CER As Boolean
        CER = False

        If A = "b" Or A = "c" Or A = "d" Or A = "f" Or A = "g" Or A = "p" Or A = "r" Or A = "t" Then
            If B = "r" Then
                CER = True
            End If
        End If

        If A = "b" Or A = "c" Or A = "f" Or A = "g" Or A = "p" Or A = "t" Or A = "1" Then
            If B = "1" Then
                CER = True
            End If
        End If

        If B = "h" Then
            If A = "c" Then
                CER = True
            End If
        End If
        Return CER
    End Function

    Private Function LETRA(ByVal C As Char) As Integer
        Dim i As Integer = -1
        Dim ascii As Integer
        ascii = Asc(C)
        If ascii <> -1 Then
            Select Case ascii
                Case 97
                    i = 1
                Case 101
                    i = 2
                Case 104
                    i = 6
                Case 105
                    i = 4
                Case 111
                    i = 3
                Case 117
                    i = 5
                Case 225
                    i = 1
                Case 233
                    i = 2
                Case 237
                    i = 4
                Case 243
                    i = 3
                Case 250
                    i = 5
                Case 252
                    i = 5
                Case Else
                    i = 19
            End Select
        End If
        Return i
    End Function



    Private Function HIATO(ByVal V As Char, ByVal V2 As Char) As Boolean
        Dim CER As Boolean = False

        If LETRA(V) < 4 Then
            If LETRA(V2) < 4 Then
                CER = True
            Else
                If V = "í" Or V = "ú" Then
                    CER = True
                Else
                    CER = False
                End If
            End If
        Else

            If LETRA(V2) < 4 Then
                If V = "í" Or V = "ú" Then
                    CER = True
                Else
                    CER = False
                End If
            Else
                If V = V2 Then
                    CER = True
                Else
                    CER = False
                End If
            End If

        End If
        Return CER
    End Function


    Private Function STRWSTR(ByVal S1 As String, ByVal S2 As String) As Boolean
        Dim CER As Boolean
        Dim C1, C2 As Char
        C1 = S1(S1.Length - 1)
        C2 = S2(0)
        CER = False

        If LETRA(C1) < 6 And LETRA(C2) < 6 Then
            If HIATO(C1, C2) Then
                CER = False
            Else
                CER = True
            End If
        End If
        Return CER
    End Function


    Public Function SILABEAR(ByVal PALABRA As String) As String
        Dim TEMP As String
        Dim S As String = ""
        Dim K As Integer
        K = PALABRA.Length - 1

        For I As Integer = 0 To K
            TEMP = SILABA(PALABRA)
            If I = 0 Then
                S = S + TEMP
            Else
                If STRCONSONANTES(TEMP) Then
                    S = S + TEMP
                Else
                    If STRWSTR(S, TEMP) Then
                        S = S + TEMP
                    Else
                        If STRCONSONANTES(S) Then
                            S = S + TEMP
                        Else
                            S = S + "-" + TEMP
                        End If
                    End If
                End If
            End If
            I = I + TEMP.Length - 1
            PALABRA = SILABAREST(PALABRA)
        Next
        Return S
    End Function


    Public Function CONTAR_SILABEAR(ByVal PALABRA As String) As Integer
        Dim TEMP As String
        Dim S As String = ""
        Dim K As Integer
        Dim contador As Integer = 1
        K = PALABRA.Length - 1

        For I As Integer = 0 To K
            TEMP = SILABA(PALABRA)
            If I = 0 Then
                S = S + TEMP
            Else
                If STRCONSONANTES(TEMP) Then
                    S = S + TEMP
                Else
                    If STRWSTR(S, TEMP) Then
                        S = S + TEMP
                    Else
                        If STRCONSONANTES(S) Then
                            S = S + TEMP
                        Else
                            S = S + "-" + TEMP
                            contador = contador + 1
                        End If
                    End If
                End If
            End If
            I = I + TEMP.Length - 1
            PALABRA = SILABAREST(PALABRA)
        Next
        Return contador
    End Function

End Class
