using System;

namespace ThreeInARow
{
    class Program
    {
        static char[,] board;
        static int xPos = 0;
        static int yPos = 0;

        static int xOffset = 3;
        static int yOffset = 3;

        static char p1Char = 'O';
        static char p2Char = 'X';

        static Random r = new Random();

        static void Main(string[] args)
        {
            while (true)
            {
                GameLoop();
            }
        }

        static void GameLoop()
        {
            bool gameOver = false;
            int winner = -1;
            bool player = true;
            int moves = 0;

            // Skapa och fyll med "space"
            board = new char[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    board[x, y] = ' ';
                }
            }
            Console.Clear();            
            while (!gameOver)
            {
                DrawBoard(player);
                //Input loop
                if (player)
                {
                    Console.SetCursorPosition(xPos + xOffset, yPos + yOffset);
                    InputLoop();
                    board[xPos, yPos] = p1Char;
                }
                else
                {
                    // Find AI position
                    AIMove();
                    board[xPos, yPos] = p2Char;
                }
                moves++;

                //Draw changes
                DrawBoard(player);

                int w = CheckForWin(p1Char, p2Char);
                if (w != 0)
                {
                    gameOver = true;
                    if (w == 1) winner = 1;
                    else winner = 2;
                }
                player = !player;
                if (moves > 8) gameOver = true;
            }

            Console.Clear();

            DrawBoard(true, true);

            if (winner == 1)
            {
                Console.WriteLine("Congratulations, you won!");
            }
            else if (winner == -1)
            {
                Console.WriteLine("Draw!");
            }
            else
            {
                Console.WriteLine("You lost!");
            }

            // Pause game until input
            Console.WriteLine("Press any key to keep playing!");

            Console.ReadKey();
            
        }

        static bool CanDrawAt(int x, int y)
        {
            if (board[x, y] == ' ') return true;
            return false;
        }

        static void InputLoop()
        {
            int input = 0;
            while (true)
            {
                //Input
                input = HandleInput();
                Console.SetCursorPosition(xPos+xOffset, yPos+yOffset);
                if (input == 2)
                {
                    if (CanDrawAt(xPos, yPos)) break;
                    else
                    {
                        DrawBoard(true);
                        Console.SetCursorPosition(xPos + xOffset, yPos + yOffset);
                    }
                }
            }
        }
        static void AIMove()
        {
                bool looking = true;
                int x = 0;
                int y = 0;

                while (looking)
                {
                    x = r.Next(3);
                    y = r.Next(3);
                    if (CanDrawAt(x, y)) looking = false;
                }
                xPos = x;
                yPos = y;
        }

        static int HandleInput()
        {
            ConsoleKey c = Console.ReadKey().Key;
            if (c == ConsoleKey.LeftArrow)
            {
                xPos--;
                if (xPos < 0) xPos = 2;
                return 0;
            }
            if (c == ConsoleKey.RightArrow)
            {
                xPos++;
                if (xPos > 2) xPos = 0;
                return 0;
            }
            if (c == ConsoleKey.UpArrow)
            {
                yPos--;
                if (yPos < 0) yPos = 2;

                return 0;
            }
            if (c == ConsoleKey.DownArrow)
            {
                yPos++;
                if (yPos > 2) yPos = 0;
                return 0;
            }
            if (c  == ConsoleKey.Spacebar)
            {
                return 2;
            }
            return 1;
        }

        static int CheckForWin(char winCharacter1, char winCharacter2)
        {
            // Check y
            for (int x = 0; x < 3; x++)
            {
                int l1 = 0;
                int l2 = 0;
                for (int y = 0; y < 3; y++)
                {
                    if (board[x, y] == winCharacter1) l1++;
                    if (board[x, y] == winCharacter2) l2++;
                }
                if (l1 == 3) return 1;
                if (l2 == 3) return 2;
            }

            // Check x
            for (int y = 0; y < 3; y++)
            {
                int l1 = 0;
                int l2 = 0;
                for (int x = 0; x < 3; x++)
                {
                    if (board[x, y] == winCharacter1) l1++;
                    if (board[x, y] == winCharacter2) l2++;
                }
                if (l1 == 3) return 1;
                if (l2 == 3) return 2;
            }

            //Sideways LUp to RDown
            if (board[0, 0] == winCharacter1 &&
                board[1, 1] == winCharacter1 &&
                board[2, 2] == winCharacter1) return 1;
            //Sideways RUp to LDown
            if (board[2, 0] == winCharacter1 &&
                board[1, 1] == winCharacter1 &&
                board[0, 2] == winCharacter1) return 1;

            //Sideways LUp to RDown
            if (board[0, 0] == winCharacter2 &&
                board[1, 1] == winCharacter2 &&
                board[2, 2] == winCharacter2) return 2;
            //Sideways RUp to LDown
            if (board[2, 0] == winCharacter2 &&
                board[1, 1] == winCharacter2 &&
                board[0, 2] == winCharacter2) return 2;
            return 0;
        }

        static void DrawBoard(bool player, bool none = false)
        {
            Console.SetCursorPosition(0, 0);
            if (!none)
            {
                if (player)
                {
                    Console.WriteLine("Your turn!");
                }
                else
                {
                    Console.WriteLine("AI Turn!");
                }
            }
            else
            {
                Console.WriteLine();
            }

            //Draw border
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    Console.SetCursorPosition(xOffset-1 + x, yOffset-1 + y);
                    Console.Write('#');
                }
            }

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Console.SetCursorPosition(xOffset + x, yOffset + y);
                    Console.Write(board[x, y]);
                }
            }
            Console.SetCursorPosition(0, 0);
        }
    }
}
