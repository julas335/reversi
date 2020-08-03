Module Module1


    Function PlayerMove(ByRef reversi As ReversiEngine, ByRef colour As ReversiEngine.ReversiPawn, ByRef items(,) As Integer, ByRef TestBoard(,) As ReversiEngine.ReversiPawn, ByVal botCheck As Boolean) As Array
        Dim row, column As Integer
        Dim check As Boolean



        Do
            Console.WriteLine("Enter row")
            Console.Write("> ")
            row = Console.ReadLine()
            Console.WriteLine("Enter column")
            Console.Write("> ")
            column = Console.ReadLine()


            If (reversi.CheckMove(row, column, items, colour)).Count > 0 Then
                reversi.Board(row, column) = colour
                reversi.FlipPieces(reversi.CheckMove(row, column, items, colour), colour, TestBoard, botCheck)
                check = True
            Else
                Console.WriteLine("That is an invalid move, please try again.")
            End If
        Loop Until check = True

        Return reversi.Board
    End Function

    Function GetMaxWhiteFlipped(ByRef reversi As ReversiEngine, ByRef items(,) As Integer, ByRef TestBoard(,) As ReversiEngine.ReversiPawn) As Integer
        Dim currentMax As Integer
        Dim currentCompare As Integer
        Dim iteration As Integer = 1

        For row = 0 To 7

            For column = 0 To 7
                If TestBoard(row, column) = reversi.ReversiPawn.empty Then

                    If reversi.CheckMove(row, column, items, reversi.ReversiPawn.white).Count > 0 Then
                        If iteration = 1 Then
                            currentMax = reversi.CheckMove(row, column, items, reversi.ReversiPawn.white).Count
                        Else
                            currentCompare = reversi.CheckMove(row, column, items, reversi.ReversiPawn.white).Count
                        End If

                        If currentMax < currentCompare Then
                            currentMax = currentCompare
                        End If

                    End If
                End If
            Next

        Next

        SetUpTestBoard(reversi, TestBoard)


        Return currentMax
    End Function

    Function BotMove(ByRef reversi As ReversiEngine, ByRef colour As ReversiEngine.ReversiPawn, ByRef items(,) As Integer, ByRef TestBoard(,) As ReversiEngine.ReversiPawn, ByVal botCheck As Boolean)
        Dim row, column As Integer
        Dim botFlipped As Integer
        Dim currentCompare As Integer
        Dim total As Integer
        Dim bestRow, bestColumn As Integer
        Dim iteration As Integer = 1

        For row = 0 To 7

            For column = 0 To 7
                If reversi.Board(row, column) = reversi.ReversiPawn.empty Then

                    If reversi.CheckMove(row, column, items, colour).Count > 0 Then
                        botCheck = True
                        reversi.FlipPieces(reversi.CheckMove(row, column, items, colour), colour, TestBoard, botCheck)
                        botFlipped = reversi.CheckMove(row, column, items, colour).Count

                        If iteration = 1 Then
                            total = botFlipped - GetMaxWhiteFlipped(reversi, items, TestBoard)
                            bestRow = row
                            bestColumn = column
                        Else
                            currentCompare = botFlipped - GetMaxWhiteFlipped(reversi, items, TestBoard)
                        End If

                        If total < currentCompare Then
                            total = currentCompare
                            bestRow = row
                            bestColumn = column
                        End If

                    End If

                End If

            Next

        Next

        botCheck = False

        reversi.Board(bestRow, bestColumn) = colour
        reversi.FlipPieces(reversi.CheckMove(bestRow, bestColumn, items, colour), colour, TestBoard, botCheck)

    End Function

    Sub SetUpTestBoard(ByRef reversi As ReversiEngine, ByRef TestBoard(,) As ReversiEngine.ReversiPawn)
        For row = 0 To 7
            For column = 0 To 7
                TestBoard(row, column) = reversi.Board(row, column)
            Next
        Next
    End Sub

    Function DisplayMenu()
        Dim choice As Integer

        Console.WriteLine("1. Player against player (2 player)")
        Console.WriteLine("2. Player against bot (1 player)")
        Console.WriteLine("3. Exit")
        Console.WriteLine()

        Console.Write("> ")
        choice = Console.ReadLine()
        Return choice
    End Function

    Function PlayGame()
        Dim numIterations
        Dim choiceMenu As Integer
        Dim colour1 As ReversiEngine.ReversiPawn
        Dim colour2 As ReversiEngine.ReversiPawn
        Dim iteration As Integer = 1
        Dim exitGame As Boolean
        Dim reversi As New ReversiEngine
        Dim TestBoard(7, 7) As ReversiEngine.ReversiPawn
        Dim items(1, 7) As Integer
        Dim botCheck As Boolean

        reversi.SetUpBoard()
        reversi.CreateDirectionArray(items)


        colour1 = reversi.ReversiPawn.white
        colour2 = reversi.ReversiPawn.black

        Console.WriteLine("Welcome to Reversi")
        Console.WriteLine()
        Threading.Thread.Sleep(1000)
        Console.WriteLine("Choose an option:")
        Threading.Thread.Sleep(1500)
        choiceMenu = DisplayMenu()
        If choiceMenu <> 3 Then
            Console.WriteLine("The board looks like this at the start of the game:")
            Console.WriteLine()
            Threading.Thread.Sleep(1500)
            reversi.PrintBoard()
            Threading.Thread.Sleep(1500)
            Console.WriteLine()
        End If

        Do
            Select Case choiceMenu

                Case 1
                    If numIterations = 0 Then
                        Console.WriteLine("Player 1's colour is white, and player 2's colour is black")
                    End If
                    Console.WriteLine()
                    Threading.Thread.Sleep(2000)

                    Console.WriteLine("Player 1 (white)")
                    Threading.Thread.Sleep(1000)
                    PlayerMove(reversi, colour1, items, TestBoard, botCheck)
                    reversi.PrintBoard()
                    Console.WriteLine("Player 2 (black)")
                    Threading.Thread.Sleep(1000)
                    PlayerMove(reversi, colour2, items, TestBoard, botCheck)
                    reversi.PrintBoard()
                Case 2
                    If numIterations = 0 Then
                        Console.WriteLine("Player 1's colour is white, and bots' colour is black")
                    End If
                    Console.WriteLine()
                    Threading.Thread.Sleep(2000)



                    Console.WriteLine("Player 1 (white)")
                    Threading.Thread.Sleep(1000)
                    PlayerMove(reversi, colour1, items, TestBoard, botCheck)
                    reversi.PrintBoard()
                    SetUpTestBoard(reversi, TestBoard)
                    Console.WriteLine()
                    Console.WriteLine("Bot (black)")
                    Console.WriteLine()

                    Threading.Thread.Sleep(500)
                    BotMove(reversi, colour2, items, TestBoard, botCheck)
                    reversi.PrintBoard()
                Case 3
                    exitGame = True
            End Select

            numIterations += 1

        Loop Until numIterations = 30 Or exitGame = True
    End Function

    Sub Main()
        PlayGame()

    End Sub

End Module
