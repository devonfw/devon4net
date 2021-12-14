using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Common
{
    public class DynamoSearchCriteria
    {
        private List<DynamoSearchCriteriaEntity> SearchCriteria { get; set; }

        public DynamoSearchCriteria()
        {
            SearchCriteria = new List<DynamoSearchCriteriaEntity>();
        }

        public void AddCriteria(string propertyName, object value, ScanOperator criteriaOperator = ScanOperator.Equal)
        {
            SearchCriteria.Add(new DynamoSearchCriteriaEntity { PropertyName = propertyName, Value = value, Operator = criteriaOperator });
        }

        public void ClearCriteria()
        {
            SearchCriteria = new List<DynamoSearchCriteriaEntity>();
        }

        public List<DynamoSearchCriteriaEntity> GetSearchCriteriaList()
        {
            return SearchCriteria;
        }

        public List<ScanCondition> GetCriteriaScanConditions()
        {
            if (SearchCriteria == null || SearchCriteria.Count == 0) return new List<ScanCondition>();

            var result = new List<ScanCondition>();

            foreach (var item in SearchCriteria)
            {
                result.Add(new ScanCondition(item.PropertyName, ScanOperator.Equal, item.Value));
            }

            return result;
        }
    }
}