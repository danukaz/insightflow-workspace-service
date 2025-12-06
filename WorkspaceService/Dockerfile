# stage build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App

COPY . ./
WORKDIR /App/WorkspaceService
RUN dotnet restore
RUN dotnet publish -c Release -o out

# stage runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App
COPY --from=build /App/WorkspaceService/out .
ENTRYPOINT ["dotnet", "WorkspaceService.dll"]
