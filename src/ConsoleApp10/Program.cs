using System;
using ConsoleApp10;
using LanguageExt;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static LanguageExt.Prelude;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddHasSlack();

var app = builder.Build();


var q = from http in IHasSlack<Runtime>.Eff
        from _2 in liftEff(async rt => await http.SendAsync(new()))
        select unit;

await using var scope = app.Services.CreateAsyncScope();
q.Run(new Runtime(scope.ServiceProvider), EnvIO.New());

struct Runtime
(
    IServiceProvider serviceProvider
) : IHasSlack<Runtime>
{
    static string IHasSlack<Runtime>.UUID {get;} = "";

    readonly IServiceProvider IHas.ServiceProvider => serviceProvider;
}


