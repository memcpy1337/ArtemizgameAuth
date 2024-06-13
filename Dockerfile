FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 6023
EXPOSE 6024

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthService.API/AuthService.API.csproj", "AuthService.API_build/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "AuthService.API_build/AuthService.API.csproj"

WORKDIR "/src/AuthService.API_build"
COPY .. .

RUN dotnet build "./AuthService.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV JWT_ACCESS_TOKEN_SECRET=$JWT_ACCESS_TOKEN_SECRET
ENV JWT_REFRESH_TOKEN_SECRET=$JWT_REFRESH_TOKEN_SECRET
ENV CONNSTRING=$CONNSTRING
ENTRYPOINT ["dotnet", "AuthService.API.dll"]