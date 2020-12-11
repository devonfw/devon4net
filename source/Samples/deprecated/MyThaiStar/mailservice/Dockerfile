FROM microsoft/dotnet:2.1-sdk
 
COPY . /app
WORKDIR /app/OASP4Net.Application.WebAPI
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

ENV ASPNETCORE_URLS http://0.0.0.0:80
EXPOSE 2025/tcp
ENTRYPOINT ["dotnet", "run"]