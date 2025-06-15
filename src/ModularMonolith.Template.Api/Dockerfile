# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy main API and shared projects csproj files
COPY ["src/ModularMonolith.Template.Api/ModularMonolith.Template.Api.csproj", "src/ModularMonolith.Template.Api/"]
COPY ["src/ModularMonolith.Template.Config/ModularMonolith.Template.Config.csproj", "src/ModularMonolith.Template.Config/"]
COPY ["src/Modules/SharedKernel/ModularMonolith.Template.SharedKernel.csproj", "src/Modules/SharedKernel/"]

# Copy Users.API project csproj
COPY ["src/Modules/Users/Users.API/Users.API.csproj", "src/Modules/Users/Users.API/"]
# Copy Auth.API project csproj
COPY ["src/Modules/Auth/Auth.API/Auth.API.csproj", "src/Modules/Auth/Auth.API/"]

# Restore dependencies for main API (this will restore all project references)
RUN dotnet restore "src/ModularMonolith.Template.Api/ModularMonolith.Template.Api.csproj"

# Copy all source code files
COPY . .

# Build Users.API first
RUN dotnet build "src/Modules/Users/Users.API/Users.API.csproj" -c $BUILD_CONFIGURATION

# Then build Auth.API
RUN dotnet build "src/Modules/Auth/Auth.API/Auth.API.csproj" -c $BUILD_CONFIGURATION

# Build main API (this will trigger the custom Target in csproj to copy DLLs)
WORKDIR "/src/src/ModularMonolith.Template.Api"
RUN dotnet build "ModularMonolith.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage, perform the final publish which triggers the csproj ModularPublish Target to copy module DLLs
FROM build AS publish
RUN dotnet publish "ModularMonolith.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModularMonolith.Template.Api.dll"]
