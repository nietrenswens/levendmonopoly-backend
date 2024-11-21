# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN echo "deb http://us-west-2.ec2.archive.ubuntu.com/ubuntu/ trusty multiverse \
deb http://us-west-2.ec2.archive.ubuntu.com/ubuntu/ trusty-updates multiverse \
deb http://us-west-2.ec2.archive.ubuntu.com/ubuntu/ trusty-backports main restricted universe multiverse" | sudo tee /etc/apt/sources.list.d/multiverse.list
RUN apt update -y && apt install -y fontconfig ttf-mscorefonts-installer

WORKDIR /app
COPY LevendMonopoly.Api/. .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
CMD dotnet LevendMonopoly.Api.dll