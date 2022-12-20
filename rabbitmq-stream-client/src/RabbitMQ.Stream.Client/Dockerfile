FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/RabbitMQ.Stream.Client/RabbitMQ.Stream.Client.csproj", "src/RabbitMQ.Stream.Client/"]
RUN dotnet restore "src/RabbitMQ.Stream.Client/RabbitMQ.Stream.Client.csproj"
COPY . .
WORKDIR "/src/src/RabbitMQ.Stream.Client"
RUN dotnet build "RabbitMQ.Stream.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitMQ.Stream.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMQ.Stream.Client.dll"]
