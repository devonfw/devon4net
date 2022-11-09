using Devon4Net.Infrastructure.Common.Helpers.Interfaces;

namespace Devon4Net.Infrastructure.Common.Helpers
{
    public class ObjectTypeHelper : IObjectTypeHelper
    {
        public object CovertObjectFromClassName(object objectInstance, string fullClassName)
        {
            if (string.IsNullOrEmpty(fullClassName)) throw new ArgumentException("The class name cannot be null");
            var classNameTarget = Type.GetType(fullClassName);
            if (classNameTarget == null) throw new ArgumentException("Cannot get the type of the provided class name");
            return Convert.ChangeType(objectInstance, classNameTarget);
        }
    }
}
