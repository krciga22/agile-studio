## Dockerfile
Use this Dockerfile to build the Agile Studio Server image. The server 
is a .NET 8.0 ASP.NET REST API that is meant to serve data 
to the Agile Studio Client.

## Setup Environment Variables
Review the .env.example file in the `AgileStudioServer/Docker/` directory and create a 
`.env` file with the appropriate environment variables for your setup.

## Generate a Self-Signed TLS Certificate
Use the following OpenSSL command to generate self-signed TLS certificates for development. The 
generated files should be placed in the `./storage/certs/` directory so that they can be bind 
mounted into the container.
```bash
openssl req -newkey rsa:4096 -x509 -sha256 -days 3650 -nodes -out agile-studio-server.pem -keyout agile-studio-server.key
```

## Build the Image
From within the `AgileStudioServer/Docker/` directory, execute the following command to build the image:
```bash
docker image build -t agile-studio-server:0.1 -f .\Dockerfile ../
```

## Run With Docker Compose (recommended)
From within the `AgileStudioServer/Docker/` directory, copy and paste the `compose-example.yml` file 
and name the new file `compose.yaml`, then modify it as needed for your environment. You can then 
execute the following commands to start and stop the container using Docker Compose:
```bash
docker compose up
docker compose down
```

## Run the Container
From within the `AgileStudioServer/Docker/` directory, execute the following command to run the container:
```bash
docker container run -it --name agile-studio-server -e ASPNETCORE_ENVIRONMENT=Development -e HTTP_PORTS=80 -e HTTPS_PORTS=443 -e ASPNETCORE_Kestrel__Certificates__Default__Path=/app/storage/certs/agile-studio-server.pem -e ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/app/storage/certs/agile-studio-server.key -e AUTH0_CLIENT_SECRET=changeme -e DB_PASS=changeme -p 127.0.0.1:8080:80 -p 127.0.0.1:8443:443 -v ./storage/:/app/storage/ -d agile-studio-server:0.1

#Stop and Remove the Container
docker container stop agile-studio-server; docker container rm agile-studio-server;
```

## App Directory (/app)
The app directory contains important files and folders for the app server.
- /app
	- /publish (directory containing the published .NET application)
	- /scripts
		- build.sh (script to finish building the docker image)
	- /storage (bind mount for persistent storage)
	  - /certs (tls certs)
	  - /logs (application logs)
	  - /uploads (user uploads)