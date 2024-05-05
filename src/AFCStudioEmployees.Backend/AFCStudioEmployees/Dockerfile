FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AFCStudioEmployees.csproj", "./"]
RUN dotnet restore "AFCStudioEmployees.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "AFCStudioEmployees.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AFCStudioEmployees.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AFCStudioEmployees.dll"]
