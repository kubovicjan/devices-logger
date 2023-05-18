// Copyright (c) Jan Kubovic All rights reserved.
// ThermometerMeasurement.cs

namespace DevicesLogger.Domain.Measurements;

using DevicesLogger.Domain.Devices;
using FluentValidation;

public class ThermometerMeasurement : Measurement
{
    public required double Temperature { get; init; }

    public override bool VerifyCompatiblerDevice(Device device)
    {
        return device is Thermometer;
    }

    public class ThermometerMeasurementValidator : AbstractValidator<ThermometerMeasurement>
    {
        public ThermometerMeasurementValidator()
        {
            Include(new MeasurementValidator());
            RuleFor(x => x.Temperature).NotEmpty().GreaterThan(0);
        }
    }
}
