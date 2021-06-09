FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["Cakes.Web/Cakes.Web.csproj", "Cakes.Web/"]
RUN dotnet restore "Cakes.Web/Cakes.Web.csproj"
COPY . .
WORKDIR "/src/Cakes.Web"
RUN dotnet build "Cakes.Web.csproj" -c Release -o /app/build

ENV REACT_APP_API_URL=http://localhost:8082

FROM build AS publish
RUN dotnet publish "Cakes.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cakes.Web.dll"]
