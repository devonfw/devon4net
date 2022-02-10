using Amazon.DynamoDBv2.DocumentModel;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Common
{
    public class DynamoQueryCriteriaEntity
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public object SecondValue { get; set; }
        public QueryOperator QueryOperator { get; set; }
    }
}
