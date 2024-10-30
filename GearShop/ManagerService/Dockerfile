
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ManagerService/ManagerService.csproj", "ManagerService/"]
COPY ["BusinessObject/BusinessObject.csproj", "BusinessObject/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "./ManagerService/ManagerService.csproj"
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ManagerService.dll"]