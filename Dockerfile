# Fase base per l'immagine runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
WORKDIR /app                       # La cartella di lavoro è /app
RUN mkdir -p data                  # Crea la cartella 'data' per il database SQLite
RUN chmod -R 777 data              # Garantisce i permessi di scrittura
EXPOSE 8080
EXPOSE 8081

# Fase di build per compilare il progetto
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Meals-API.csproj", "."]
RUN dotnet restore "./Meals-API.csproj"
COPY . .
RUN dotnet build "./Meals-API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase di publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Meals-API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase finale per produzione
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meals-API.dll"]