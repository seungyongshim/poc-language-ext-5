using LanguageExt;
using LanguageExt.Traits;

namespace ConsoleApp1;

public interface Mappable<F>
    where F: Mappable<F>
{
    public static abstract K<F, B> Select<A, B>(K<F, A> ma, Func<A, B> f);
}

public record Lst<A>(IEnumerable<A> Value) : K<Lst, A>;

public class Lst: Mappable<Lst>
{
    public static K<Lst, B> Select<A, B>(K<Lst, A> ma, Func<A, B> f) =>
        ma switch
        {
            Lst<A> xs => new Lst<B>(xs.Value.Select(f)),
            _ => new Lst<B>([]),
        };
}

public static class MappableExtensions
{
    public static K<F, B> Select<F, A, B>(this K<F, A> fa, Func<A, B> f)
        where F : Mappable<F> =>
        F.Select(fa, f);
}
