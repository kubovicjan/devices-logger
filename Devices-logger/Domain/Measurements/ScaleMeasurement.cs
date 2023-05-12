// Copyright (c) Jan Kubovic All rights reserved.
// ScaleMeasurement.cs

namespace DevicesLogger.Domain.Measurements;

public class ScaleMeasurement : Measurement
{
    public required double Weight { get; set; }
}
