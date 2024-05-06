using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;
namespace Effects;

public static class LoggerExtensionsMethod
{
    public static Eff<Unit> DebugEff(this ILogger logger, string? message, params object?[] args) =>
        from _1 in liftEff(() => logger.LogDebug(message, args))
        select unit;

    public static Eff<Unit> InfoEff(this ILogger logger, string? message, params object?[] args) =>
        from _1 in liftEff(() => logger.LogInformation(message, args))
        select unit;

    public static Eff<T> IfErrorEff<T>(this ILogger logger, Eff<T> eff) => eff | @catchM(err => true, err =>
             from _1 in liftEff(() => logger.LogError(err.ToException(), ""))
             from _2 in FailEff<T>(err)
             select _2);
}
