namespace Http_Server.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal class HttpPOST : Attribute
{
    public readonly string MethodURI;

    public HttpPOST(string methodUri = "")
    {
        MethodURI = methodUri;
    }
}