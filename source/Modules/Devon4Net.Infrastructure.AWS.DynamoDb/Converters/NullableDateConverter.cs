using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Devon4Net.Infrastructure.AWS.DynamoDb.Converters
{
    public class NullableDateConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var dateTime = entry?.AsString();

            if (string.IsNullOrEmpty(dateTime)) return null;

            if (!DateTime.TryParse(dateTime, out DateTime value))
            {
                throw new ArgumentException("The entry parameter is not a valid DateTime value.", nameof(entry));
            }

            return value;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            if (value == null) return new DynamoDBNull();

            if (DateTime.TryParse(value.ToString(), out DateTime dt))
            {
                return dt.ToString();
            }

            throw new ArgumentException("The value parameter must be a DateTime or a Nullable<DateTime>.", nameof(value));
        }
    }
}