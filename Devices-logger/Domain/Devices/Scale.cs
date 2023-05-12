// Copyright (c) Jan Kubovic All rights reserved.
// Scale.cs

namespace DevicesLogger.Domain.Devices;
public class Scale : Device
{
    public required double MaxWeight { get; init; }
    public required double MinWeight { get; init; }
}
