using System.Collections.Concurrent;

namespace Devon4Net.Infrastructure.AWS.Lambda.Extensions
{
    public static class EnumerableExtension
    {
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int maxDegreeOfParallelism, Func<T, Task> body)
        {
            return Task.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(maxDegreeOfParallelism)
                select Task.Run(async delegate
                {
                    using (partition)
                        while (partition.MoveNext())
                            await body(partition.Current).ConfigureAwait(false);
                }));
        }
    }
}
