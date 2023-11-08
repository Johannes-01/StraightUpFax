#runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine-arm64v8 AS base
WORKDIR /app

#sdk
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY FaxWebService/FaxWebService.csproj FaxWebService/
RUN dotnet restore "FaxWebService/FaxWebService.csproj"

# copy all locally files to the working directory
COPY . .
WORKDIR "/src/FaxWebService"
RUN dotnet build "FaxWebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FaxWebService.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY FaxWebService/quotes.txt .
ENTRYPOINT ["dotnet", "FaxWebService.dll"]