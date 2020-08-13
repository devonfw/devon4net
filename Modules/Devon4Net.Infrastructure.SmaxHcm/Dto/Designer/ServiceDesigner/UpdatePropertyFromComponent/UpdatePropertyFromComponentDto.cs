using Devon4Net.Infrastructure.SmaxHcm.Common;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdatePropertyFromComponent
{
    public class UpdatePropertyFromComponentDto
    {
        public string propertyId { get; set; }
        public ComponentPropertyTypesConst valueType { get; set; }
        public int? valueNumber { get; set; }
        public string valueString { get; set; }
        public bool? valueBoolean { get; set; }
        public IList<PropertyListType> valueList { get; set; }
    }
}
