using System;

class SnakeGame
{
    const int BoardWidth = 20;
    const int BoardHeight = 10;

    char[,] board = new char[BoardHeight, BoardWidth];

    List<Position> snake = new List<Position>();

    Position fruit;

    public SnakeGame()
    {
        snake.Add(new Position(5, 5));

        for (int i = 4; i >= 0; i--)
        {
            snake.Add(new Position(i, 5));
        }

        GenerateFruit();

        ClearBoard();
    }

    public void ClearBoard()
    {
        for (int i = 0; i < BoardHeight; i++)
        {
            for (int j = 0; j < BoardWidth; j++)
            {
                board[i, j] = ' ';
            }
        }
    }
    public void DrawBoard()
    {
        Console.Clear();

        for (int i = 0; i < BoardHeight + 2; i++)
        {
            for (int j = 0; j < BoardWidth + 2; j++) 
            {
                if (i == 0 || i == BoardHeight + 1 || j == 0 || j == BoardWidth + 1)
                {
                    Console.Write('#'); // Rita kanterna
                }
                else if (IsSnake(i - 1, j - 1)) // Justera för att matcha insidan
                {
                    Console.Write('O'); // Rita ormen
                }
                else if (i - 1 == fruit.Y && j - 1 == fruit.X) // Rita frukten
                {
                    Console.Write('X');
                }
                else
                {
                    Console.Write(' '); // Tom insida
                }
            }
            Console.WriteLine();
        }
    }


    public bool IsSnake(int y, int x)
    {
        foreach (var segment in snake)
        {
            if (segment.Y == y && segment.X == x)
            {
                return true;
            }
        }

        return false;
    }

    public void GenerateFruit()
    {
        Random rnd = new Random();
        fruit = new Position(rnd.Next(0, BoardWidth), rnd.Next(0, BoardHeight));
    }
}