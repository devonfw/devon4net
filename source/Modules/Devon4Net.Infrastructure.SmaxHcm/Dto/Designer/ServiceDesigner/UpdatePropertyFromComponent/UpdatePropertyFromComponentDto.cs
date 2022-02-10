using Devon4Net.Infrastructure.SmaxHcm.Common;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdatePropertyFromComponent
{
    public class UpdatePropertyFromComponentDto
    {
        public string propertyId { get; set; }
        public ComponentPropertyTypesConst valueType { get; set; }
        public object value { get; set; }
    }
}