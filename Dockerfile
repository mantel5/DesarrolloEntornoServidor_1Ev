# Imagen del SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el proyecto y restauramos
COPY ["ApiSuplementos.csproj", "."]
RUN dotnet restore "./ApiSuplementos.csproj"

# Copiamos todo y compilamos
COPY . .
WORKDIR "/src/."
RUN dotnet build "ApiSuplementos.csproj" -c Release -o /app/build

# Publicamos
FROM build AS publish
RUN dotnet publish "ApiSuplementos.csproj" -c Release -o /app/publish

# Imagen final ligera para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiSuplementos.dll"]