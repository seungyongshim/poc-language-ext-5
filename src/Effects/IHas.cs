global using LanguageExt;
global using static LanguageExt.Prelude;
using LanguageExt.Traits;

namespace Effects;

public interface IHas<RT, TRAIT> : Has<Eff<RT>, TRAIT> where RT : IHas<RT, TRAIT>
{
    protected TRAIT It { get; }
    static Eff<RT, TRAIT> Eff => liftEff<RT, TRAIT>(static rt => rt.It);
    K<Eff<RT>, TRAIT> Has<Eff<RT>, TRAIT>.Trait => Eff;
}
