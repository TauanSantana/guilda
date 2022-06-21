#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HostNameProject/HostNameProject.csproj", "HostNameProject/"]
RUN dotnet restore "HostNameProject/HostNameProject.csproj"
COPY . .
WORKDIR "/src/HostNameProject"
RUN dotnet build "HostNameProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HostNameProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HostNameProject.dll"]