Module Module1
    Public Class Field
        Public row As Integer
        Public column As Integer
    End Class

    Sub FlipPieces(ByRef piecesFlipped As List(Of Field), ByRef Board(,) As Char, ByVal colour As Char)

        For Each field In piecesFlipped

            If colour = "W" Then
                Board(field.row, field.column) = "W"
            Else
                Board(field.row, field.column) = "B"
            End If
        Next

    End Sub

    Function CheckMove(ByRef Board(,) As Char, ByVal row As Integer, ByVal column As Integer, ByRef items(,) As Integer, ByVal colour As Char) As List(Of Field)
        Dim numToFlip As Integer
        Dim num1, num2 As Integer
        Dim num1Start, num2Start As Integer
        Dim check As Boolean
        Dim piecesFlipped As New List(Of Field)


        'check each direction
        For i = 0 To 7
            Try
                If (Board(row + items(0, i), column + items(1, i)) <> "-") And (Board(row + items(0, i), column + items(1, i)) <> colour) Then

                    numToFlip += 1
                    num1 = 2 * items(0, i)
                    num2 = 2 * items(1, i)
                    num1Start = items(0, i)
                    num2Start = items(1, i)


                    Do
                        If (Board(row + num1, column + num2)) <> "-" And (Board(row + num1, column + num2) <> colour) Then
                            numToFlip += 1
                        ElseIf Board(row + num1, column + num2) = colour Then
                            check = True
                        End If
                        num1 += items(0, i)
                        num2 += items(1, i)
                    Loop Until (check = True) Or ((row + num1) = 7 Or (row + num1) = 0 Or (column + num2) = 7 Or (column + num2) = 0)

                    If check = True Then
                        For e = 1 To numToFlip

                            piecesFlipped.Add(New Field With {.row = row + num1Start,
                                            .column = column + num2Start})
                            num1Start += items(0, i)
                            num2Start += items(1, i)

                        Next
                    End If

                    numToFlip = 0
                End If
            Catch ex As IndexOutOfRangeException

            End Try

        Next

        Return piecesFlipped
    End Function

    Function CreateDirectionArray(ByRef items(,) As Integer) As Array

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

    Function PlayerMove(ByRef Board(,) As Char) As Array
        Dim row, column As Integer
        Dim check As Boolean
        Dim items(1, 7) As Integer



        Do
            Console.WriteLine("Enter row")
            Console.Write("> ")
            row = Console.ReadLine()
            Console.WriteLine("Enter column")
            Console.Write("> ")
            column = Console.ReadLine()


            CreateDirectionArray(items)

            If (CheckMove(Board, row, column, items, "W")).Count > 0 Then
                Board(row, column) = "W"
                FlipPieces(CheckMove(Board, row, column, items, "W"), Board, "W")
                check = True
            Else
                Console.WriteLine("That is an invalid move, please try again.")
            End If
        Loop Until check = True

        Return Board
    End Function

    Function SetUpBoard(ByRef Board(,) As Char) As Array

        Dim row, column As Integer

        For row = 0 To 7
            For column = 0 To 7
                If (row = 3 And column = 3) Or (row = 4 And column = 4) Then
                    Board(row, column) = "W"
                ElseIf (row = 3 And column = 4) Or (row = 4 And column = 3) Then
                    Board(row, column) = "B"
                Else
                    Board(row, column) = "-"
                End If
            Next
        Next

        Return Board
    End Function

    Sub PrintBoard(ByRef Board(,) As Char)
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

                If Board(row, column) = "W" Then
                    If column = 0 Then
                        Console.Write(row & " _W_|")
                    Else
                        Console.Write("_W_|")
                    End If
                ElseIf Board(row, column) = "B" Then
                    If column = 0 Then
                        Console.Write(row & " _B_|")
                    Else
                        Console.Write("_B_|")
                    End If
                ElseIf Board(row, column) = "-" Then
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

    Function PlayGame(ByRef Board(,) As Char)
        Dim numPiecesPlayed = 0

        Console.WriteLine("Welcome to Reverse.")
        Threading.Thread.Sleep(1000)
        Console.WriteLine("The board looks like this at the start of the game:")
        Console.WriteLine()
        Threading.Thread.Sleep(1500)
        PrintBoard(Board)
        Threading.Thread.Sleep(1500)
        Console.WriteLine()
        Console.WriteLine("Your colour is white.")

        Do
            PrintBoard(PlayerMove(Board))

        Loop Until numPiecesPlayed = 60
    End Function

    Sub Main()
        Dim Board(7, 7) As Char

        SetUpBoard(Board)
        PlayGame(Board)

        Console.ReadKey()
    End Sub

End Module
