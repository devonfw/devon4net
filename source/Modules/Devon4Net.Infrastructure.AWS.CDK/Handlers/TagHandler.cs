using System;
using Amazon.CDK;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class TagHandler
    {
        public void LogTag(string name, IConstruct result)
        {
            Tags.Of(result).Add("Name", name.ToLowerInvariant());
            Console.WriteLine($"Entity name: {name}");
        }
    }
}
