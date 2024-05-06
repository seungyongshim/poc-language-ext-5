using LanguageExt.Traits;

namespace Effects;

public static class HttpClient<RT> where RT : IHas<RT, HttpClient>
{
    public static Eff<RT, HttpClient> Create(string key = nameof(HttpClient)) => IHas<RT, HttpClient>.Eff(key);
}
