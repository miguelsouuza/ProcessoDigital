# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj
COPY ProcessoDigital_Server.csproj ./
RUN dotnet restore ProcessoDigital_Server.csproj

# Copiar tudo
COPY . ./

# Publicar
RUN dotnet publish ProcessoDigital_Server.csproj -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /out ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ProcessoDigital_Server.dll"]
