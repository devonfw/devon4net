@echo off
Echo Building devon4net

SET PROJ=devon4net.sln
dotnet msbuild %PROJ% /t:restore /p:Configuration=Release
dotnet msbuild %PROJ% /t:build /p:Configuration=Release
dotnet msbuild %PROJ% /t:pack /p:Configuration=Release;PackageOutputPath=%~dp0artifacts

:end