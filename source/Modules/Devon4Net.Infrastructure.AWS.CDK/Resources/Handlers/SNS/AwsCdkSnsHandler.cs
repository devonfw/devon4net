using Amazon.CDK;
using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.SNS;
using Amazon.CDK.AWS.SNS.Subscriptions;
using Amazon.CDK.AWS.SQS;
using Constructs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.SNS
{
    public class AwsCdkSnsHandler : AwsCdkBaseHandler
    {
        public AwsCdkSnsHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public ITopic CreateSNSTopic(string id, string name, string displayName, bool? fifo, bool? contentBasedDeduplication, IKey masterKey)
        {
            return new Topic(Scope, id, new TopicProps
            {
                DisplayName = displayName,
                Fifo = fifo,
                TopicName = name,
                MasterKey = masterKey,
                ContentBasedDeduplication = contentBasedDeduplication
            });
        }

        public EmailSubscription CreateEmailSubscription(string emailAddress, bool? json, IDictionary<string, SubscriptionFilter> filterPolicy, IQueue queue)
        {
            return new EmailSubscription(emailAddress, new EmailSubscriptionProps
            {
                Json = json,
                DeadLetterQueue = queue,
                FilterPolicy = filterPolicy
            });
        }

        public void SubscribeToTopic(ITopic topic, ITopicSubscription subscription)
        {
            topic.AddSubscription(subscription);
        }

        public void SubscribeToTopic(ITopic topic, IEnumerable<ITopicSubscription> subscriptions)
        {
            foreach(var subscription in subscriptions)
            {
                topic.AddSubscription(subscription);
            }
        }
    }
}
