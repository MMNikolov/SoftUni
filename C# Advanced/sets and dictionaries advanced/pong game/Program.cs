using System;
using System.Threading;

class Program
{
    static int windowHeight = 20;
    static int windowWidth = 40;
    static int paddleWidth = 3;
    static int paddleHeight = 3;
    static int paddle1X = 1;
    static int paddle1Y = windowHeight / 2 - paddleHeight / 2;
    static int paddle2X = windowWidth - paddleWidth - 1;
    static int paddle2Y = windowHeight / 2 - paddleHeight / 2;
    static int ballX = windowWidth / 2;
    static int ballY = windowHeight / 2;
    static int ballSpeedX = -1;
    static int ballSpeedY = 1;

    static void DrawPaddle(int x, int y)
    {
        for (int i = 0; i < paddleHeight; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write("|");
        }
    }

    static void DrawBall(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write("O");
    }

    static void ClearPaddle(int x, int y)
    {
        for (int i = 0; i < paddleHeight; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(" ");
        }
    }

    static void ClearBall(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(" ");
    }

    static void Main(string[] args)
    {
        Console.WindowHeight = windowHeight;
        Console.WindowWidth = windowWidth;
        Console.BufferHeight = windowHeight;
        Console.BufferWidth = windowWidth;

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow && paddle2Y > 0)
                {
                    ClearPaddle(paddle2X, paddle2Y);
                    paddle2Y--;
                }
                if (key.Key == ConsoleKey.DownArrow && paddle2Y + paddleHeight < windowHeight)
                {
                    ClearPaddle(paddle2X, paddle2Y);
                    paddle2Y++;
                }
            }

            ClearPaddle(paddle1X, paddle1Y);
            ClearPaddle(paddle2X, paddle2Y);
            ClearBall(ballX, ballY);

            if (ballX == paddle1X + 1 && ballY >= paddle1Y && ballY < paddle1Y + paddleHeight)
                ballSpeedX = 1;

            if (ballX == paddle2X - 1 && ballY >= paddle2Y && ballY < paddle2Y + paddleHeight)
                ballSpeedX = -1;

            if (ballX == 0 || ballX == windowWidth - 1)
                ballSpeedX *= -1;

            if (ballY == 0 || ballY == windowHeight - 1)
                ballSpeedY *= -1;

            ballX += ballSpeedX;
            ballY += ballSpeedY;

            DrawPaddle(paddle1X, paddle1Y);
            DrawPaddle(paddle2X, paddle2Y);
            DrawBall(ballX, ballY);

            Thread.Sleep(20);
        }
    }
}
