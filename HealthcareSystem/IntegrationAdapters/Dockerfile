FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY . .
EXPOSE 80
CMD ASPNETCORE_URLS=http://*:$PORT dotnet IntegrationAdapters.dll
