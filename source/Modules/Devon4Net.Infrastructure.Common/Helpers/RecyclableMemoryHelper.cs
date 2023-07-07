using Devon4Net.Infrastructure.Common.Helpers.Interfaces;
using Microsoft.IO;

namespace Devon4Net.Infrastructure.Common.Helpers;

public class RecyclableMemoryHelper : IRecyclableMemoryHelper
{
    private int BlockSize { get; set; }
    private int LargeBufferMultiple { get; set; }
    private int MaxBufferSize { get; set; }
    private int MaximumFreeSmallPoolBytes { get; set; }
    private RecyclableMemoryStreamManager RecyclableMemoryStreamManager { get; }

    public RecyclableMemoryHelper()
    {
        BlockSize = 1024;
        LargeBufferMultiple = 80 * 1024;
        MaxBufferSize = 16 * LargeBufferMultiple;
        MaximumFreeSmallPoolBytes = 250 * BlockSize;
        RecyclableMemoryStreamManager = new RecyclableMemoryStreamManager(BlockSize, LargeBufferMultiple, MaxBufferSize)
        {
            AggressiveBufferReturn = true,
            GenerateCallStacks = false,
            MaximumFreeLargePoolBytes = MaximumFreeSmallPoolBytes,
            MaximumFreeSmallPoolBytes = MaximumFreeSmallPoolBytes
        };
    }

    public RecyclableMemoryHelper(RecyclableMemoryStreamManager recyclableMemoryStreamManager)
    {
        RecyclableMemoryStreamManager = recyclableMemoryStreamManager;
    }

    public MemoryStream GetMemoryStream()
    {
        return RecyclableMemoryStreamManager.GetStream();
    }

    public MemoryStream GetMemoryStream(byte[] byteArray)
    {
        return RecyclableMemoryStreamManager.GetStream(byteArray);
    }
}