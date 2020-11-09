#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LipeGames.Api/LipeGames.Api.csproj", "LipeGames.Api/"]
COPY ["LipeGames.Dominio/LipeGames.Dominio.csproj", "LipeGames.Dominio/"]
COPY ["LipeGames.Infraestrutura.Dados.EFCore/LipeGames.Infraestrutura.Dados.EFCore.csproj", "LipeGames.Infraestrutura.Dados.EFCore/"]
RUN dotnet restore "LipeGames.Api/LipeGames.Api.csproj"
COPY . .
WORKDIR "/src/LipeGames.Api"
RUN dotnet build "LipeGames.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LipeGames.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LipeGames.Api.dll"]