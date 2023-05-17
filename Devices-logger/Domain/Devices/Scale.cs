// Copyright (c) Jan Kubovic All rights reserved.
// Scale.cs

namespace DevicesLogger.Domain.Devices;
using System.Text.Json.Serialization;
using DevicesLogger.Core;
using FluentValidation;

[JsonDerivedType(typeof(Scale), typeDiscriminator: "scale")]
public class Scale : Device
{
    public required double MaxWeight { get; init; }
    public required double MinWeight { get; init; }

    public class ScaleValidator : AbstractValidator<Scale>
    {
        public ScaleValidator()
        {
            Include(new DeviceValidator());
            RuleFor(x => x.MinWeight).NotEmpty().GreaterThan(0);
            RuleFor(x => x.MaxWeight).NotEmpty().GreaterThan(x => x.MinWeight);
        }
    }
}
