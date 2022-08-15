#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SafeShop.Application/SafeShop.Application.csproj", "SafeShop.Application/"]
RUN dotnet restore "SafeShop.Application/SafeShop.Application.csproj"
COPY . .
WORKDIR "/src/SafeShop.Application"
RUN dotnet build "SafeShop.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SafeShop.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SafeShop.Application.dll"]