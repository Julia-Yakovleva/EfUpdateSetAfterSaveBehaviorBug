FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TestEFBreakingChanges/TestEFBreakingChanges.csproj", "TestEFBreakingChanges/"]
RUN dotnet restore "TestEFBreakingChanges/TestEFBreakingChanges.csproj"
COPY . .
WORKDIR "/src/TestEFBreakingChanges"
RUN dotnet build "TestEFBreakingChanges.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TestEFBreakingChanges.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestEFBreakingChanges.dll"]
