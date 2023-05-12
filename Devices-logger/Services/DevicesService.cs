// Copyright (c) Jan Kubovic All rights reserved.
// DevicesService.cs

namespace DevicesLogger.Services;

using DevicesLogger.Domain.Devices;
using Microsoft.Extensions.Caching.Memory;

public class DevicesService
{
    private readonly IMemoryCache _memoryCache;

    public DevicesService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public bool AddDevice()
    {
        //TODO: Add implementation of AddDevice method
        throw new NotImplementedException();
    }

    public bool RemoveDevice()
    {
        //TODO: Add implementation of RemoveDevice method
        throw new NotImplementedException();
    }

    public IEnumerable<Device> GetAllDevices()
    {
        //TODO: Add implementation of GetAllDevices method
        throw new NotImplementedException();
    }
}
