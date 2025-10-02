## Dockerfile
Dockerfile is used  to build the agile studio server image.

```bash
docker build -t krciga22/agile-studio-server:0.1 -f .\Dockerfile ../
```

## Setup Docker Containers for Local Development
For local development, you can use docker-compose to run the database container.
```bash
#from AgileStudioServer/Docker/, run:
docker compose --file docker-compose.yml --file docker-compose.development.yml --env-file ../../data/development/.env up agile-studio-server-db
```

Docker Compose File (docker-compose.development.yml):
- Configure the database service using 

Appsettings File (appsettings.Development.json):
- Similar to appsettings.json, but with development specific settings.

Secrets:
- See secrets.json for secrets to configure.

Volumes:
- [solution-dir]/data/development/certs
- [solution-dir]/data/development/db
- [solution-dir]/data/testing/certs
- [solution-dir]/data/testing/db

## Setup Docker Containers for Local Testing
For local testing, you can use docker-compose to run the application server, web server, and database containers.
```bash
#from AgileStudioServer/Docker/, run:
docker compose --file docker-compose.yml --file docker-compose.testing.yml --env-file ../../data/testing/.env up -d agile-studio-server agile-studio-server-db agile-studio-server-reverse-proxy
```

Make sure to setup the following volumes locally for development and testing:
- [solution-dir]/data/development/certs
- [solution-dir]/data/development/db
- [solution-dir]/data/testing/certs
- [solution-dir]/data/testing/db

The docker-compose.yml file is used to start the web server and database containers.

For the development environment, run the db container only:
```bash
#in the project root folder, run:
docker compose --file .\AgileStudioServer\docker-compose.yml --file .\AgileStudioServer\docker-compose.development.yml --env-file .\data\development\.env up agile-studio-server-db
```

For the testing environment, run both the web server and db containers:
```bash
#in the project root folder, run:
docker compose --file .\AgileStudioServer\docker-compose.yml --file .\AgileStudioServer\docker-compose.testing.yml --env-file .\data\testing\.env up agile-studio-server agile-studio-server-db
```

To bring the service containers up:
```bash
docker compose up -d
docker compose up -d agile-studio-server-db-temp
```

To take all service containers down:
```bash
docker compose down
```
