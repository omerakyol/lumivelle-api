# See https://aka.ms/containerfastmode
# ---------------- BASE ----------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

# ---------------- BUILD ----------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["Core/Core.csproj", "Core/"]

RUN dotnet restore "WebAPI/WebAPI.csproj"

COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

# ---------------- PUBLISH ----------------
FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

# ---------------- FINAL ----------------
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

# Media volumes
VOLUME ["/app/wwwroot/images", "/app/wwwroot/media"]

ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "WebAPI.dll"]
