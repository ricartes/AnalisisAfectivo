Imports Microsoft.VisualBasic

Public Class Stemmer

    Public Function Execute(ByVal word As String) As String
        Return Execute(word, False)
    End Function


    Public Function Execute(ByVal word As String, ByVal useStopWords As Boolean) As String
        Dim result As String = word

        If Not useStopWords AndAlso Specials.stop_words.IndexOf(word) < 0 Then

            If word.Length >= 3 Then
                Dim sb As StringBuilder = New StringBuilder(word.ToLower())
                If sb(0) = "'"c Then sb.Remove(0, 1)
                Dim r1 As Integer = 0, r2 As Integer = 0, rv As Integer = 0
                computeR1R2RV(sb, r1, r2, rv)
                step0(sb, rv)
                Dim cont As Integer = sb.Length
                step1(sb, r1, r2)

                If sb.Length = cont Then
                    step2a(sb, rv)

                    If sb.Length = cont Then
                        step2b(sb, rv)
                    End If
                End If

                step3(sb, rv)
                removeAcutes(sb)
                result = sb.ToString().ToLower()
            End If
        End If

        Return result
    End Function


    Private Sub computeR1R2RV(ByVal sb As StringBuilder, ByRef r1 As Integer, ByRef r2 As Integer, ByRef rv As Integer)
        r1 = sb.Length
        r2 = sb.Length
        rv = sb.Length

        For i As Integer = 1 To sb.Length - 1

            If (Not isVowel(sb(i))) AndAlso (isVowel(sb(i - 1))) Then
                r1 = i + 1
                Exit For
            End If
        Next

        For i As Integer = r1 + 1 To sb.Length - 1

            If (Not isVowel(sb(i))) AndAlso (isVowel(sb(i - 1))) Then
                r2 = i + 1
                Exit For
            End If
        Next

        If sb.Length >= 2 Then

            If Not isVowel(sb(1)) Then

                For i As Integer = 1 To sb.Length - 1

                    If isVowel(sb(i)) Then
                        rv = If(sb.Length > i, i + 1, sb.Length)
                        Exit For
                    End If
                Next
            Else

                If isVowel(sb(0)) AndAlso isVowel(sb(1)) Then

                    For i As Integer = 1 To sb.Length - 1

                        If Not isVowel(sb(i)) Then
                            rv = If(sb.Length > i, i + 1, sb.Length)
                            Exit For
                        End If
                    Next
                Else
                    rv = If(sb.Length >= 3, 3, sb.Length)
                End If
            End If
        End If
    End Sub


    Private Function isVowel(ByVal c As Char) As Boolean
        Return Specials.Vocales.IndexOf(c) >= 0
    End Function


    Private Sub step0(ByVal sb As StringBuilder, ByVal rv As Integer)
        Dim index As Integer = -1
        Dim i As Integer = 5

        While i > 1 AndAlso index < 0

            If sb.Length >= i Then
                index = Specials.Step0.LastIndexOf(sb.ToString(sb.Length - i, i))

                If index >= 0 Then
                    Dim aux As String = Specials.Step0(index)
                    Dim index_after As Integer = Specials.AfterStep0.LastIndexOf(aux)

                    If index_after >= 0 Then
                        Dim palabra As String = Specials.AfterStep0(index_after)

                        If sb.ToString(0, index).Substring(0, index_after).Length + palabra.Length = sb.ToString(0, index).Length Then

                            If Specials.AfterStep0(index_after) = "yendo" AndAlso sb(index_after - 1) = "u"c AndAlso index_after >= rv Then
                                sb.Remove(sb.Length - index, index)
                            Else
                                sb.Remove(sb.Length - index, index)

                                For j As Integer = index_after To sb.Length - 1
                                    sb(j) = Specials.EliminaAcento(sb(j))
                                Next
                            End If
                        End If
                    End If
                End If
            End If

            i -= 1
        End While
    End Sub

    Private Sub step1(ByVal sb As StringBuilder, ByVal r1 As Integer, ByVal r2 As Integer)
        Dim posicion As Integer = -1
        Dim coleccion As Integer = -1
        Dim encontrada As String = ""
        Dim buscar As String = sb.ToString()

        For Each s As String In Specials.Step1_1
            Dim index As Integer = buscar.LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = buscar.Substring(index)
                Dim aux As Integer = -1
                aux = Specials.Step1_1.LastIndexOf(palabra)

                If aux >= 0 AndAlso Specials.Step1_1(aux).Length > encontrada.Length Then
                    encontrada = Specials.Step1_1(aux)
                    posicion = index
                    coleccion = 1
                End If
            End If
        Next

        For Each s As String In Specials.Step1_2
            Dim index As Integer = buscar.LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = buscar.Substring(index)
                Dim aux As Integer = -1
                aux = Specials.Step1_2.LastIndexOf(palabra)

                If aux >= 0 AndAlso Specials.Step1_2(aux).Length > encontrada.Length Then
                    encontrada = Specials.Step1_2(aux)
                    posicion = index
                    coleccion = 2
                End If
            End If
        Next

        For Each s As String In Specials.Step1_3
            Dim index As Integer = buscar.LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = buscar.Substring(index)
                Dim aux As Integer = -1
                aux = Specials.Step1_3.LastIndexOf(palabra)

                If aux >= 0 AndAlso Specials.Step1_3(aux).Length > encontrada.Length Then
                    encontrada = Specials.Step1_3(aux)
                    posicion = index
                    coleccion = 3
                End If
            End If
        Next

        For Each s As String In Specials.Step1_4
            Dim index As Integer = buscar.LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = buscar.Substring(index)
                Dim aux As Integer = -1
                aux = Specials.Step1_4.LastIndexOf(palabra)

                If aux >= 0 AndAlso Specials.Step1_4(aux).Length > encontrada.Length Then
                    encontrada = Specials.Step1_4(aux)
                    posicion = index
                    coleccion = 4
                End If
            End If
        Next

        For Each s As String In Specials.Step1_5
            Dim index As Integer = buscar.LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = buscar.Substring(index)
                Dim aux As Integer = -1
                aux = Specials.Step1_5.LastIndexOf(palabra)

                If aux >= 0 AndAlso Specials.Step1_5(aux).Length > encontrada.Length Then
                    encontrada = Specials.Step1_5(aux)
                    posicion = index
                    coleccion = 5
                End If
            End If
        Next

        For Each s As String In Specials.Step1_6
            Dim index As Integer = buscar.LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = buscar.Substring(index)
                Dim aux As Integer = -1
                aux = Specials.Step1_6.LastIndexOf(palabra)

                If aux >= 0 AndAlso Specials.Step1_6(aux).Length > encontrada.Length Then
                    encontrada = Specials.Step1_6(aux)
                    posicion = index
                    coleccion = 6
                End If
            End If
        Next

        For Each s As String In Specials.Step1_7
            Dim index As Integer = buscar.LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = buscar.Substring(index)
                Dim aux As Integer = -1
                aux = Specials.Step1_7.LastIndexOf(palabra)

                If aux >= 0 AndAlso Specials.Step1_7(aux).Length > encontrada.Length Then
                    encontrada = Specials.Step1_7(aux)
                    posicion = index
                    coleccion = 7
                End If
            End If
        Next

        For Each s As String In Specials.Step1_8
            Dim index As Integer = buscar.LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = buscar.Substring(index)
                Dim aux As Integer = -1
                aux = Specials.Step1_8.LastIndexOf(palabra)

                If aux >= 0 AndAlso Specials.Step1_8(aux).Length > encontrada.Length Then
                    encontrada = Specials.Step1_8(aux)
                    posicion = index
                    coleccion = 8
                End If
            End If
        Next

        For Each s As String In Specials.Step1_9
            Dim index As Integer = buscar.LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = buscar.Substring(index)
                Dim aux As Integer = -1
                aux = Specials.Step1_9.LastIndexOf(palabra)

                If aux >= 0 AndAlso Specials.Step1_9(aux).Length > encontrada.Length Then
                    encontrada = Specials.Step1_9(aux)
                    posicion = index
                    coleccion = 9
                End If
            End If
        Next

        If posicion >= 0 Then

            Select Case coleccion
                Case 1
                    If posicion >= r2 Then sb.Remove(posicion, sb.Length - posicion)
                Case 2
                    If posicion >= r2 Then sb.Remove(posicion, sb.Length - posicion)
                Case 3

                    If posicion >= r2 Then
                        sb.Remove(posicion, sb.Length - posicion)
                        sb.Append("log")
                    End If

                Case 4

                    If posicion >= r2 Then
                        sb.Remove(posicion, sb.Length - posicion)
                        sb.Append("u")
                    End If

                Case 5

                    If posicion >= r2 Then
                        sb.Remove(posicion, sb.Length - posicion)
                        sb.Append("ente")
                    End If

                Case 6

                    If posicion >= r1 Then
                        sb.Remove(posicion, sb.Length - posicion)
                    Else
                        Dim aux As String = sb.ToString(0, posicion)

                        If aux.Substring(0, aux.Length - 2) = "iv" AndAlso aux.Substring(0, aux.Length - 2) = "oc" AndAlso aux.Substring(0, aux.Length - 2) = "ic" AndAlso aux.Substring(0, aux.Length - 2) = "ad" AndAlso posicion >= r2 Then
                            sb.Remove(posicion, sb.Length - posicion)
                        End If
                    End If

                Case 7, 8, 9

                    If posicion >= r2 Then
                        sb.Remove(posicion, sb.Length - posicion)
                    End If
            End Select
        End If
    End Sub

    Private Sub step2a(ByVal sb As StringBuilder, ByVal rv As Integer)
        Dim index As Integer = -1
        index = Specials.Step2_a.IndexOf(sb.ToString())

        If index >= rv AndAlso sb.ToString().Substring(sb.Length - index - 1, 1) = "u" Then
            sb.Remove(sb.Length - index, index)
        End If
    End Sub

    Private Sub step2b(ByVal sb As StringBuilder, ByVal rv As Integer)
        Dim seleccionado As String = ""
        Dim pos As Integer = -1
        Dim index As Integer = -1

        For Each s As String In Specials.Step2_b1
            index = sb.ToString().LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = sb.ToString().Substring(index)
                Dim aux As Integer = index
                index = Specials.Step2_b1.LastIndexOf(palabra)

                If index >= 0 Then
                    seleccionado = Specials.Step2_b1(index)
                    pos = aux
                End If
            End If
        Next

        If pos >= rv AndAlso sb.ToString(sb.Length - pos - 2, pos) = "gu" Then sb.Remove(pos - 1, sb.Length - pos + 1)
        pos = -1
        index = -1
        seleccionado = ""

        For Each s As String In Specials.Step2_b2
            index = sb.ToString().LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = sb.ToString().Substring(index)
                Dim aux As Integer = index
                index = Specials.Step2_b2.LastIndexOf(palabra)

                If index >= 0 Then
                    seleccionado = Specials.Step2_b2(index)
                    pos = aux
                End If
            End If
        Next

        If pos >= rv Then sb.Remove(pos, sb.Length - pos)
    End Sub

    Private Sub step3(ByVal sb As StringBuilder, ByVal rv As Integer)
        Dim seleccionado As String = ""
        Dim pos As Integer = -1
        Dim index As Integer = -1

        For Each s As String In Specials.Step3_1
            index = sb.ToString().LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = sb.ToString().Substring(index)
                Dim aux As Integer = index
                index = Specials.Step3_1.LastIndexOf(palabra)

                If index >= 0 Then
                    seleccionado = Specials.Step3_1(index)
                    pos = aux
                End If
            End If
        Next

        If pos >= rv Then sb.Remove(pos, sb.Length - pos)
        pos = -1
        index = -1
        seleccionado = ""

        For Each s As String In Specials.Step3_2
            index = sb.ToString().LastIndexOf(s)

            If index >= 0 Then
                Dim palabra As String = sb.ToString().Substring(index)
                Dim aux As Integer = index
                index = Specials.Step3_2.LastIndexOf(palabra)

                If index >= 0 Then
                    seleccionado = Specials.Step3_2(index)
                    pos = index
                End If
            End If
        Next

        If pos >= 0 AndAlso sb.ToString(sb.Length - pos - 2, pos) = "gu" AndAlso pos - 1 >= rv Then sb.Remove(pos - 1, sb.Length - pos + 1)
    End Sub

    Private Sub removeAcutes(ByVal sb As StringBuilder)
        For i As Integer = 0 To sb.Length - 1
            Dim c As Char = sb(i)
            sb(i) = Specials.EliminaAcento(c)
        Next
    End Sub

End Class
