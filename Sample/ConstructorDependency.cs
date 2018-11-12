public class ConstructorDependency
{
    readonly ILogger logger;
    public ConstructorDependency(ILogger logger)
    {
        this.logger = logger;
    }
    
    public void logIt(string str){
        logger.Success(str);
    }
}