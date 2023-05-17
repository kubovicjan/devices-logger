// Copyright (c) Jan Kubovic All rights reserved.
// ValidationConstants.cs

namespace DevicesLogger.Core;

public static class ValidationConstants
{
    public const int SerialNumberLength = 10;
    public const double AbsoluteZero = -273.15;
    public const string VendorRegex = "^[A-Z][a-zA-Z ]*$";
}
