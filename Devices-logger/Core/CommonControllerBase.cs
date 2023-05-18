// Copyright (c) Jan Kubovic All rights reserved.
// CommonControllerBase.cs

namespace DevicesLogger.Core;

using DevicesLogger.Domain.Errors;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(typeof(ErrorResponse), 400)]
[ProducesResponseType(typeof(ErrorResponse), 500)]
public class CommonControllerBase : ControllerBase
{
    protected IMediator Mediator { get; }

    public CommonControllerBase(IMediator mediator)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
}
