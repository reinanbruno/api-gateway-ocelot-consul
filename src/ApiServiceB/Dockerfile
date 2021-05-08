#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5102

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiServiceB/ApiServiceB.csproj", "ApiServiceB/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "ApiServiceB/ApiServiceB.csproj"
COPY . .
WORKDIR "/src/ApiServiceB"
RUN dotnet build "ApiServiceB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiServiceB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiServiceB.dll"]