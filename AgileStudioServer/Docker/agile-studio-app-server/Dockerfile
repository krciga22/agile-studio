#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AgileStudioServer.csproj", "AgileStudioServer/"]
RUN dotnet restore "AgileStudioServer/AgileStudioServer.csproj"
COPY . "AgileStudioServer/"
WORKDIR "/src/AgileStudioServer"
RUN dotnet build "AgileStudioServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgileStudioServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish publish/

COPY Docker/agile-studio-app-server/scripts/*.sh scripts/
RUN chown root:root scripts/ && chmod -R 770 scripts/
RUN scripts/build.sh && rm scripts/build.sh

USER agile-studio-app

ENTRYPOINT ["/app/scripts/entrypoint.sh"]