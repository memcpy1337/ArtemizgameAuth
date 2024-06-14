# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 6023
EXPOSE 6024

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["AuthService.API/AuthService.API.csproj", "AuthService.API/"]
COPY ["AuthService.API/serilog.json", "AuthService.API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "AuthService.API/AuthService.API.csproj"

# Copy all files and build
WORKDIR "/src/AuthService.API"
COPY . .

RUN dotnet build "AuthService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuthService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.API.Build.dll"]