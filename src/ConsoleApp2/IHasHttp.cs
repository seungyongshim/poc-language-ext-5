using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt.Traits;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp2;

public interface IHasHttp<RT, K> : IHas<RT, K, HttpClient> where K : IKey
{
    HttpClient IHas<K, HttpClient>.It => ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient(K.UUID);
}

public interface IHasSlack<RT> : IKey, IHasHttp<RT, IHasSlack<RT>>
{
    static string IKey.UUID => "IHasSlack<RT>";

    public static Eff<RT, Unit> Hello)
}

public static class HasSlackExtension
{
    public static IServiceCollection AddHasSlack(this IServiceCollection services)
    {
        services.AddHttpClient("IHasSlack<RT>");
        return services;
    }
}
