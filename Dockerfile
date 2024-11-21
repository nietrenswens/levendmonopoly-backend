# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN apt update -y && apt install fontconfig ttf-mscorefonts-installer -y

WORKDIR /app
COPY LevendMonopoly.Api/. .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
CMD dotnet LevendMonopoly.Api.dll