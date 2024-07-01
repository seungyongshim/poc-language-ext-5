using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp2;

public interface IHas
{
    protected IServiceProvider ServiceProvider { get; }
}

public interface IKey
{
    public static abstract string UUID { get; }
}

public interface IHas<RT, K, T> : IHas
    where K : IKey
    where T : notnull
{
    protected T It => ServiceProvider.GetRequiredKeyedService<T>(K.UUID);
}

