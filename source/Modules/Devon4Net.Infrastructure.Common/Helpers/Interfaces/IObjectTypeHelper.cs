namespace Devon4Net.Infrastructure.Common.Helpers.Interfaces
{
    public interface IObjectTypeHelper
    {
        object CovertObjectFromClassName(object objectInstance, string fullClassName);
    }
}