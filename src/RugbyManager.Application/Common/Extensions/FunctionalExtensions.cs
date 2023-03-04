namespace RugbyManager.Application.Common.Extensions;

public static class FunctionalExtensions
{
    public static TOutput Transform<TInput, TOutput>(this TInput input, Func<TInput, TOutput> func) =>
        func(input);
}