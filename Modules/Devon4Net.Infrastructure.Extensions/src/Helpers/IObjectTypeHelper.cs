namespace Devon4Net.Infrastructure.Extensions.Helpers
{
    public interface IObjectTypeHelper
    {
        object CovertObjectFromClassName(object objectInstance, string fullClassName);
    }
}