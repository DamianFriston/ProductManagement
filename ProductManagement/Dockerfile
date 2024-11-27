# Stage 1: Build the .NET API and install Node.js for the client-side build
FROM mcr.microsoft.com/dotnet/sdk:8.0.402 AS build

FROM mcr.microsoft.com/dotnet/sdk:8.0.402 AS build
WORKDIR /src

# Copy the solution and project files
COPY ProductManagement.sln . 
COPY ProductManagement.Server/ProductManagement.Server.csproj ./ProductManagement.Server/
COPY productmanagement.client/package.json ./productmanagement.client/

# Restore dependencies for the .NET API
RUN dotnet restore ProductManagement.Server/ProductManagement.Server.csproj

# Install Node.js and npm for the client-side build
RUN apt-get update \
    && apt-get install -y curl gnupg2 lsb-release ca-certificates \
    && curl -sL https://deb.nodesource.com/setup_18.x | bash - \
    && apt-get install -y nodejs \
    && apt-get clean

# Copy the rest of the code
COPY . . 

# Run npm build for the client-side part
WORKDIR /src/productmanagement.client
RUN npm install
RUN npm run build

# Build and publish the .NET API
WORKDIR /src
RUN dotnet build ProductManagement.Server/ProductManagement.Server.csproj -c Release -o /app/build
RUN dotnet publish ProductManagement.Server/ProductManagement.Server.csproj -c Release -o /app/publish

# Stage 2: Run the .NET API
FROM mcr.microsoft.com/dotnet/sdk:8.0.402 AS final-api
WORKDIR /app
EXPOSE 5000

# Copy the build artifacts from the previous stage
COPY --from=build /app/publish .

# Set environment variable for DOTNET to listen on any IP Address
ENV DOTNET_URLS=http://+:5000

# Set the entry point to run the API
ENTRYPOINT ["dotnet", "ProductManagement.Server.dll"]
