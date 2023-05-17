// Copyright (c) Jan Kubovic All rights reserved.
// Messages.cs

namespace DevicesLogger.Controllers;

using DevicesLogger.Core;
using DevicesLogger.CQRS.Commands;
using DevicesLogger.CQRS.Queries;
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
    public async Task<IActionResult> GetAllMessages()
    {
        //TODO: Add implementation
        throw new NotImplementedException();
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetAllMessagesCount

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

    [HttpPost("thermometer/{serialNumber}")]
    public async Task<IActionResult> ReceiveThermometerMessage([FromRoute] string serialNumber,
                                                [FromBody] ThermometerMeasurement message)
    {
        _ = await Mediator.Send(new ReceiveMessage.Command
        {
            Measurement = message,
            SerialNumber = serialNumber
        });
        return NoContent();
    }

    [HttpPost("scale/{serialNumber}")]
    public IActionResult ReceiveScaleMessage([FromRoute] string serialNumber,
                                                [FromBody] ScaleMeasurement message)
    {
        //TODO: Add implementation here
        throw new NotImplementedException();
    }
}
