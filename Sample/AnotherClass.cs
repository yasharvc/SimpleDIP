using System;

public class AnotherClass : IMessageWriter
{
    public void WriteMessage(string msg)
    {
        Console.WriteLine($"{nameof(AnotherClass)} Message : {msg}");
    }
}