#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY StraightUpFax/StraightUpFax.csproj StraightUpFax/
COPY FaxWebService/FaxWebService.csproj FaxWebService/
RUN dotnet restore "StraightUpFax/StraightUpFax.csproj"
RUN dotnet restore "FaxWebService/FaxWebService.csproj"

# copy all locally files to the working directory
COPY . .
WORKDIR "/src/StraightUpFax"
RUN dotnet build "StraightUpFax.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StraightUpFax.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet publish "FaxWebService.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StraightUpFax.dll"]