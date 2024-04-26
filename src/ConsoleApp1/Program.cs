using System.Diagnostics;
using ConsoleApp1;
using LanguageExt;
using LanguageExt.Sys.Traits;
using LanguageExt.Traits;
using static LanguageExt.Prelude;

Eff<Runtime, Unit> eff = unitEff;
eff.Run(env, EnvIO.New());


public record Runtime : Has<Eff<Runtime>, ActivitySourceIO>,
{
    public K<Eff<Runtime>, ActivitySourceIO> Trait { get; }
}
