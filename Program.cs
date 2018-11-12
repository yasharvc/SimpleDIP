using System;

namespace SimpleDIP
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleDIP.Register<IMessageWriter,ClassWithBothInterface>();

            IMessageWriter msgWriter = SimpleDIP.Resolve<IMessageWriter>();
            //Magic happens here!!!
            ILogger logger = SimpleDIP.Resolve<ILogger>();
            Console.WriteLine($"IMessageWriter.WriteMessage :");
            msgWriter.WriteMessage("WriteMessage Test");
            Console.WriteLine();
            Console.WriteLine($"ILogger.Warning :");
            logger.Warning("Warning Test!");

            //Change IMessageWriter handler to AnotherClass
            SimpleDIP.Register<AnotherClass>();
            
            msgWriter = SimpleDIP.Resolve<IMessageWriter>();
            Console.WriteLine($"IMessageWriter.WriteMessage :");
            msgWriter.WriteMessage("Second Message Test!");

            SimpleDIP.CreateInstance<ConstructorDependency>().logIt("Salam");
        }
    }
}
