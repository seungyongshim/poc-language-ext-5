global using LanguageExt;
using LanguageExt.Traits;

namespace Effects;

public interface IHas<RT, A> : Has<Eff<RT>, A> where RT : IHas<RT, A>
{
    protected A It { get; }
    static Eff<RT, A> Eff => StateM.gets<Eff<RT>, RT, A>(static rt => rt.It).As();
    K<Eff<RT>, A> Has<Eff<RT>, A>.Trait => StateM.gets<Eff<RT>, RT, A>(static rt => rt.It);
}
