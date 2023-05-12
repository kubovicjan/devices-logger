// Copyright (c) Jan Kubovic All rights reserved.
// CommonControllerBase.cs

namespace DevicesLogger.Core;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CommonControllerBase : ControllerBase
{
    private readonly IMediator _mediator;

    protected IMediator Mediator => _mediator;

    public CommonControllerBase(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
}
