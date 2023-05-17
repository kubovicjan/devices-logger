// Copyright (c) Jan Kubovic All rights reserved.
// ReceiveThermometerMessage.cs

namespace DevicesLogger.CQRS.Commands.Messages;

using System.Threading;
using System.Threading.Tasks;
using DevicesLogger.Core;
using DevicesLogger.Domain.Measurements;
using DevicesLogger.Services;
using FluentValidation;
using MediatR;

public class ReceiveMessage
{
    public class Command : IRequest<bool>
    {
        public required Measurement Measurement { get; init; }
        public required string SerialNumber { get; init; }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SerialNumber).NotEmpty()
                                            .Length(ValidationConstants.SerialNumberLength);

                RuleFor(x => x.Measurement).SetInheritanceValidator(x =>
                {
                    x.Add(new ScaleMeasurement.ScaleMeasurementValidator());
                    x.Add(new ThermometerMeasurement.ThermometerMeasurementValidator());
                });
            }
        }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IDevicesService _devicesService;
        public Handler(IDevicesService devicesService)
        {
            _devicesService = devicesService ?? throw new ArgumentNullException(nameof(devicesService));
        }

        public Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_devicesService.AddMessageForDevice(request.SerialNumber,
                                                                       request.Measurement));
        }
    }
}
