using LanguageExt.Traits;

namespace Effects;

public static class HttpClient<RT> where RT : IHas<RT, HttpClient>
{
    public static Eff<RT, HttpClient> Create() =>
        from _1 in IHas<RT, HttpClient>.Eff
        from _2 in Resource.use<Eff, HttpClient>(eio => _1).As()
        select _2;
}
