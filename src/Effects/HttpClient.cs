using LanguageExt.Traits;

namespace Effects;

public static class HttpClient<RT> where RT : IHas<RT, IReadOnlyDictionary<string, HttpClient>>
{
    public static Eff<RT, HttpClient> Create() => IHas<RT, HttpClient>.Eff;
}


public static class HttpClientFactory<RT> where RT : IHas<RT, IHttpClientFactory>
{
    public static Eff<RT, HttpClient> Create(string name) =>
        from _1 in IHas<RT, IHttpClientFactory>.Eff
        from _2 in Resource.use<Eff, HttpClient>(eio => _1.CreateClient(name)).As()
        select _2;
}
