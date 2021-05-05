FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["L2CacheTest/L2CacheTest.csproj", "L2CacheTest/"]
RUN dotnet restore "L2CacheTest/L2CacheTest.csproj"
COPY . .
WORKDIR "/src/L2CacheTest"
RUN dotnet build "L2CacheTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "L2CacheTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "L2CacheTest.dll"]
