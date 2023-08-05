#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

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

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSaver.API.dll"]