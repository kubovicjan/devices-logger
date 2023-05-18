// Copyright (c) Jan Kubovic All rights reserved.
// DevicesServiceTests.cs

namespace DevicesLogger.Tests;

using DevicesLogger.Domain.Devices;
using DevicesLogger.Domain.Measurements;
using DevicesLogger.Services;

public class DevicesServiceTests
{
    [Fact]
    public void AddNullDeviceShouldThrowArgumentNullException()
    {
        //Arrange
        var sut = new DevicesService();

        //Act + Assert
        _ = Assert.Throws<ArgumentNullException>(() => sut.AddDevice(null!));
    }

    [Fact]
    public void AddDeviceShouldThrowInvalidOperationException()
    {
        //Arrange
        var sut = new DevicesService();
        var scale = new Scale()
        {
            SerialNumber = "abcd",
            FirmwareVersion = "1.0",
            MaxWeight = 100,
            MinWeight = 1,
            Status = 0,
            Vendor = "Bizerba",
            BaseUnit ="Kg"
        };
        _ = sut.AddDevice(scale);

        //Act + Assert
        _ = Assert.Throws<InvalidOperationException>(() => sut.AddDevice(scale));
    }

    [Fact]
    public void AddDeviceShouldSucceed()
    {
        //Arrange
        var sut = new DevicesService();
        var serialNumber = "abcd";
        var scale = new Scale()
        {
            SerialNumber = serialNumber,
            FirmwareVersion = "1.0",
            MaxWeight = 100,
            MinWeight = 1,
            Status = 0,
            Vendor = "Bizerba",
            BaseUnit = "Kg"
        };

        //Act
        var result = sut.AddDevice(scale);
        var messages = sut.GetMessagesForDevice(serialNumber);
        //Assert
        Assert.True(result);
        Assert.Empty(messages);
    }

    [Fact]
    public void AddMessageForUnregisteredDeviceShouldThrowInvalidOperationException()
    {
        //Arrange
        var sut = new DevicesService();
        var measurement = new ScaleMeasurement()
        {
            Latitude = 12.5,
            Longitude = 44.4,
            MeasurementDate = DateTime.Now,
            MeasurementUnit = "Kg",
            SerialNumber = "1234",
            Weight = 44.4
        };

        //Act + Assert
        _ = Assert.Throws<InvalidOperationException>(() => sut.AddMessageForDevice(measurement));
    }

    [Fact]
    public void AddMessageForDifferentTypeDeviceShouldThrowInvalidOperationException()
    {
        var sut = new DevicesService();
        var serialNumber = "0123456789";
        var device = new Thermometer()
        {
            FirmwareVersion = "abc",
            MaxTemperature = 100,
            MinTemperature = 0,
            SerialNumber = serialNumber,
            Status= 0,
            Vendor = "Xyz",
            BaseUnit = "K"
        };

        var measurement = new ScaleMeasurement()
        {
            Latitude = 12.5,
            Longitude = 44.4,
            MeasurementDate = DateTime.Now,
            MeasurementUnit = "Kg",
            SerialNumber = "1234",
            Weight = 44.4
        };

        //Act + Assert
        _ = Assert.Throws<InvalidOperationException>(() => sut.AddMessageForDevice(measurement));
    }

    [Fact]
    public void AddMessageForDeviceShouldSucceed()
    {
        //Arrange
        var serialNumber = "abcd";
        var sut = new DevicesService();

        var scale = new Scale()
        {
            SerialNumber = serialNumber,
            FirmwareVersion = "1.0",
            MaxWeight = 100,
            MinWeight = 1,
            Status = 0,
            Vendor = "Bizerba",
            BaseUnit = "Kg"
        };
        _ = sut.AddDevice(scale);

        var measurement = new ScaleMeasurement()
        {
            Latitude = 12.5,
            Longitude = 44.4,
            MeasurementDate = DateTime.Now,
            MeasurementUnit = "Kg",
            SerialNumber = serialNumber,
            Weight = 44.4
        };

        //Act
        var result = sut.AddMessageForDevice(measurement);
        var messages = sut.GetMessagesForDevice(serialNumber);

        //Assert
        Assert.True(result);
        _ = Assert.Single(messages);
    }

    [Fact]
    public void UnregisterUnregisteredDeviceShouldThrowInvalidOperationException()
    {
        //Arrange
        var serialNumber = "0123456789";
        var sut = new DevicesService();

        //Act + Assert
        _ = Assert.Throws<InvalidOperationException>(() => sut.RemoveDevice(serialNumber));
    }

    [Fact]
    public void UnregisterRegisteredDeviceShouldSucceed()
    {
        //Arrange
        var serialNumber = "0123456789";
        var sut = new DevicesService();

        var scale = new Scale()
        {
            SerialNumber = serialNumber,
            FirmwareVersion = "1.0",
            MaxWeight = 100,
            MinWeight = 1,
            Status = 0,
            Vendor = "Bizerba",
            BaseUnit = "Kg"
        };
        _ = sut.AddDevice(scale);

        var measurement = new ScaleMeasurement()
        {
            Latitude = 12.5,
            Longitude = 44.4,
            MeasurementDate = DateTime.Now,
            MeasurementUnit = "Kg",
            SerialNumber = serialNumber,
            Weight = 44.4
        };
        _ = sut.AddMessageForDevice(measurement);

        //Act + Assert
        var result = sut.RemoveDevice(serialNumber);
        _ = Assert.Throws<InvalidOperationException>(() => sut.GetMessagesForDevice(serialNumber));
        _ = Assert.Throws<InvalidOperationException>(() => sut.GetDevice(serialNumber));
    }
}
