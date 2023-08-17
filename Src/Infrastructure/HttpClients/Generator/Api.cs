namespace Infrastructure.HttpClients.Generator;

public class Api
{
    public string Url { get; set; }
    public string Name { get; set; }
    public string Namespace => "Infrastructure.HttpClients." + Name;
    public string ClassName => Name + "Api";
    public string Output => $"./HttpClients/Generated/{ClassName}.generated.cs";

    public Api(string name, string url)
    {
        Name = name;
        Url = url;
    }
}
