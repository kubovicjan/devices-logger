// Copyright (c) Jan Kubovic All rights reserved.
// ErrorResponse.cs

namespace DevicesLogger.Domain.Errors;

public class ErrorResponse
{
    public required string ErrorMessage { get; init; }
    public int ErrorCode { get; init; }
}
