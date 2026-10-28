# Build from the repository root:
# Dockerfile: modern/DerafshSample.Modern/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY modern/DerafshSample.Modern/DerafshSample.Modern.csproj modern/DerafshSample.Modern/
RUN dotnet restore modern/DerafshSample.Modern/DerafshSample.Modern.csproj

COPY modern/ modern/
RUN dotnet publish modern/DerafshSample.Modern/DerafshSample.Modern.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DerafshSample.Modern.dll"]
