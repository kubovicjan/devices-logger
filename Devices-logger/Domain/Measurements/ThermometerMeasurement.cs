// Copyright (c) Jan Kubovic All rights reserved.
// ThermometerMeasurement.cs

namespace DevicesLogger.Domain.Measurements;

using FluentValidation;

public class ThermometerMeasurement : Measurement
{
    public required double Temperature { get; init; }

    public class ThermometerMeasurementValidator : AbstractValidator<ThermometerMeasurement>
    {
        public ThermometerMeasurementValidator()
        {
            Include(new MeasurementValidator());
            RuleFor(x => x.Temperature).NotEmpty().GreaterThan(0);
        }
    }
}
