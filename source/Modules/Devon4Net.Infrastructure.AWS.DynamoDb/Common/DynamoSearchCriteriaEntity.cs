using Amazon.DynamoDBv2.DocumentModel;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Common
{
    public class DynamoSearchCriteriaEntity
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public ScanOperator Operator { get; set; }
    }
}
