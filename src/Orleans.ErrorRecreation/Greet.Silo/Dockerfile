FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Greet.Silo/Greet.Silo.csproj", "Greet.Silo/"]
RUN dotnet restore "Greet.Silo/Greet.Silo.csproj"
COPY . .
WORKDIR "/src/Greet.Silo"
RUN dotnet build "Greet.Silo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Greet.Silo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Greet.Silo.dll"]
