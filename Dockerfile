# ============================================
# Etapa 1 — Build (SDK)
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o .csproj
COPY ProcessoDigital/ProcessoDigital_Server/ProcessoDigital_Server.csproj ./ProcessoDigital_Server/

# Restaura dependências
RUN dotnet restore ./ProcessoDigital_Server/ProcessoDigital_Server.csproj

# Copia TODO o projeto
COPY ProcessoDigital/ProcessoDigital_Server/ ./ProcessoDigital_Server/

# Publica o projeto
RUN dotnet publish ./ProcessoDigital_Server/ProcessoDigital_Server.csproj -c Release -o /app/publish


# ============================================
# Etapa 2 — Runtime
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Porta do Render
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ProcessoDigital_Server.dll"]
