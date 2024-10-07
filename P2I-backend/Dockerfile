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
COPY . .
COPY --from=build-env /out .

# Expose port 80 for the app
EXPOSE 8080

# Set the entry point to the application
ENTRYPOINT ["dotnet", "P2I-backend.dll"]
#CMD ["sleep", "3600"]