Get an exe

```
dotnet publish --self-contained --runtime win-x64
```

Install as a service

```
C:\windows\System32\sc.exe create AspNetCoreWindowsService binPath= '"C:\_cp\Git\aspnet-core-samples\windows-service-host\SampleApp\bin\Debug\netcoreapp2.0\win-x64\publish\SampleApp.exe" web --service' start= demand
```

