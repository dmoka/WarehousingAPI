FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Warehousing.API/Warehousing.API.csproj", "Warehousing.API/"]
RUN dotnet restore "Warehousing.API/Warehousing.API.csproj"
COPY . .
WORKDIR "/src/Warehousing.API"
RUN dotnet build "Warehousing.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Warehousing.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warehousing.API.dll"]