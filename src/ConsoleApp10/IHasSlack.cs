using LanguageExt.Traits;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApp10;



public interface IHasLogger<RT, TKey> : IHas<RT, TKey, ILogger>
    where RT : IHas<RT, TKey, ILogger>
    where TKey : IKey
{
    Func<string, ILogger> IHas<RT, TKey, ILogger>.HowToGetIt => ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger;
}

public interface IHasHttp<RT, TKey> : IHas<RT, TKey, HttpClient>
    where RT : IHas<RT, TKey, HttpClient>
    where TKey : IKey
{
    Func<string, HttpClient> IHas<RT, TKey, HttpClient>.HowToGetIt => ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient;
}

public interface IHasSlack<RT, TKey> : IHasHttp<RT, TKey>
    where RT : IHasHttp<RT, TKey>
    where TKey : IKey
{
}

public interface IHasSlack<RT> : IKey, IHasHttp<RT, IHasSlack<RT>>
    where RT : IHasHttp<RT, IHasSlack<RT>>
{
    static string IKey.UUID => "IHasSlack<RT>";
}

public static class HasSlackExtension
{
    public static IServiceCollection AddHasSlack(this IServiceCollection services)
    {
        services.AddHttpClient("IHasSlack<RT>", httpclient =>
        {
            httpclient.BaseAddress = new Uri("https://slack.com/api/");
        });
        return services;
    }
}
