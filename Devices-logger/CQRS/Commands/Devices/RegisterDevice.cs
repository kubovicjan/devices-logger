// Copyright (c) Jan Kubovic All rights reserved.
// RegisterDevice.cs

namespace DevicesLogger.CQRS.Commands.Devices;

using System.Threading;
using System.Threading.Tasks;
using DevicesLogger.Domain.Devices;
using DevicesLogger.Services;
using MediatR;
using FluentValidation;
using DevicesLogger.Core;
using DevicesLogger.Domain.Measurements;

public class RegisterDevice
{
    public class Command : IRequest<bool>
    {
        public required Device Device { get; init; }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Device).NotEmpty().SetInheritanceValidator(x =>
                {
                    x.Add(new Scale.ScaleValidator());
                    x.Add(new Thermometer.ThermometerValidator());
                });
            }
        }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IDevicesService _service;
        public Handler(IDevicesService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_service.AddDevice(request.Device));
        }
    }
}
