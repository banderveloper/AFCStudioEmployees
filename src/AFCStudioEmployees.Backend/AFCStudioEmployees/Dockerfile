FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AFCStudioEmployees.WebAPI/AFCStudioEmployees.WebAPI.csproj", "AFCStudioEmployees.WebAPI/"]
RUN dotnet restore "AFCStudioEmployees.WebAPI/AFCStudioEmployees.WebAPI.csproj"
COPY . .
WORKDIR "/src/AFCStudioEmployees.WebAPI"
RUN dotnet build "AFCStudioEmployees.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AFCStudioEmployees.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AFCStudioEmployees.WebAPI.dll"]
