using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace Infrastructure.HttpClients.Generator;

public static class ClientGenerator
{
    public async static Task GenerateCSharpClient(Api api)
    {
        var document = await OpenApiDocument.FromUrlAsync(api.Url);

        await GenerateClient(document, api);
    }        

    private static async Task GenerateClient(OpenApiDocument document, Api api)
    {
        Console.WriteLine($"Generating {api.Output}...");

        var settings = new CSharpClientGeneratorSettings
        {
            InjectHttpClient= true,
            AdditionalNamespaceUsages = new[] { "Infrastructure.HttpClients.Exceptions" },
            ClassName = api.ClassName,
            GenerateClientInterfaces = true,
            GenerateExceptionClasses = false,
            GenerateOptionalParameters = true,
            UseBaseUrl = false,
        };

        var generator = new CSharpClientGenerator(document, settings);
        var code = generator.GenerateFile();

        code = code.Replace("MyNamespace", api.Namespace);
        await File.WriteAllTextAsync(Path.Join("../../../", api.Output), code);
    }
}
