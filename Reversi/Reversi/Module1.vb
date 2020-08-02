Module Module1


    Function PlayerMove(ByRef reversi As ReversiEngine, ByRef colour As ReversiEngine.ReversiPawn) As Array
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

            reversi.CreateDirectionArray(items)

            If (reversi.CheckMove(row, column, items, colour)).Count > 0 Then
                reversi.Board(row, column) = colour
                reversi.FlipPieces(reversi.CheckMove(row, column, items, colour), colour)
                check = True
            Else
                Console.WriteLine("That is an invalid move, please try again.")
            End If
        Loop Until check = True

        Return reversi.Board
    End Function

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
        Dim numPiecesPlayed = 0
        Dim choiceMenu As Integer
        Dim colour1 As ReversiEngine.ReversiPawn
        Dim colour2 As ReversiEngine.ReversiPawn
        Dim iteration As Integer = 1
        Dim exitGame As Boolean
        Dim reversi As New ReversiEngine

        reversi.SetUpBoard()


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
                    If iteration = 1 Then
                        Console.WriteLine("Player 1's colour is white, and player 2's colour is black")
                    End If
                    Console.WriteLine()
                    Threading.Thread.Sleep(2000)

                    If iteration Mod 2 = 1 Then
                        Console.WriteLine("Player 1 (white)")
                        Threading.Thread.Sleep(1000)
                        PlayerMove(reversi, colour1)
                        reversi.PrintBoard()
                    Else
                        Console.WriteLine("Player 2 (black)")
                        Threading.Thread.Sleep(1000)
                        PlayerMove(reversi, colour2)
                        reversi.PrintBoard()
                    End If

                    iteration += 1
                Case 2

                Case 3
                    exitGame = True
            End Select



        Loop Until numPiecesPlayed = 60 Or exitGame = True
    End Function

    Sub Main()
        PlayGame()

    End Sub

End Module
