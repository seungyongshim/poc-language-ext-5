using LanguageExt.Traits;
using static LanguageExt.Prelude;

namespace Effects;

public static class HttpClientExtensionMethod
{
    public static Eff<HttpResponseMessage> GetEff(this HttpClient httpClient, string path) =>
        from ___ in unitEff
        let f1 = use(liftIO(() => httpClient.GetAsync(path))).As()
        from res in f1.RetryWhile(Schedule.recurs(5), err => err.ToException() switch
        {
            HttpRequestException => true,
            _ => false
        }).RepeatWhile(res => (int)res.StatusCode switch
        {
            429 => true,
            >= 500 => true,
            _ => false
        })
        select res;
}

public class HttpClient<T> : HttpClient
{

}
