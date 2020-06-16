#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Expense.API/Expense.API.csproj", "Expense.API/"]
RUN dotnet restore "Expense.API/Expense.API.csproj"
COPY . .
WORKDIR "/src/Expense.API"
RUN dotnet build "Expense.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Expense.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Expense.API.dll"]