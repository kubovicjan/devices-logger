// Copyright (c) Jan Kubovic All rights reserved.
// RegisteredDevices.cs

using DevicesLogger.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesLogger.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegisteredDevices : CommonControllerBase
{
    public RegisteredDevices(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public Task<IActionResult> GetAllRegisteredDevices()
    {
        //TODO: Add implementation to controller method
        throw new NotImplementedException();
    }

    [HttpGet("{serialNumber}")]
    public Task<IActionResult> GetRegisteredDevice([FromRoute]string serialNumber)
    {
        //TODO: Add implementation to controller method
        throw new NotImplementedException();
    }
}
