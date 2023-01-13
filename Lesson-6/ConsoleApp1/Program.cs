
using Autofac;

var builder = new ContainerBuilder();
builder.RegisterType<SomeService>().As<ISomeService>();
IContainer container = builder.Build();


ISomeService service = container.Resolve<ISomeService>();

service.PrintHello();


public interface ISomeService
{
    void PrintHello();
}

public sealed class SomeService : ISomeService
{
    public void PrintHello()
    {
        Console.WriteLine($"Hello from {nameof(SomeService)}");
    }
}
