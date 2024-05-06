FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ARG POSTGRES_DB_USERNAME
ARG POSTGRES_DB_PASSWORD

ENV Database__Username=$POSTGRES_DB_USERNAME
ENV Database__Password=$POSTGRES_DB_PASSWORD

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AFCStudioEmployees.Domain/AFCStudioEmployees.Domain.csproj", "AFCStudioEmployees.Domain/AFCStudioEmployees.Domain.csproj"]
COPY ["AFCStudioEmployees.Application/AFCStudioEmployees.Application.csproj", "AFCStudioEmployees.Application/AFCStudioEmployees.Application.csproj"]
COPY ["AFCStudioEmployees.Persistence/AFCStudioEmployees.Persistence.csproj", "AFCStudioEmployees.Persistence/AFCStudioEmployees.Persistence.csproj"]
COPY ["AFCStudioEmployees.WebAPI/AFCStudioEmployees.WebAPI.csproj", "AFCStudioEmployees.WebAPI/AFCStudioEmployees.WebAPI.csproj"]

COPY . .
WORKDIR "/src/AFCStudioEmployees.WebAPI"
RUN dotnet build "AFCStudioEmployees.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AFCStudioEmployees.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AFCStudioEmployees.WebAPI.dll"]