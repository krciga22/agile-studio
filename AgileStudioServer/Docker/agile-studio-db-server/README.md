## Dockerfile
Use this Dockerfile to build the agile studio db server image. The 
db server is a MySql database server that is meant to store data 
for the agile studio app server.

## Docker Commands
```bash
#build the image
docker build -t agile-studio-server-db:0.1 -f .\Dockerfile .\

#run the container
docker run --name agile-studio-server-db -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=agile-studio-server-db -e MYSQL_USER=agile-studio-server-db-user -e MYSQL_PASSWORD=password -p 127.0.0.1:33060:3306 -v ./file-share/:/app/file-share -v /data:/var/lib/mysql -d --rm agile-studio-server-db:0.1

#stop the container
docker stop agile-studio-server-db
```

## App Directory (/app)
The app directory contains important files and folders for the db server.
- /app
	- /file-share (volume mount point)
	  - /data (mysql database data directory)
	  - /logs (mysql database logs directory)
	- /scripts
		- build.sh (script to finish building the docker image)
		- entrypoint.sh (script to initialize the container)