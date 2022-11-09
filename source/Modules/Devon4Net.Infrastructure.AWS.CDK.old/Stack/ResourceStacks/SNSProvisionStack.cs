using Amazon.CDK.AWS.SNS;
using Devon4Net.Infrastructure.AWS.CDK.Options.Global;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
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

            foreach (var subscriptionOption in CdkOptions.SnsEmailSubscriptions)
            {
                var emailSubscription = AwsCdkHandler.CreateEmailSubscription(subscriptionOption.Email, subscriptionOption.Json);
                foreach (var topicId in subscriptionOption.TopicIds)
                {
                    var topic = LocateSnsTopic(topicId, $"Could not find topicId with id {subscriptionOption.TopicIds}");
                    AwsCdkHandler.SubscribeToTopic(topic, emailSubscription);
                }
            }
        }

        private void CreateSnsTopics()
        {
            if (CdkOptions == null || CdkOptions.SnsTopics?.Any() != true) return;

            foreach (var topicOption in CdkOptions.SnsTopics)
            {
                var topic = AwsCdkHandler.CreateSNSTopic(topicOption.Id, topicOption.Name, topicOption.DisplayName, topicOption.Fifo, topicOption.ContentBasedDeduplication);
                StackResources.SnsTopics.Add(topicOption.Id, topic);
            }
        }

        private ITopic LocateSnsTopic(string id, string exceptionMessageIfTopicDoesNotExist, string exceptionMessageIfTopicIsEmpty = null)
        {
            return StackResources.Locate<ITopic>(id, exceptionMessageIfTopicDoesNotExist, exceptionMessageIfTopicIsEmpty);
        }
    }
}
