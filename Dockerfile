# ============================================
# Etapa 1 ─ Build (SDK)
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o csproj
COPY ProcessoDigital_Server.csproj ./ 

# Restaura dependências
RUN dotnet restore ProcessoDigital_Server.csproj

# Copia todo o conteúdo agora
COPY . .

# Publica em modo Release
RUN dotnet publish ProcessoDigital_Server.csproj -c Release -o /app/publish


# ============================================
# Etapa 2 ─ Runtime (ASP.NET)
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia da etapa de build
COPY --from=build /app/publish .

# Render usa a porta 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ProcessoDigital_Server.dll"]
