global using LanguageExt;
using Microsoft.Extensions.DependencyInjection;

namespace Effects;

public interface IHas<A> : where A : notnull
{
    protected Dictionary<string, A> It => [];
    protected A GetIt(string key) => It[key];
}

public interface IHas<RT, A> : IHas<A> where RT : IHas<RT, A> where A : notnull
{


    static Eff<RT, A> Eff => Prelude.liftEff<RT, A>(static rt => rt.It);
}

