// Copyright (c) Jan Kubovic All rights reserved.
// Measurement.cs

namespace DevicesLogger.Domain.Measurements;

using DevicesLogger.Domain.Devices;

public abstract class Measurement
{
    public required Device Device { get; init; }
    public required DateTime MeasurementDate { get; init; }
    public required double Longitude { get; init; }
    public required double Latitude { get; init; }
    public required string MeasurementUnit { get; init; }
}
