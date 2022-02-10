FROM microsoft/dotnet:2.1-sdk
RUN	apt-get update 
RUN	apt-get -f install -y libgdiplus
RUN	apt-get -f install -y wget
RUN wget http://ftp.de.debian.org/debian/pool/contrib/m/msttcorefonts/ttf-mscorefonts-installer_3.6_all.deb
RUN apt-get -f install -y ./ttf-mscorefonts-installer_3.6_all.deb
RUN	apt-get -f install -y

COPY . /reporting

WORKDIR /reporting/RdlEngine.Core.Server
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

ENV ASPNETCORE_URLS http://0.0.0.0:2021
EXPOSE 2021/tcp
ENTRYPOINT ["dotnet", "run"]