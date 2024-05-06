global using LanguageExt;
using Microsoft.Extensions.DependencyInjection;

namespace Effects;

public interface IHas
{
    protected IServiceProvider ServiceProvider { get; }
}

public interface IHas<A> : IHas where A: notnull
{
    protected A It => ServiceProvider.GetRequiredService<A>();
}

public interface IHas<RT, A> : IHas<A> where RT : IHas<RT, A> where A : notnull
{
    static Eff<RT, A> Eff => Prelude.liftEff<RT, A>(static rt => rt.It);
}
