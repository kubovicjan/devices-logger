// Copyright (c) Jan Kubovic All rights reserved.
// RegisteredDevices.cs

namespace DevicesLogger.Controllers;
using DevicesLogger.Core;
using DevicesLogger.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RegisteredDevices : CommonControllerBase
{
    public RegisteredDevices(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRegisteredDevices()
    {
        var result = await Mediator.Send(new GetAllDevices.Query());
        return Ok(result);
    }

    [HttpGet("{serialNumber}")]
    public IActionResult GetRegisteredDevice([FromRoute] string serialNumber)
    {
        //TODO: Add implementation to controller method
        throw new NotImplementedException();
    }
}
