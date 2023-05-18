// Copyright (c) Jan Kubovic All rights reserved.
// GetDevice.cs

namespace DevicesLogger.CQRS.Queries.Devices;

using System.Threading;
using System.Threading.Tasks;
using DevicesLogger.Core;
using DevicesLogger.Domain.Devices;
using DevicesLogger.Services;
using FluentValidation;
using MediatR;

public class GetDevice
{
    public class Query : IRequest<Device>
    {
        public required string SerialNumber { get; init; }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.SerialNumber).NotEmpty().Length(ValidationConstants.SerialNumberLength);
            }
        }
    }

    public class Handler : IRequestHandler<Query, Device>
    {
        private readonly IDevicesService _devicesService;
        public Handler(IDevicesService devicesService)
        {
            _devicesService = devicesService ?? throw new ArgumentNullException(nameof(devicesService));
        }

        public Task<Device> Handle(Query request, CancellationToken cancellationToken)
        {
            var device = _devicesService.GetDevice(request.SerialNumber);
            return Task.FromResult(device);
        }
    }
}
