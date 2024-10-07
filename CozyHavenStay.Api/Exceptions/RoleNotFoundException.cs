namespace CozyHavenStay.Api.Exceptions;
public class RoleNotFoundException : Exception
{
    public RoleNotFoundException(string message) : base(message) { }
}
