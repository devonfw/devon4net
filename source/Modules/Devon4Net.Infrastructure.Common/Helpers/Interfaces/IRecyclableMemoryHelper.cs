namespace Devon4Net.Infrastructure.Common.Helpers.Interfaces;

public interface IRecyclableMemoryHelper
{
    MemoryStream GetMemoryStream();
    MemoryStream GetMemoryStream(byte[] byteArray);
}