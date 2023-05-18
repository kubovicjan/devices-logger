// Copyright (c) Jan Kubovic All rights reserved.
// UnregisterDevice.cs

namespace DevicesLogger.CQRS.Commands.Devices;

using System.Threading;
using System.Threading.Tasks;
using DevicesLogger.Core;
using DevicesLogger.Services;
using FluentValidation;
using MediatR;

public class UnregisterDevice
{
    public class Command : IRequest<bool>
    {
        public required string SerialNumber { get; init; }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SerialNumber).NotEmpty().Length(ValidationConstants.SerialNumberLength);
            }
        }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IDevicesService _deviceService;
        
        public Handler(IDevicesService deviceService)
        {
            _deviceService = deviceService ?? throw new ArgumentNullException(nameof(deviceService));
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = _deviceService.RemoveDevice(request.SerialNumber);

            return await Task.FromResult(result);
        }
    }
}
