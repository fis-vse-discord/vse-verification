FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VseVerification.csproj", "./"]
RUN dotnet restore "VseVerification.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "VseVerification.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VseVerification.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VseVerification.dll"]
