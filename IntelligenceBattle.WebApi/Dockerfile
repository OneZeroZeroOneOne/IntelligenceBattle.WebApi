FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /source

COPY IntelligenceBattle.WebApi/*.csproj IntelligenceBattle.WebApi/
COPY IntelligenceBattle.WebApi.Dal/*.csproj IntelligenceBattle.WebApi.Dal/
COPY IntelligenceBattle.WebApi.Bll/*.csproj IntelligenceBattle.WebApi.Bll/
COPY IntelligenceBattle.WebApi.Utilities/*.csproj IntelligenceBattle.WebApi.Utilities/
COPY IntelligenceBattle.WebApi.Security/*.csproj IntelligenceBattle.WebApi.Security/

RUN dotnet restore IntelligenceBattle.WebApi/IntelligenceBattle.WebApi.csproj

COPY IntelligenceBattle.WebApi/ IntelligenceBattle.WebApi/
COPY IntelligenceBattle.WebApi.Dal/ IntelligenceBattle.WebApi.Dal/
COPY IntelligenceBattle.WebApi.Bll/ IntelligenceBattle.WebApi.Bll/
COPY IntelligenceBattle.WebApi.Utilities/ IntelligenceBattle.WebApi.Utilities/
COPY IntelligenceBattle.WebApi.Security/ IntelligenceBattle.WebApi.Security/
WORKDIR /source/IntelligenceBattle.WebApi
RUN dotnet build -c release

FROM build AS publish
RUN dotnet publish -c release --no-build -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "IntelligenceBattle.WebApi.dll"]
