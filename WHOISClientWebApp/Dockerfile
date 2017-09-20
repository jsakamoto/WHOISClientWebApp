FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENV PORT 80
CMD ASPNETCORE_URLS=http://*:$PORT dotnet WHOISClientWebApp.dll
