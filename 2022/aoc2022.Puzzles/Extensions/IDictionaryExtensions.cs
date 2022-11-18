using System;
using System.Collections.Generic;

namespace aoc2022.Puzzles.Extensions;

public static class IDictionaryExtensions
{
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueGenerator)
    {
        if (!dictionary.TryGetValue(key, out var value))
        {
            value = valueGenerator(key);
            dictionary.Add(key, value);
        }

        return value;
    }

    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair, out TKey key, out TValue value)
    {
        key = keyValuePair.Key;
        value = keyValuePair.Value;
    }

    public static IEnumerable<TKey> RepeatKeys<TKey, TValue>(this IDictionary<TKey, TValue> keyValuePair, Func<KeyValuePair<TKey, TValue>, int, bool> countFunc)
    where TValue : struct 
    {
        foreach (var item in keyValuePair)
            for (int i = 0; countFunc(item, i); i++)
                yield return item.Key;
    }

}