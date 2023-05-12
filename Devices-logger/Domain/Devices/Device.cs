// Copyright (c) Jan Kubovic All rights reserved.
// Device.cs

namespace DevicesLogger.Domain.Devices;

public abstract class Device
{
    public required string SerialNumber { get; init; }
    public required string FirmwareVersion { get; init; }
    public required string Vendor { get; init; }
    public required Status Status { get; init; }
}
