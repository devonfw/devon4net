﻿namespace Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Dto
{
    public class ObjectTest
    {
        public bool EnableAws { get; set; }
        public bool UseSecrets { get; set; }
        public bool UseParameterStore { get; set; }
        public ObjectValues Values { get; set; }
    }
}
