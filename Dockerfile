FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ProcessoDigital_Server/ProcessoDigital_Server.csproj ProcessoDigital_Server/
RUN dotnet restore ProcessoDigital_Server/ProcessoDigital_Server.csproj

COPY ProcessoDigital_Server/ ProcessoDigital_Server/
RUN dotnet publish ProcessoDigital_Server/ProcessoDigital_Server.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ProcessoDigital_Server.dll"]
