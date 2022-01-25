using System;
using Amazon.CDK;
using Constructs;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers
{
    public class TagHandler
    {
        public void LogTag(string name, IConstruct result)
        {
            Tags.Of(result).Add("Name", name.ToLowerInvariant());
            Console.WriteLine($"Entity name: {name}");
        }

        /// <summary>
        /// Add custom Tags to the CFN resource
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        public void AddCustomTag(string key, string value, IConstruct result)
        {
            Tags.Of(result).Add(key, value);
        }
    }
}
