FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8084
EXPOSE 8085

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG GITHUB_USERNAME
RUN --mount=type=secret,id=github_personal_access_token \
    GITHUB_PERSONAL_ACCESS_TOKEN="$(cat /run/secrets/github_personal_access_token)" && \
    dotnet nuget add source "https://nuget.pkg.github.com/DreamFly-Airlines/index.json" \
    --name "github" \
    --username $GITHUB_USERNAME \
    --password $GITHUB_PERSONAL_ACCESS_TOKEN \
    --store-password-in-clear-text

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Authentication.Api/Authentication.Api.csproj", "src/Authentication.Api/"]
COPY ["src/Authentication.Domain/Authentication.Domain.csproj", "src/Authentication.Domain/"]
COPY ["src/Authentication.Application/Authentication.Application.csproj", "src/Authentication.Application/"]
COPY ["src/Authentication.Infrastructure/Authentication.Infrastructure.csproj", "src/Authentication.Infrastructure/"]
RUN dotnet restore "src/Authentication.Api/Authentication.Api.csproj"
COPY . .
WORKDIR "/src/src/Authentication.Api"
RUN dotnet build "./Authentication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Authentication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Api.dll"]
