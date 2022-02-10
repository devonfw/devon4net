using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Common
{
    public class DynamoSearchCriteria
    {
        private List<DynamoScanSearchCriteriaEntity> SearchCriteria { get; set; }
        private List<DynamoQueryCriteriaEntity> QueryCriteria { get; set; }
        

        public DynamoSearchCriteria()
        {
            SearchCriteria = new List<DynamoScanSearchCriteriaEntity>();
            QueryCriteria = new List<DynamoQueryCriteriaEntity>();
        }

        public void AddSearchCriteria(string propertyName, object value, ScanOperator criteriaOperator = ScanOperator.Equal)
        {
            SearchCriteria.Add(new DynamoScanSearchCriteriaEntity { PropertyName = propertyName, Value = value, ScanOperator= criteriaOperator });
        }

        public void AddQueryCriteria(string propertyName, object value, QueryOperator criteriaOperator = QueryOperator.Equal, object secondValue = null)
        {
            QueryCriteria.Add(new DynamoQueryCriteriaEntity { PropertyName = propertyName, Value = value, QueryOperator = criteriaOperator, SecondValue = secondValue });
        }

        public void ClearScanCriteria()
        {
            SearchCriteria = new List<DynamoScanSearchCriteriaEntity>();
        }

        public void ClearQueryCriteria()
        {
            QueryCriteria = new List<DynamoQueryCriteriaEntity>();
        }

        public List<DynamoScanSearchCriteriaEntity> GetSearchCriteriaList()
        {
            return SearchCriteria;
        }

        public List<DynamoQueryCriteriaEntity> GetQueryCriteriaList()
        {
            return QueryCriteria;
        }

        public List<ScanCondition> GetScanConditionList()
        {
            if (SearchCriteria == null || SearchCriteria.Count == 0) return new List<ScanCondition>();

            var result = new List<ScanCondition>();

            foreach (var item in SearchCriteria)
            {
                result.Add(new ScanCondition(item.PropertyName, item.ScanOperator, item.Value));
            }

            return result;
        }

        public QueryFilter GetCriteriaQueryFilterForQueryFilter()
        {
            if (QueryCriteria == null || QueryCriteria.Count == 0) return new QueryFilter();

            var result = new QueryFilter();

            foreach (var item in QueryCriteria)
            {
                if (item.SecondValue != null)
                {
                    result.AddCondition(item.PropertyName, item.QueryOperator, new List<AttributeValue> { new AttributeValue { S = item.Value.ToString() }, new AttributeValue { S = item.SecondValue.ToString() } });
                }
                else
                {
                    result.AddCondition(item.PropertyName, item.QueryOperator, item.Value.ToString());
                }
            }

            return result;
        }

        public ScanFilter GetCriteriaScanFilterForSearchCriteria()
        {
            if (SearchCriteria == null || SearchCriteria.Count == 0) return new ScanFilter();

            var result = new ScanFilter();

            foreach (var item in SearchCriteria)
            {
                result.AddCondition(item.PropertyName, item.ScanOperator, item.Value.ToString());
            }

            return result;
        }
    }
}