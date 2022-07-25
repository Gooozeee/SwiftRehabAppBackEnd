#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SwiftUserManagement/SwiftUserManagement.csproj", "SwiftUserManagement/"]
RUN dotnet restore "SwiftUserManagement/SwiftUserManagement.csproj"
COPY . .
WORKDIR "/src/SwiftUserManagement"
RUN dotnet build "SwiftUserManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SwiftUserManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SwiftUserManagement.dll"]