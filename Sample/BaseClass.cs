public abstract class BaseClass{
    readonly IMessageWriter writer;
    public BaseClass(IMessageWriter w)
    {
        this.writer = w;
    }
    public void Log(string str){
        writer.WriteMessage($"{this.GetType().ToString()} Log > {str}");
    }
}