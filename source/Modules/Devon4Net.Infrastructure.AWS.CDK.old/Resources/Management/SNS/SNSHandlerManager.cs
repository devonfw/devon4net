using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.SNS.Subscriptions;
using Amazon.CDK.AWS.SNS;
using Amazon.CDK.AWS.SQS;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.SNS
{
    public partial class AwsCdkHandlerManager
    {
        public EmailSubscription CreateEmailSubscription(string emailAddress, bool? json = null, IDictionary<string, SubscriptionFilter> filterPolicy = null, IQueue queue = null)
        {
            return HandlerResources.AwsCdkSnsHandler.CreateEmailSubscription(emailAddress, json, filterPolicy, queue);
        }

        public ITopic CreateSNSTopic(string id, string name, string displayName, bool? fifo = null, bool? contentBasedDeduplication = null, IKey masterKey = null)
        {
            return HandlerResources.AwsCdkSnsHandler.CreateSNSTopic(id, name, displayName, fifo, contentBasedDeduplication, masterKey);
        }

        public void SubscribeToTopic(ITopic topic, ITopicSubscription subscription)
        {
            HandlerResources.AwsCdkSnsHandler.SubscribeToTopic(topic, subscription);
        }

        public void SubscribeToTopic(ITopic topic, IList<ITopicSubscription> subscriptions)
        {
            HandlerResources.AwsCdkSnsHandler.SubscribeToTopic(topic, subscriptions);
        }
    }
}
