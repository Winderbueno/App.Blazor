# Introduction 
This project is an app template based on Blazor & Clean Architecture
As defined in architecture, it is a client of :
* [Shop.Api](https://shoppinglistappback.azurewebsites.net/swagger/index.html). Users

# Getting Started

## Configuration
* Startup Projects. 'Ui'
* Files
  * ./Ui/Properties/launchSettings.json. Configure Env (Development | Integration | Production) in `Profiles/Xxx/environmentVariables/ASPNETCORE_ENVIRONMENT`
  * ./Ui/wwwroot/appsettings.<env>.json. By Env, configure Log, ApiUrls... 

## Build & Run
* App. 
  * Normal. Just clic play in Visual Studio 20XX
  * [HotReload](https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-net-6-preview-3/#initial-net-hot-reload-support). Run `dotnet watch --Project C:\<where you have the repo>\App.Blazor\Src\Ui\ run`
* Test. No test for now :(