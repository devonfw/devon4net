using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.Kafka.Common.Extensions
{
    public static class ListExtensions
    {
        public static T PopOrDefault<T>(this IList<T> list, int index = 0)
        {
            
            if (index < 0 || list.Count <= 0) return default;
            T value = list[index];
            list.RemoveAt(index);
            return value;
            
        }
    }
}
