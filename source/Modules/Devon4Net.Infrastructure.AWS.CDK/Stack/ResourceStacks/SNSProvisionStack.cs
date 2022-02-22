using Amazon.CDK.AWS.Events.Targets;
using Amazon.CDK.AWS.SNS;
using Devon4Net.Infrastructure.AWS.CDK.Options.Global;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack
{
    public partial class ProvisionStack
    {
        public void CreateSns()
        {
            CreateSnsTopics();
            CreateSnsEmailSubscriptions();
        }

        private void CreateSnsEmailSubscriptions()
        {
            if (CdkOptions == null || CdkOptions.SnsEmailSubscriptions?.Any() != true) return;

            foreach (var subscriptionConfig in CdkOptions.SnsEmailSubscriptions)
            {
                var emailSubscription = AwsCdkHandler.CreateEmailSubscription(subscriptionConfig.Email, subscriptionConfig.Json);
                foreach (var topic in subscriptionConfig.TopicIds)
                {
                    var topicLocated = LocateSnsTopic(topic, $"Could not find topic with id {subscriptionConfig.TopicIds}");
                    AwsCdkHandler.SubscribeToTopic(topicLocated, emailSubscription);
                }
            }
        }
        private void CreateSnsTopics()
        {
            if (CdkOptions == null || CdkOptions.SnsTopics?.Any() != true) return;

            StackResources.SnsTopics = new Dictionary<string, ITopic>();
            foreach (var topicConfig in CdkOptions.SnsTopics)
            {
                var topic = AwsCdkHandler.CreateSNSTopic(topicConfig.Id, topicConfig.Name, topicConfig.DisplayName, topicConfig.Fifo, topicConfig.ContentBasedDeduplication);
                StackResources.SnsTopics.Add(topicConfig.Id, topic);
            }
        }

        private ITopic LocateSnsTopic(string id, string exceptionMessageIfTopicDoesNotExist, string exceptionMessageIfTopicIsEmpty = null)
        {
            return StackResources.Locate<ITopic>(id, exceptionMessageIfTopicDoesNotExist, exceptionMessageIfTopicIsEmpty);
        }
    }
}
