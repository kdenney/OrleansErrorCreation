FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Grpc.Api/Grpc.Api.csproj", "Grpc.Api/"]
RUN dotnet restore "Grpc.Api/Grpc.Api.csproj"
COPY . .
WORKDIR "/src/Grpc.Api"
RUN dotnet build "Grpc.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Grpc.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Grpc.Api.dll"]
