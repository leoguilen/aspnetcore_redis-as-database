FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet_restore
COPY "TechshopService.sln" "TechshopService.sln"
COPY "src/TechshopService.Api/" "src/TechshopService.Api/"
COPY "src/TechshopService.Core/" "src/TechshopService.Core/"
COPY "src/TechshopService.Infra.Data/" "src/TechshopService.Infra.Data/"
COPY "src/TechshopService.Infra.Logger/" "src/TechshopService.Infra.Logger/"
COPY "src/TechshopService.Shared/" "src/TechshopService.Shared/"
COPY "test/TechshopService.Api.Test/" "test/TechshopService.Api.Test/"
COPY "test/TechshopService.Core.Test/" "test/TechshopService.Core.Test/"
COPY "test/TechshopService.Infra.Data.Test/" "test/TechshopService.Infra.Data.Test/"
COPY "test/TechshopService.Infra.Logger.Test/" "test/TechshopService.Infra.Logger.Test/"
COPY "test/TechshopService.Shared.Test/" "test/TechshopService.Shared.Test/"
RUN dotnet restore "TechshopService.sln" --no-cache
RUN dotnet test "TechshopService.sln"

FROM dotnet_restore as dotnet_publish
WORKDIR /src
COPY . .
RUN dotnet publish "TechshopService.Api/TechshopService.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS dotnet_runtime
WORKDIR /app
COPY --from=dotnet_publish /app .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "TechshopService.Api.dll"]