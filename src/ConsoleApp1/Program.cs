using static LanguageExt.Prelude;
using LanguageExt;


var q = from _3 in liftEff(() => throw new NotImplementedException())
        from _1 in liftIO(Console.In.ReadLineAsync)
        from _2 in liftIO(() => Console.Out.WriteLineAsync(_1))
        from _4 in lift(Console.In.ReadLine)
        select _1;

try
{
    q.Run().ThrowIfFail();
}
catch(Exception ex)
{
    Console.WriteLine($"catch {ex}");
}
