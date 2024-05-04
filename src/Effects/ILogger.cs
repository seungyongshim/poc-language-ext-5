using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;
namespace Effects;

public static class Logger<RT> where RT : IHas<RT, ILogger>
{
    public static Eff<RT, Unit> Debug(string? message, params object?[] args) =>
        from logger in IHas<RT, ILogger>.Eff
        from _1 in liftEff(() => logger.LogDebug(message, args))
        select unit;

    public static Eff<RT, Unit> Info(string? message, params object?[] args) =>
        from logger in IHas<RT, ILogger>.Eff
        from _1 in liftEff(() => logger.LogInformation(message, args))
        select unit;

    public static Eff<RT, T> IfError<T>(Eff<RT, T> eff) => eff | @catchM(err => true, err => from logger in IHas<RT, ILogger>.Eff
             from _1 in liftEff(() => logger.LogError(err.ToException(),""))
             from _2 in FailEff<T>(err)
             select _2);
}

public static class LoggerExtensionMethod
{
    public static Eff<RT, T> IfLogError<RT, T>(this Eff<RT, T> eff)
        where RT : IHas<RT, ILogger> => Logger<RT>.IfError(eff);
    
}
