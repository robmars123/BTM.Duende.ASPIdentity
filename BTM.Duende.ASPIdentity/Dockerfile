# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy the CSPROJ and restore dependencies
COPY BTM.Duende.ASPIdentity.csproj ./
RUN dotnet restore BTM.Duende.ASPIdentity.csproj

# Copy everything else
COPY . ./

# Build and publish
RUN dotnet build BTM.Duende.ASPIdentity.csproj -c Release -o bin/Release/net9.0/publish /p:UseAppHost=false
RUN dotnet publish BTM.Duende.ASPIdentity.csproj -c Release -o bin/Release/net9.0/publish /p:UseAppHost=false
# Final image
FROM base AS final
WORKDIR /app
COPY bin/Release/net9.0/publish/. .

ENTRYPOINT ["dotnet", "BTM.Duende.ASPIdentity.dll"]
