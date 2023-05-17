// Copyright (c) Jan Kubovic All rights reserved.
// GetMessagesForDevice.cs

namespace DevicesLogger.CQRS.Queries.Messages;

using System.Threading;
using System.Threading.Tasks;
using DevicesLogger.Domain.Measurements;
using DevicesLogger.Services;
using MediatR;

public class GetMessagesForDevice
{
    public class Query : IRequest<IEnumerable<Measurement>>
    {
        public required string SerialNumber { get; init; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Measurement>>
    {
        private readonly IDevicesService _devicesService;
        public Handler(IDevicesService devicesService)
        {
            _devicesService = devicesService ?? throw new ArgumentNullException(nameof(devicesService));
        }

        public Task<IEnumerable<Measurement>> Handle(Query request, CancellationToken cancellationToken)
        {
            var messages = _devicesService.GetMessagesForDevice(request.SerialNumber);
            return Task.FromResult(messages);
        }
    }
}
