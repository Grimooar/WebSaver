FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebSaver.API/WebSaver.API.csproj", "WebSaver.API/"]
COPY ["WebSaver.APP/WebSaver.APP.csproj", "WebSaver.APP/"]
COPY ["WebSaver.Domain/WebSaver.Domain.csproj", "WebSaver.Domain/"]
COPY ["WebSaver.DTOs/WebSaver.DTOs.csproj", "WebSaver.DTOs/"]
COPY ["WebSaver.Infrastructure/WebSaver.Infrastructure.csproj", "WebSaver.Infrastructure/"]
RUN dotnet restore "WebSaver.API/WebSaver.API.csproj"
COPY . .
WORKDIR "/src/WebSaver.API"

FROM build AS publish
RUN dotnet publish "WebSaver.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Здесь используем образ postgres вместо aspnet
FROM postgres AS db

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSaver.API.dll"]
