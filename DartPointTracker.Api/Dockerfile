FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DartPointTracker.Api/DartPointTracker.Api/DartPointTracker.Api.csproj", "DartPointTracker.Api/DartPointTracker.Api/"]
RUN dotnet restore "DartPointTracker.Api/DartPointTracker.Api/DartPointTracker.Api.csproj"
COPY . .
WORKDIR "/src/DartPointTracker.Api/DartPointTracker.Api"
RUN dotnet build "DartPointTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DartPointTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DartPointTracker.Api.dll"]
