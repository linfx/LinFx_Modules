FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/AuthServer.Host/AuthServer.Host.csproj src/AuthServer.Host/
RUN dotnet restore src/AuthServer.Host/AuthServer.Host.csproj
COPY . .
WORKDIR /src/src/AuthServer.Host
RUN dotnet build AuthServer.Host.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AuthServer.Host.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthServer.Host.dll"]
