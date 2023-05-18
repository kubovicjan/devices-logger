// Copyright (c) Jan Kubovic All rights reserved.
// RegisteredDevices.cs

namespace DevicesLogger.Controllers;
using DevicesLogger.Core;
using DevicesLogger.CQRS.Queries.Devices;
using DevicesLogger.Domain.Devices;
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
    [ProducesResponseType(typeof(Device), 200)]
    public IActionResult GetRegisteredDevice([FromRoute] string serialNumber)
    {
        var device = Mediator.Send(new GetDevice.Query()
        {
            SerialNumber= serialNumber
        });
        return Ok(device);
    }
}
