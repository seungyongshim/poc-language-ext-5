using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Traits;
using Microsoft.Extensions.DependencyInjection;
using static LanguageExt.Prelude;

namespace ConsoleApp10;



public interface IKey
{
    public static abstract string UUID { get; }
}

public interface IHas
{
    protected IServiceProvider ServiceProvider { get; }
}

public interface IHas<RT, K, T> : IHas
    where RT : IHas<RT, K, T>
    where K : IKey
    where T : notnull
{
    protected Func<string, T> HowToGetIt => ServiceProvider.GetRequiredKeyedService<T>;
    protected T It => HowToGetIt(K.UUID);
    public static Eff<RT, T> Eff => liftEff<RT, T>(static rt => rt.It);
}

