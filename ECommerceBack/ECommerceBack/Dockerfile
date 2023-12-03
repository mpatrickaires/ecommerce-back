FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime-env

ENV ASPNETCORE_URLS=http://*:5129
EXPOSE 5129

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env

WORKDIR /usr/src/source

COPY ECommerceBack.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o app

FROM runtime-env
WORKDIR /usr/src/app
COPY --from=build-env /usr/src/source/app .

CMD ["dotnet", "ECommerceBack.dll"]