# Stage 1: Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Expose standard HTTP and HTTPS ports
EXPOSE 80
EXPOSE 443

# Stage 2: Build and publish
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project file and restore dependencies
COPY BTM.Duende.ASPIdentity.csproj ./
RUN dotnet restore BTM.Duende.ASPIdentity.csproj

# Copy the full source and publish
COPY . ./
RUN dotnet publish BTM.Duende.ASPIdentity.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Final runtime image
FROM base AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Optional: Force HTTPS binding
ENV ASPNETCORE_URLS="https://+:443"

# Start the application
ENTRYPOINT ["dotnet", "BTM.Duende.ASPIdentity.dll"]
