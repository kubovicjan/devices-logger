﻿// Copyright (c) Jan Kubovic All rights reserved.
// GetAllDevices.cs

namespace DevicesLogger.CQRS.Queries.Devices;

using System.Threading;
using System.Threading.Tasks;
using DevicesLogger.Domain.Devices;
using DevicesLogger.Services;
using MediatR;

public class GetAllDevices
{
    public class Query : IRequest<IEnumerable<Device>>
    {

    }

    public class Handler : IRequestHandler<Query, IEnumerable<Device>>
    {
        private readonly IDevicesService _devicesService;

        public Handler(IDevicesService devicesService)
        {
            _devicesService = devicesService ?? throw new ArgumentNullException(nameof(devicesService));
        }

        public async Task<IEnumerable<Device>> Handle(Query request, CancellationToken cancellationToken)
        {
            var devices = _devicesService.GetAllDevices();
            return await Task.FromResult(devices);
        }
    }
}
