// Copyright (c) Jan Kubovic All rights reserved.
// Device.cs

namespace DevicesLogger.Domain.Devices;

using System.Text.Json.Serialization;
using DevicesLogger.Core;
using FluentValidation;

[JsonDerivedType(typeof(Scale), typeDiscriminator: "scale")]
[JsonDerivedType(typeof(Device), typeDiscriminator: "device")]
[JsonDerivedType(typeof(Thermometer), typeDiscriminator: "thermometer")]
public class Device
{
    public required string SerialNumber { get; init; }
    public required string FirmwareVersion { get; init; }
    public required string Vendor { get; init; }
    public required Status Status { get; init; }
    public required string BaseUnit { get; init; }

    public class DeviceValidator : AbstractValidator<Device>
    {
        public DeviceValidator()
        {
            RuleFor(x => x.SerialNumber).NotEmpty().Length(ValidationConstants.SerialNumberLength);
            RuleFor(x => x.FirmwareVersion).NotEmpty();
            RuleFor(x => x.Vendor).NotEmpty().Matches(ValidationConstants.VendorRegex);
            RuleFor(x => x.BaseUnit).NotEmpty();
        }
    }
}
