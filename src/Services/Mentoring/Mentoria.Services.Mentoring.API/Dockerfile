# Etapa base: imagen para tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa build: compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar archivos .csproj y restaurar dependencias
COPY ["src/Services/Mentoring/Mentoria.Services.Mentoring.API/Mentoria.Services.Mentoring.API.csproj", "Mentoring.API/"]
COPY ["src/Services/Mentoring/Mentoria.Services.Mentoring.Application/Mentoria.Services.Mentoring.Application.csproj", "Application/"]
COPY ["src/Services/Mentoring/Mentoria.Services.Mentoring.Infraestructure/Mentoria.Services.Mentoring.Infraestructure.csproj", "Infrastructure/"]
COPY ["src/Services/Mentoring/Mentoria.Services.Mentoring.Domain/Mentoria.Services.Mentoring.Domain.csproj", "Domain/"]

# Restaurar dependencias
RUN dotnet restore "Mentoring.API/Mentoria.Services.Mentoring.API.csproj"

# Copiar todo el código fuente
COPY . .

# Establecer el directorio de trabajo del proyecto principal
WORKDIR "/src/src/Services/Mentoring/Mentoria.Services.Mentoring.API"

# Compilar la aplicación
RUN dotnet build "Mentoria.Services.Mentoring.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa publish: publicar la app optimizada
FROM build AS publish
RUN dotnet publish "Mentoria.Services.Mentoring.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final: imagen que se ejecutará
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mentoria.Services.Mentoring.API.dll"]
