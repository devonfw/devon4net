rem dotnet tool install --global dotnet-sonarscanner
dotnet sonarscanner begin /k:"devon4net" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="e5f3a1e995780c74bf7cd3f06849e77f53c248e9"
dotnet build devon4net.sln
dotnet sonarscanner end /d:sonar.login="e5f3a1e995780c74bf7cd3f06849e77f53c248e9"