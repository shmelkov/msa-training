#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

#EXPOSE 8001

#ENV ASPNETCORE_URLS=https://+:5009;http://+:80
#ENV ASPNETCORE_HTTPS_PORT=5009

#test data that to test environment config
ENV POSTGRES_HOST=postgres
ENV POSTGRES_PORT=5432
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=postgres

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Users/Users.csproj", "Users/"]
RUN dotnet restore "Users/Users.csproj"
COPY . .
WORKDIR "/src/Users"
RUN dotnet build "Users.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Users.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.dll"]