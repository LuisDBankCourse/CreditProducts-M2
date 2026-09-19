FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CreditProducts.Api/CreditProducts.Api.csproj", "src/CreditProducts.Api/"]
COPY ["src/CreditProducts.Application/CreditProducts.Application.csproj", "src/CreditProducts.Application/"]
COPY ["src/CreditProducts.Domain/CreditProducts.Domain.csproj", "src/CreditProducts.Domain/"]
COPY ["src/CreditProducts.Infrastructure/CreditProducts.Infrastructure.csproj", "src/CreditProducts.Infrastructure/"]
RUN dotnet restore "src/CreditProducts.Api/CreditProducts.Api.csproj"
COPY . .
WORKDIR "/src/src/CreditProducts.Api"
RUN dotnet build "./CreditProducts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CreditProducts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreditProducts.Api.dll"]
