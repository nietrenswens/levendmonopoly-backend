# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY LevendMonopoly.Api/. .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
RUN sed -i 's/Components: main*/& contrib/' /etc/apt/sources.list.d/debian.sources
RUN apt update -y && apt install -y fontconfig ttf-mscorefonts-installer apt-utils libgdiplus libc6-dev
WORKDIR /app
COPY --from=build /app/out .
CMD dotnet LevendMonopoly.Api.dll