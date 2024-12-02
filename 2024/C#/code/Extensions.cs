public static class IEnumerableExtensions
{
    public static IEnumerable<(T, T)> Window<T>(this IEnumerable<T> source)
    {
        using var iter = source.GetEnumerator();
        if (iter.MoveNext())
        {
            var current = iter.Current;
            while (iter.MoveNext())
            {
                yield return (current, iter.Current);
                current = iter.Current;
            }
        }
    }
}
