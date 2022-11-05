using System.Net;

namespace Http_Server;

public class ResponseInfo
{
    public readonly byte[] Buffer;
    public readonly string ContentType;
    public readonly HttpStatusCode ResponseStatusCode;

    public ResponseInfo(byte[] buffer, string contentType, HttpStatusCode responseStatusCode)
    {
        Buffer = buffer;
        ContentType = contentType;
        ResponseStatusCode = responseStatusCode;
    }
}