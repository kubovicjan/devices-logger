// Copyright (c) Jan Kubovic All rights reserved.
// GetAllMessages.cs

namespace DevicesLogger.CQRS.Queries.Messages;

using System.Threading;
using System.Threading.Tasks;
using DevicesLogger.Domain.Measurements;
using DevicesLogger.Services;
using MediatR;

public class GetAllMessages
{
    public class Query : IRequest<IEnumerable<Measurement>>
    {
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Measurement>>
    {
        private readonly IDevicesService _devicesService;

        public Handler(IDevicesService devicesService)
        {
            _devicesService = devicesService ?? throw new ArgumentNullException(nameof(devicesService));
        }

        public async Task<IEnumerable<Measurement>> Handle(Query request, CancellationToken cancellationToken)
        {
            var messages = new List<Measurement>();
            var devices = _devicesService.GetAllDevices();

            foreach(var device in devices)
            {
                messages.AddRange(_devicesService.GetMessagesForDevice(device.SerialNumber));
            }

            return await Task.FromResult(messages);
        }
    }
}
