# Sử dụng image SDK 8.0 để build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj và restore
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ code và publish
COPY . ./
RUN dotnet publish -c Release -o out

# Sử dụng image Runtime 8.0 để chạy app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/out .

# Khởi chạy ứng dụng
ENTRYPOINT ["dotnet", "NexusCart.dll"]