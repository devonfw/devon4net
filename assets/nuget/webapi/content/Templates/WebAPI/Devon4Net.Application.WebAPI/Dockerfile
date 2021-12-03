#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Devon4Net.Application.WebAPI/Devon4Net.Application.WebAPI.csproj", "Devon4Net.Application.WebAPI/"]
RUN dotnet restore "Devon4Net.Application.WebAPI/Devon4Net.Application.WebAPI.csproj"
COPY . .
WORKDIR "/src/Devon4Net.Application.WebAPI"
RUN dotnet build "Devon4Net.Application.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devon4Net.Application.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devon4Net.Application.WebAPI.dll"]