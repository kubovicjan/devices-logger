// Copyright (c) Jan Kubovic All rights reserved.
// ScaleMeasurement.cs

namespace DevicesLogger.Domain.Measurements;

using FluentValidation;

public class ScaleMeasurement : Measurement
{
    public required double Weight { get; set; }

    public class ScaleMeasurementValidator : AbstractValidator<ScaleMeasurement>
    {
        public ScaleMeasurementValidator()
        {
            Include(new MeasurementValidator());
            RuleFor(x => x.Weight).NotEmpty().GreaterThan(0);
        }
    }
}
