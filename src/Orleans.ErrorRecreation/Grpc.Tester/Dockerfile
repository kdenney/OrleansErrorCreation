FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Grpc.Tester/Grpc.Tester.csproj", "Grpc.Tester/"]
RUN dotnet restore "Grpc.Tester/Grpc.Tester.csproj"
COPY . .
WORKDIR "/src/Grpc.Tester"
RUN dotnet build "Grpc.Tester.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Grpc.Tester.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Grpc.Tester.dll"]
