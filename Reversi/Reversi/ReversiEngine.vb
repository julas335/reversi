Public Class ReversiField
    Public row As Integer
    Public column As Integer
End Class

Public Class ReversiEngine


        Public Enum ReversiPawn
            black
            white
            empty
        End Enum

        Public Board(7, 7) As ReversiPawn

        Public Sub FlipPieces(ByRef piecesFlipped As List(Of ReversiField), ByVal colour As ReversiPawn)

            For Each Field In piecesFlipped

                If colour = ReversiPawn.white Then
                    Board(Field.row, Field.column) = ReversiPawn.white
                Else
                    Board(Field.row, Field.column) = ReversiPawn.black
                End If
            Next

        End Sub

        Public Function CreateDirectionArray(ByRef items(,) As Integer) As Array

            items(0, 0) = 1
            items(0, 1) = 1
            items(0, 2) = 0
            items(0, 3) = -1
            items(0, 4) = -1
            items(0, 5) = -1
            items(0, 6) = 0
            items(0, 7) = 1

            items(1, 0) = 0
            items(1, 1) = -1
            items(1, 2) = -1
            items(1, 3) = -1
            items(1, 4) = 0
            items(1, 5) = 1
            items(1, 6) = 1
            items(1, 7) = 1

            Return items
        End Function

        Public Function CheckMove(ByVal row As Integer, ByVal column As Integer, ByRef items(,) As Integer, ByVal colour As ReversiPawn) As List(Of ReversiField)
            Dim numToFlip As Integer
            Dim num1, num2 As Integer
            Dim num1Start, num2Start As Integer
            Dim check As Boolean
            Dim piecesFlipped As New List(Of ReversiField)


            'check each direction
            For i = 0 To 7
            Try
                If (Board(row + items(0, i), column + items(1, i)) <> ReversiPawn.empty) And (Board(row + items(0, i), column + items(1, i)) <> colour) Then

                    numToFlip += 1
                    num1 = 2 * items(0, i)
                    num2 = 2 * items(1, i)
                    num1Start = items(0, i)
                    num2Start = items(1, i)


                    Do
                        If (Board(row + num1, column + num2)) <> ReversiPawn.empty And (Board(row + num1, column + num2) <> colour) Then
                            numToFlip += 1
                        ElseIf Board(row + num1, column + num2) = colour Then
                            check = True
                        End If
                        num1 += items(0, i)
                        num2 += items(1, i)
                    Loop Until (check = True) Or ((row + num1) = 7 Or (row + num1) = 0 Or (column + num2) = 7 Or (column + num2) = 0)

                    If check = True Then
                        For e = 1 To numToFlip

                            piecesFlipped.Add(New ReversiField With {.row = row + num1Start,
                                            .column = column + num2Start})
                            num1Start += items(0, i)
                            num2Start += items(1, i)

                        Next
                    End If

                    numToFlip = 0
                    check = False
                End If
            Catch ex As IndexOutOfRangeException

            End Try

            Next

            Return piecesFlipped

            For Each piece In piecesFlipped
                piecesFlipped.Remove(piece)
            Next
        End Function

        Public Function SetUpBoard() As Array

            Dim row, column As Integer

            For row = 0 To 7
                For column = 0 To 7
                    If (row = 3 And column = 3) Or (row = 4 And column = 4) Then
                        Board(row, column) = ReversiPawn.white
                    ElseIf (row = 3 And column = 4) Or (row = 4 And column = 3) Then
                        Board(row, column) = ReversiPawn.black
                    Else
                        Board(row, column) = ReversiPawn.empty
                    End If
                Next
            Next

            Return Board
        End Function

        Public Sub PrintBoard()
            Dim row, column As Integer

            Console.WriteLine()

            For column = 0 To 7

                If column = 0 Then
                    Console.Write("   " & column & " ")
                Else
                    Console.Write("  " & column & " ")
                End If

            Next

            Console.WriteLine()

            For row = 0 To 7

                For column = 0 To 7

                    If Board(row, column) = ReversiPawn.white Then
                        If column = 0 Then
                            Console.Write(row & " _W_|")
                        Else
                            Console.Write("_W_|")
                        End If
                    ElseIf Board(row, column) = ReversiPawn.black Then
                        If column = 0 Then
                            Console.Write(row & " _B_|")
                        Else
                            Console.Write("_B_|")
                        End If
                    ElseIf Board(row, column) = ReversiPawn.empty Then
                        If column = 0 Then
                            Console.Write(row & " ___|")
                        Else
                            Console.Write("___|")
                        End If
                    End If
                Next

                Console.WriteLine()
            Next
        End Sub

    End Class
