﻿using Devon4Net.Infrastructure.Common.Helpers.Interfaces;
using Microsoft.IO;
using static Microsoft.IO.RecyclableMemoryStreamManager;

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
        RecyclableMemoryStreamManager = new RecyclableMemoryStreamManager(new Options 
        {
            BlockSize = BlockSize,
            LargeBufferMultiple = LargeBufferMultiple,
            AggressiveBufferReturn = true,
            GenerateCallStacks = false,
            MaximumLargePoolFreeBytes = MaximumFreeSmallPoolBytes,
            MaximumSmallPoolFreeBytes = MaximumFreeSmallPoolBytes,
            MaximumBufferSize = MaxBufferSize,
        } ); 
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