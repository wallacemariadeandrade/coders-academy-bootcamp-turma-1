# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:5.0 AS build
WORKDIR /CodersAcademy
COPY *.sln .
COPY CodersAcademy.API/*.csproj CodersAcademy.API/
RUN dotnet restore
COPY . .

# publish
FROM build AS publish
WORKDIR /CodersAcademy/CodersAcademy.API
RUN dotnet publish -c Release -o /CodersAcademy/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:5.0 AS runtime
WORKDIR /CodersAcademy
COPY --from=publish /CodersAcademy/publish .
# ENTRYPOINT ["dotnet", "Colors.API.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CodersAcademy.API.dll
