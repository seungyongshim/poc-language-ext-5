using static LanguageExt.Prelude;
using LanguageExt;

Console.WriteLine("Started");


var q = from _1 in liftEff(async () => await Console.In.ReadLineAsync())
        from _2 in liftEff(async () => await Console.Out.WriteLineAsync(_1).ToUnit())
        select _1;

try
{
    q.Run().ThrowIfFail();
}
catch(Exception ex)
{
    Console.WriteLine($"catch {ex}");
}
