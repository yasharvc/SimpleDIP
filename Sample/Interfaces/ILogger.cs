public interface ILogger
{
    void Info(string text);
    void Warning(string text);
    void Success(string text);
    void Error(string text);
}