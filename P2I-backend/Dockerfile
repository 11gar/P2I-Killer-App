# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -o /out

# Stage 2: Production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output from the build environment
COPY --from=build-env /out .
COPY ./BDD ./BDD
COPY ./generateSecret.sh .

# Generate a random JWT secret and replace in appsettings.json
RUN apt-get update && apt-get install -y openssl

RUN bash generateSecret.sh

# Expose port 8080 for the app
EXPOSE 8080

# Set the entry point to the application
ENTRYPOINT ["dotnet", "P2I-backend.dll"]
