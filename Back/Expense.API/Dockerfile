#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Expense.API/Expense.API.csproj", "Expense.API/"]
COPY ["ExpenseManagement.Domain.Infraestructure/ExpenseManagement.Domain.Infraestructure.csproj", "ExpenseManagement.Domain.Infraestructure/"]
COPY ["ExpenseManagement.Domain/ExpenseManagement.Domain.csproj", "ExpenseManagement.Domain/"]
COPY ["Domain.Core/Domain.Core.csproj", "Domain.Core/"]
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