# Use the official ASP.NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Build the runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

WORKDIR /app
COPY --from=build-env /app/out ./

# Copy the SQLite database file to the working directory
COPY products-chonaDb.db ./

# Set the entry point for the application.
ENTRYPOINT ["dotnet", "ProductsChona.dll"]
