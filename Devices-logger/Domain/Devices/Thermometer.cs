// Copyright (c) Jan Kubovic All rights reserved.
// Thermometer.cs

namespace DevicesLogger.Domain.Devices;
using System.Text.Json.Serialization;
using DevicesLogger.Core;
using FluentValidation;

[JsonDerivedType(typeof(Thermometer), typeDiscriminator: "thermometer")]
public class Thermometer : Device
{
    public required double MinTemperature { get; set; }
    public required double MaxTemperature { get; set; }

    public class ThermometerValidator : AbstractValidator<Thermometer>
    {
        public ThermometerValidator()
        {
            Include(new DeviceValidator());
            RuleFor(x => x.MinTemperature).GreaterThanOrEqualTo(ValidationConstants.AbsoluteZero);
            RuleFor(x => x.MaxTemperature).GreaterThan(x => x.MinTemperature);
        }
    }
}
