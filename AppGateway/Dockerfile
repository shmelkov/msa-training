#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

#EXPOSE 8001

#ENV ASPNETCORE_URLS=https://+:5009;http://+:80
#ENV ASPNETCORE_HTTPS_PORT=5009

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AppGateway/AppGateway.csproj", "AppGateway/"]
RUN dotnet restore "AppGateway/AppGateway.csproj"
COPY . .
WORKDIR "/src/AppGateway"
RUN dotnet build "AppGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppGateway.dll"]