// Copyright (c) Jan Kubovic All rights reserved.
// CommonControllerBase.cs

namespace DevicesLogger.Core;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CommonControllerBase : ControllerBase
{
    protected IMediator Mediator { get; }

    public CommonControllerBase(IMediator mediator)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
}
