namespace ECommerceBack.Common.Extensions;

public static class StringExtensions
{
    public static bool EndsWithAny(this string input, params string[] values) => values.Any(input.EndsWith);
}
