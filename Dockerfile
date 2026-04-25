# ----------------------
# Base runtime image
# ----------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# ----------------------
# Build stage
# ----------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["LMSAppMVC/LMSAppMVC.csproj", "LMSAppMVC/"]
RUN dotnet restore "LMSAppMVC/LMSAppMVC.csproj"

# Copy the rest of the project
COPY . .
WORKDIR "/src/LMSAppMVC"

# Build the project
RUN dotnet build "LMSAppMVC.csproj" -c Release -o /app/build

# ----------------------
# Publish stage
# ----------------------
FROM build AS publish
RUN dotnet publish "LMSAppMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ----------------------
# Final image
# ----------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LMSAppMVC.dll"]
