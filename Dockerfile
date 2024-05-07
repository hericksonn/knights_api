# Use uma imagem base do .NET para execução
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use uma imagem base do .NET SDK para construção
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KnightsApi/KnightsApi.csproj", "KnightsApi/"]
RUN dotnet restore "KnightsApi/KnightsApi.csproj"
COPY . .
WORKDIR "/src/KnightsApi"
RUN dotnet build "KnightsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KnightsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KnightsApi.dll"]
