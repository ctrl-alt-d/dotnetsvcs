Packaging:

```
dotnet pack
```

Publishing:

```
cd src
dotnet nuget push Dotnetsvcs.DtoData.Abstractions\bin\Debug\Dotnetsvcs.DtoData.Abstractions.1.0.69.nupkg  --api-key __the_secret_api_key__ --source https://api.nuget.org/v3/index.json
dotnet nuget push Dotnetsvcs.DtoParm.Abstractions\bin\Debug\Dotnetsvcs.DtoParm.Abstractions.1.0.69.nupkg  --api-key __the_secret_api_key__ --source https://api.nuget.org/v3/index.json
dotnet nuget push Dotnetsvcs.DbCtx.Abstractions\bin\Debug\Dotnetsvcs.DbCtx.Abstractions.1.0.69.nupkg --api-key __the_secret_api_key__ --source https://api.nuget.org/v3/index.json
dotnet nuget push Dotnetsvcs.DbCtx\bin\Debug\Dotnetsvcs.DbCtx.1.0.69.nupkg --api-key __the_secret_api_key__ --source https://api.nuget.org/v3/index.json
dotnet nuget push Dotnetsvcs.Svc.Abstractions\bin\Debug\Dotnetsvcs.Svc.Abstractions.1.0.69.nupkg --api-key __the_secret_api_key__ --source https://api.nuget.org/v3/index.json
dotnet nuget push Dotnetsvcs.Facade.Abstractions\bin\Debug\Dotnetsvcs.Facade.Abstractions.1.0.69.nupkg --api-key __the_secret_api_key__ --source https://api.nuget.org/v3/index.json
dotnet nuget push Dotnetsvcs.Svc\bin\Debug\Dotnetsvcs.Svc.1.0.69.nupkg --api-key __the_secret_api_key__ --source https://api.nuget.org/v3/index.json
dotnet nuget push Dotnetsvcs\bin\Debug\Dotnetsvcs.1.0.69.nupkg --api-key __the_secret_api_key__ --source https://api.nuget.org/v3/index.json
```