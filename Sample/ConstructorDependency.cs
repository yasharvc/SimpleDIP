public class ConstructorDependency : BaseClass
{
    readonly ILogger logger;
    public ConstructorDependency(ILogger logger, IMessageWriter writer) : base(writer)
    {
        this.logger = logger;
    }

    public void logIt(string str)
    {
        logger.Success(str);
    }
}