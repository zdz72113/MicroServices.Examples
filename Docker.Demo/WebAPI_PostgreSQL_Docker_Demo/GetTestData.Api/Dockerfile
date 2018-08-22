FROM microsoft/dotnet:2.1-aspnetcore-runtime

WORKDIR /app

ENV ASPNETCORE_URLS http://*:80
EXPOSE 80/tcp

CMD ["dotnet", "GetTestData.Api.dll"]