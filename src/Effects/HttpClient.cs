using LanguageExt.Traits;
using static LanguageExt.Prelude;

namespace Effects;

public static class HttpClientExtensionMethod
{
    public static Eff<HttpResponseMessage> GetEff(this HttpClient httpClient, string path) =>
        from ___ in unitEff
        let f1 = from res in liftEff(() => httpClient.GetAsync(path).ToValue())
                 from _2 in Resource.use<Eff, HttpResponseMessage>(() => res)
                 select res
        from res in f1.RetryUntil(Schedule.recurs(5), err => err.ToException() switch
        {
            HttpRequestException => true,
            _ => false
        }).RepeatUntil(res => (int)res.StatusCode switch
        {
            429 => true,
            >= 500 => true,
            _ => false
        })
        select res;
}
