﻿namespace CozyHavenStay.Api.Exceptions;
public class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException(string msg) : base(msg)
    {
        
    }
}
