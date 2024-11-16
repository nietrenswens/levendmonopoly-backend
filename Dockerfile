# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY LevendMonopoly.Api/. .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
RUN dotnet tool install --global dotnet-ef
RUN dotnet ef database update
CMD dotnet LevendMonopoly.Api.dll