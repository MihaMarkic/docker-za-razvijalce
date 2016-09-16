FROM microsoft/dotnet:1.0.1-core

RUN mkdir -p /dotnetapp
WORKDIR /dotnetapp
COPY server /dotnetapp/

EXPOSE 80
ENV ASPNETCORE_URLS http://*:80

ENTRYPOINT ["dotnet", "Sledilec.Streznik.dll"]