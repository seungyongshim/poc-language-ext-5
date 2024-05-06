using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;
namespace Effects;

public static class ServiceProviderEffect
{
    public static Eff<IServiceProvider, T> GetServiceEff<T>() where T : notnull   => liftEff<IServiceProvider, T>(static sp => sp.GetRequiredService<T>());
    public static Eff<IServiceProvider, ILogger> GetLoggerEff(string name)        => liftEff<IServiceProvider, ILogger>(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger(name));
    public static Eff<IServiceProvider, HttpClient> GetHttpClientEff(string name) => liftEff<IServiceProvider, HttpClient>(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(name));
}
