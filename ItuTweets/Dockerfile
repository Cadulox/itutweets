#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ItuTweets/ItuTweets.API.csproj", "ItuTweets/"]
COPY ["ItuTweets.Data/ItuTweets.Data.csproj", "ItuTweets.Data/"]
COPY ["ItuTweets.Domain/ItuTweets.Domain.csproj", "ItuTweets.Domain/"]
COPY ["ItuTweets.Services/ItuTweets.Services.csproj", "ItuTweets.Services/"]
RUN dotnet restore "ItuTweets/ItuTweets.API.csproj"
COPY . .
WORKDIR "/src/ItuTweets"
RUN dotnet build "ItuTweets.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ItuTweets.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ItuTweets.API.dll"]