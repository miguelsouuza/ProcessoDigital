# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia somente o csproj primeiro (melhor cache)
COPY ProcessoDigital_Server.csproj ./

# Copia o restante
COPY . .

# Restaura dependências
RUN dotnet restore "./ProcessoDigital_Server.csproj"

# Publica
RUN dotnet publish "./ProcessoDigital_Server.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProcessoDigital_Server.dll"]
