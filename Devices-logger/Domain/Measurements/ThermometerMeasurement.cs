// Copyright (c) Jan Kubovic All rights reserved.
// ThermometerMeasurement.cs

namespace DevicesLogger.Domain.Measurements;

public class ThermometerMeasurement : Measurement
{
    public required double Temperature { get; init; }
}
