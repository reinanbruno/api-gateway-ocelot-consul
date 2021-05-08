#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5101

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiServiceA/ApiServiceA.csproj", "ApiServiceA/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "ApiServiceA/ApiServiceA.csproj"
COPY . .
WORKDIR "/src/ApiServiceA"
RUN dotnet build "ApiServiceA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiServiceA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiServiceA.dll"]