using System.Text.Json;

namespace Http_Server;

public static class ServerFileHandle
{
    public static  (byte[]?, string?) GetFile(string rawUrl, ServerSettings serverSetting)
    {
        byte[]? buffer = null;
        string? format = null;
        var filePath = serverSetting.Path + rawUrl;

        if(Directory.Exists(filePath))
        {
            filePath += "/index.html";
            if(File.Exists(filePath))
            {
                buffer = File.ReadAllBytes(filePath);
                format = filePath.Substring(filePath.LastIndexOf(".", StringComparison.Ordinal));
            }
        }
        else if(File.Exists(filePath))
        {
            buffer = File.ReadAllBytes(filePath);
            format = filePath.Substring(filePath.LastIndexOf(".", StringComparison.Ordinal));
        }

        return (buffer, format);
    }

    public static ServerSettings ReadJsonSettings(string path)
    {
        if (File.Exists(path))
        {
            return JsonSerializer.Deserialize<ServerSettings>(File.ReadAllBytes(path)) ?? 
                   throw new InvalidDataException("JsonSetting exists but cannot be Deserialize");
        }
        return new ServerSettings();
    }
}