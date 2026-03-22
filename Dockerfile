# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY UrbanControl.Backend/UrbanControl.Backend.csproj UrbanControl.Backend/
RUN dotnet restore UrbanControl.Backend/UrbanControl.Backend.csproj

COPY . .
RUN dotnet publish UrbanControl.Backend/UrbanControl.Backend.csproj -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "UrbanControl.Backend.dll"]
