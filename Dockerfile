# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY LevendMonopoly.Api/. .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Update sources and install dependencies
RUN sed -i 's/Components: main*/& contrib/' /etc/apt/sources.list.d/debian.sources \
    && apt update -y \
    && apt install -y --no-install-recommends \
       fontconfig \
       ttf-mscorefonts-installer \
       libgdiplus \
       libc6-dev \
    && ln -s /lib/libgdiplus.so /usr/lib/libgdiplus.so \
    && ln -s /lib/libgdiplus.so.0 /usr/lib/libgdiplus.so.0 \
    && apt clean && rm -rf /var/lib/apt/lists/*

# Set working directory and copy app
WORKDIR /app
COPY --from=build /app/out .

# Set LD_LIBRARY_PATH to ensure runtime can find libgdiplus
ENV LD_LIBRARY_PATH=/usr/lib:/lib:$LD_LIBRARY_PATH

# Run the application
CMD ["dotnet", "LevendMonopoly.Api.dll"]
