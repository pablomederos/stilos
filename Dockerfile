FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

# Copy solution and project files
COPY Stilos.slnx .
COPY src/Stilos.csproj ./src/

# Restore dependencies
RUN dotnet restore src/Stilos.csproj

# Copy the rest of the source code
COPY src/ ./src/

# Publish without --no-restore to ensure a clean final step
RUN dotnet publish src/Stilos.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Stilos.dll"]
