
using System.Globalization;
using System.Numerics;

public static class StringExtensions
{
    public static T[] ParseNumbers<T>(this string line, char separator, int start = 0) where T : INumber<T> =>
        line[start..].Split(separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => T.Parse(s, new NumberFormatInfo())).ToArray();
}