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

# Create a non-root user and group named "csharp"
RUN addgroup --system csharp && adduser --system --ingroup csharp csharp

# Copy the build output from the build environment
COPY --from=build-env /out .
COPY ./BDD ./BDD
COPY ./generateSecret.sh .

RUN apt-get update && apt-get install -y openssl

# Change ownership of the app files to the "csharp" user
RUN chown -R csharp:csharp /app

# Switch to the non-root user "csharp"
USER csharp

# Expose port 8080 for the app
EXPOSE 8080

# Set the cmd 
CMD ["dotnet", "P2I-backend.dll"]
