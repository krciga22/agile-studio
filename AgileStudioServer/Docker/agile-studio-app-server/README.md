## Dockerfile
Use this Dockerfile to build the agile studio app server image. The
app server is a .NET 8.0 ASP.NET API that is meant to serve data 
to the agile studio client app.

## Docker Commands
```bash
#build the image
docker build -t agile-studio-server-app:0.1 -f .\Dockerfile ../../

#run the container
docker run --name agile-studio-server-app -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS=http://api.development.agilestudio.dev:8081 -e AUTH0_DOMAIN=changeme -e AUTH0_CLIENT_ID=changeme -e AUTH0_CLIENT_SECRET=changeme -e AUTH0_AUDIENCE=changeme -e DB_HOST=changeme -e DB_PORT=3306 -e DB_NAME=changeme -e DB_USER=changeme -e DB_PASS=changeme_ --network agile-studio-server-network -p 127.0.0.1:8081:8081 -v ./file-share/:/app/file-share -d --rm agile-studio-server-app:0.1

#stop the container
docker stop agile-studio-server-app
```

## App Directory (/app)
The app directory contains important files and folders for the app server.
- /app
	- /file-share (volume mount point)
	  - /logs (directory for application logs)
	- /publish (directory containing the published .NET application)
	- /scripts
		- build.sh (script to finish building the docker image)
		- entrypoint.sh (script to initialize the container)