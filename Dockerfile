# ===============================
# STAGE 1 - Build
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ProcessoDigital_Server.csproj ./
RUN dotnet restore ProcessoDigital_Server.csproj

COPY . .
RUN dotnet publish ProcessoDigital_Server.csproj -c Release -o /app/publish


# ===============================
# STAGE 2 - Runtime
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ProcessoDigital_Server.dll"]
