// Copyright (c) Jan Kubovic All rights reserved.
// IDevicesService.cs

namespace DevicesLogger.Services;

using DevicesLogger.Domain.Devices;
using DevicesLogger.Domain.Measurements;

public interface IDevicesService
{
    bool AddDevice(Device device);
    bool RemoveDevice(string serialNumber);
    Device GetDevice(string serialNumber);
    IEnumerable<Device> GetAllDevices();

    bool AddMessageForDevice(string serialNumber, Measurement measurement);
    IEnumerable<Measurement> GetMessagesForDevice(string serialNumber);
}
