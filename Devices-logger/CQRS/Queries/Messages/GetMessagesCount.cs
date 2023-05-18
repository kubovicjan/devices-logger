// Copyright (c) Jan Kubovic All rights reserved.
// GetMessagesCount.cs

namespace DevicesLogger.CQRS.Queries.Messages;

using System.Threading;
using System.Threading.Tasks;
using MediatR;

public class GetMessagesCount
{
    public class Query : IRequest<int>
    {
    }

    public class Handler : IRequestHandler<Query, int>
    {
        private readonly IMediator _mediator;
        public Handler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<int> Handle(Query request, CancellationToken cancellationToken)
        {
            return (await _mediator.Send(new GetAllMessages.Query())).Count();
        }
    }
}
