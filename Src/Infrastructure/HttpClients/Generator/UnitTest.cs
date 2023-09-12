using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.HttpClients.Generator;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public async Task GenerateFiles()
    {
        var apis = new[] {
            new Api("Shop", "https://localhost:5001/swagger/v1/swagger.json"),
        };

        var generations = apis
            .Select(api => ClientGenerator.GenerateCSharpClient(api))
            .ToArray();

        await Task.WhenAll(generations);
    }
}
