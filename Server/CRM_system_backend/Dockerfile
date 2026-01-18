# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CRM_system_backend/CRM_system_backend.csproj", "CRM_system_backend/"]
COPY ["system.Business/CRMSystem.Business.csproj", "system.Business/"]
COPY ["system.Core/CRMSystem.Core.csproj", "system.Core/"]
COPY ["system.DataAccess/CRMSystem.DataAccess.csproj", "system.DataAccess/"]
RUN dotnet restore "./CRM_system_backend/CRM_system_backend.csproj"
COPY . .
WORKDIR "/src/CRM_system_backend"
RUN dotnet build "./CRM_system_backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CRM_system_backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRM_system_backend.dll"]