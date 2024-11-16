# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY LevendMonopoly.Api/. .
RUN dotnet restore
RUN dotnet publish -c Release -o out
RUN dotnet tool install --version 6.0.9 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet-ef database update

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
CMD dotnet LevendMonopoly.Api.dll