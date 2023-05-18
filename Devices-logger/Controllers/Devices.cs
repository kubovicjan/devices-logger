// Copyright (c) Jan Kubovic All rights reserved.
// Devices.cs

namespace DevicesLogger.Controllers;

using DevicesLogger.Core;
using DevicesLogger.CQRS.Commands.Devices;
using DevicesLogger.Domain.Devices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class Devices : CommonControllerBase
{
    public Devices(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("scale")]
    public async Task<IActionResult> RegisterScale(Scale device)
    {
        _ = await Mediator.Send(new RegisterDevice.Command()
        {
            Device = device
        });
        return NoContent();
    }

    [HttpPost("thermometer")]
    public async Task<IActionResult> RegisterDevice(Thermometer device)
    {
        _ = await Mediator.Send(new RegisterDevice.Command()
        {
            Device = device
        });
        return NoContent();
    }

    [HttpDelete]
    public IActionResult UnregisterDevice(string serialNumber)
    {
        _ = Mediator.Send(new UnregisterDevice.Command()
        { 
            SerialNumber = serialNumber
        });
        return NoContent();
    }
}
