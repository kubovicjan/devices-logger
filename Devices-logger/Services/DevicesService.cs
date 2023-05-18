// Copyright (c) Jan Kubovic All rights reserved.
// DevicesService.cs

namespace DevicesLogger.Services;

using System.Collections.Concurrent;
using DevicesLogger.Domain.Devices;
using DevicesLogger.Domain.Measurements;

public class DevicesService : IDevicesService
{
    private readonly ConcurrentDictionary<string, List<Measurement>> _messages;
    private readonly ConcurrentDictionary<string, Device> _devices;

    public DevicesService()
    {
        _devices = new();
        _messages = new();
    }

    public bool AddDevice(Device device)
    {
        ArgumentNullException.ThrowIfNull(device);
        if (!_devices.TryAdd(device.SerialNumber, device))
        {
            throw new InvalidOperationException($"Device with serialNumber {device.SerialNumber} already registered!");
        }

        _ = _messages.TryAdd(device.SerialNumber, new());
        return true;
    }

    public bool RemoveDevice(string serialNumber)
    {
        var device = CheckDeviceExists(serialNumber);

        var pair = new KeyValuePair<string, Device>(serialNumber, device);
        _devices.TryRemove(pair);
        _messages.TryGetValue(serialNumber, out var messages);

        var messagesPair = new KeyValuePair<string, List<Measurement>>(serialNumber, messages!);
        _messages.TryRemove(messagesPair);
        return true;
    }

    public Device GetDevice(string serialNumber)
    {
        var device = CheckDeviceExists(serialNumber);
        return device!;
    }

    public IEnumerable<Device> GetAllDevices()
    {
        return _devices.Values;
    }

    public bool AddMessageForDevice(Measurement measurement)
    {
        ArgumentNullException.ThrowIfNull(measurement);
        var serialNumber = measurement.SerialNumber;
        var device = CheckDeviceExists(serialNumber);

        if (!measurement!.VerifyCompatiblerDevice(device!))
            throw new InvalidOperationException($"Mismatch between device type and message type");

        _ = _messages.TryGetValue(serialNumber, out var deviceMessages);
        _ = _messages.AddOrUpdate(serialNumber, deviceMessages!, (key, oldValue) =>
        {
            oldValue.Add(measurement!);
            return oldValue;
        });

        return true;
    }

    public IEnumerable<Measurement> GetMessagesForDevice(string serialNumber)
    {
        CheckDeviceExists(serialNumber);
        _ = _messages.TryGetValue(serialNumber, out var messages);
        return messages!;
    }

    private Device CheckDeviceExists(string serialNumber)
    {
        return !_devices.TryGetValue(serialNumber, out var device)
            ? throw new InvalidOperationException($"Device with {serialNumber} not found.")
            : device;
    }
}
