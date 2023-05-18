// Copyright (c) Jan Kubovic All rights reserved.
// Messages.cs

namespace DevicesLogger.Controllers;

using DevicesLogger.Core;
using DevicesLogger.CQRS.Commands.Messages;
using DevicesLogger.CQRS.Queries.Messages;
using DevicesLogger.Domain.Measurements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class Messages : CommonControllerBase
{
    public Messages(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Measurement>), 200)]
    public async Task<IActionResult> GetAllMessages()
    {
        var data = await Mediator.Send(new GetAllMessages.Query());
        return Ok(data);    
    }

    [HttpGet("count")]
    [ProducesResponseType(typeof(int),200)]
    public async Task<IActionResult> GetAllMessagesCount()
    {
        var count = await Mediator.Send(new GetMessagesCount.Query());
        return Ok(count);
    }

    [HttpGet("{serialNumber}")]
    [ProducesResponseType(typeof(IEnumerable<Measurement>), 200)]
    public async Task<IActionResult> GetMessagesFromDevice([FromRoute] string serialNumber)
    {
        var data = await Mediator.Send(new GetMessagesForDevice.Query()
        {
            SerialNumber = serialNumber
        });
        return Ok(data);
    }

    [HttpPost("thermometer")]
    public async Task<IActionResult> ReceiveThermometerMessage([FromBody] ThermometerMeasurement message)
    {
        _ = await Mediator.Send(new ReceiveScaleMessage.Command
        {
            Measurement = message,
        });
        return NoContent();
    }

    [HttpPost("scale")]
    public async Task<IActionResult> ReceiveScaleMessage([FromBody] ScaleMeasurement message)
    {
        _ = await Mediator.Send(new ReceiveScaleMessage.Command
        {
            Measurement = message,
        });
        return NoContent();
    }
}
