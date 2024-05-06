global using LanguageExt;
using LanguageExt.Traits;

namespace Effects;


public interface IHas<A>
{
    protected A It { get; }
}

public interface IHas<RT, A> : IHas<A> where RT : IHas<RT, A>
{
    static Eff<RT, A> Eff => Prelude.liftEff<RT, A>(rt => rt.It);
}
