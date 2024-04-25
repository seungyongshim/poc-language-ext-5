using LanguageExt;
using static LanguageExt.Prelude;

Console.WriteLine("Started");


var q = from _1 in liftEff(async () => await Console.In.ReadLineAsync())
        from _2 in liftEff(async () => await Console.Out.WriteLineAsync(_1).ToUnit())
        select _1;

var ret = q.Run();



