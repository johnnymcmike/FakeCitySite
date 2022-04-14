FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server/FakeCitySite.Server.csproj", "Server/"]
COPY ["Client/FakeCitySite.Client.csproj", "Client/"]
COPY ["Shared/FakeCitySite.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/FakeCitySite.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "FakeCitySite.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FakeCitySite.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakeCitySite.Server.dll"]
