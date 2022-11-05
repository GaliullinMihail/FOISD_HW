namespace Http_Server.Attributes;

public class HttpController : Attribute
{
    public readonly string ControllerName;

    public HttpController(string controllerName)
    {
        ControllerName = controllerName;
    }
}