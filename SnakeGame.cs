using System;

class SnakeGame
{
    const int BoardWidth = 20;
    const int BoardHeight = 10;

    char[,] board = new char[BoardHeight, BoardWidth];

    List<Position> snake = new List<Position>();

    Position fruit;

    Position direction = new Position(0, 1);

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
            else
            {
                Console.Write(' '); // Tom insida
            }
        }

        // När hela raden är klar, kontrollera om frukten ska ritas på denna rad
        if (i - 1 == fruit.Y)
        {
            Console.SetCursorPosition(fruit.X + 1, i); // Justera för kanterna
            Console.Write('X');  // Rita frukten
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

    public void MoveSnake()
    {
        Position head = snake[0];

        Position newHead = new Position(head.X + direction.X, head.Y + direction.Y);

        if (newHead.X == fruit.X && newHead.Y == fruit.Y)
        {
            snake.Insert(0, newHead);
            GenerateFruit();
        }
        else
        {
            snake.Insert(0, newHead);
            snake.RemoveAt(snake.Count - 1);
        }


    }

    public void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && direction.Y != 1)
            {
                direction = new Position(0, -1);
            }
            else if (key == ConsoleKey.DownArrow && direction.Y != -1)
            {
                direction = new Position(0, 1);
            }
            else if (key == ConsoleKey.LeftArrow && direction.X != 1)
            {
                direction = new Position(-1, 0);
            }
            else if (key == ConsoleKey.RightArrow && direction.X != -1)
            {
                direction = new Position(1, 0);
            }
        }
    }

    public bool CheckCollision()
    {
        Position head = snake[0];

        // Kolla om huvudet träffar väggarna
        if (head.X < 0 || head.X >= BoardWidth || head.Y < 0 || head.Y >= BoardHeight)
        {
            return true;
        }

        // Kolla om huvudet träffar någon del av kroppen (utom det första segmentet, som är huvudet)
        for (int i = 1; i < snake.Count; i++)
        {
            if (snake[i].X == head.X && snake[i].Y == head.Y)
            {
                return true;
            }
        }

        return false;
    }


    public void RunGame()
    {
        while (true)
        {
            HandleInput();
            MoveSnake();
            if (CheckCollision())
            {
                System.Console.WriteLine("Game over!");
                break;
            }
            DrawBoard();
            Thread.Sleep(120);
        }
    }
}