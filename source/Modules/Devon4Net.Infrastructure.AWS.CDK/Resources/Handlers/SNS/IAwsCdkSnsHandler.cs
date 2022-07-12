using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.SNS;
using Amazon.CDK.AWS.SNS.Subscriptions;
using Amazon.CDK.AWS.SQS;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.SNS
{
    public interface IAwsCdkSnsHandler
    {
        EmailSubscription CreateEmailSubscription(string emailAddress, bool? json, IDictionary<string, SubscriptionFilter> filterPolicy, IQueue queue);
        ITopic CreateSNSTopic(string id, string name, string displayName, bool? fifo, bool? contentBasedDeduplication, IKey masterKey);
        void SubscribeToTopic(ITopic topic, ITopicSubscription subscription);
        void SubscribeToTopic(ITopic topic, IEnumerable<ITopicSubscription> subscriptions);
    }
}