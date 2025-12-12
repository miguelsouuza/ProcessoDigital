# Etapa 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o csproj
COPY ProcessoDigital_Server.csproj .

# Restaura dependências
RUN dotnet restore ProcessoDigital_Server.csproj

# Copia todo o restante do projeto
COPY . .

# Compila o projeto
RUN dotnet publish ProcessoDigital_Server.csproj -c Release -o /app/publish

# Etapa 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ProcessoDigital_Server.dll"]
