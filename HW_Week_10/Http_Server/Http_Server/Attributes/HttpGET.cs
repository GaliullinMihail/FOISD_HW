namespace Http_Server.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal class HttpGET : Attribute
{
    public readonly string MethodURI;

    public HttpGET(string methodUri = "")
    {
        MethodURI = methodUri;
    }
}