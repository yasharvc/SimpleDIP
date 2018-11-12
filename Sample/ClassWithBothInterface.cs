using System;

public class ClassWithBothInterface : ILogger, IMessageWriter
{
    public void Error(string text)
    {
        WriteToConsole(text,ConsoleColor.White,ConsoleColor.Red);
    }

    public void Info(string text)
    {
        WriteToConsole(text,ConsoleColor.Black,ConsoleColor.Blue);
    }

    public void Success(string text)
    {
        WriteToConsole(text,ConsoleColor.White,ConsoleColor.Green);
    }

    public void Warning(string text)
    {
        WriteToConsole(text,ConsoleColor.Blue,ConsoleColor.Yellow);
    }

    public void WriteMessage(string msg)
    {
        WriteToConsole(msg,ConsoleColor.Black,ConsoleColor.Cyan);
    }

    private void WriteToConsole(string msg, ConsoleColor background, ConsoleColor foreground)
    {
        var foregroundColor = Console.ForegroundColor;
        var backgroundColor = Console.BackgroundColor;
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;

        Console.WriteLine(msg);

        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
    }
}