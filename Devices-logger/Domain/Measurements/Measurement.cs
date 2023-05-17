// Copyright (c) Jan Kubovic All rights reserved.
// Measurement.cs

namespace DevicesLogger.Domain.Measurements;

using System.Text.Json.Serialization;
using FluentValidation;

[JsonDerivedType(typeof(ScaleMeasurement))]
[JsonDerivedType(typeof(ThermometerMeasurement))]
public abstract class Measurement
{
    public required string SerialNumber { get; init; }
    public required DateTime MeasurementDate { get; init; }
    public required double Longitude { get; init; }
    public required double Latitude { get; init; }
    public required string MeasurementUnit { get; init; }

    public class MeasurementValidator : AbstractValidator<Measurement>
    {
        public MeasurementValidator()
        {
            RuleFor(x => x.MeasurementUnit).NotEmpty();
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180);
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
        }
    }
}
