// Copyright (c) Jan Kubovic All rights reserved.
// Thermometer.cs

namespace DevicesLogger.Domain.Devices;

public class Thermometer
{
    public required int MinTemperature { get; set; }
    public required int MaxTemperature { get; set;}
}
