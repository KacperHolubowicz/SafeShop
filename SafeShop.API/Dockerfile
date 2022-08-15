#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SafeShop.API/SafeShop.API.csproj", "SafeShop.API/"]
RUN dotnet restore "SafeShop.API/SafeShop.API.csproj"
COPY . .
WORKDIR "/src/SafeShop.API"
RUN dotnet build "SafeShop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SafeShop.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SafeShop.API.dll"]