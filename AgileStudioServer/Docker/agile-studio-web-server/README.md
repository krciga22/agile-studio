## Dockerfile
Use this Dockerfile to build the agile studio web server image. The 
web server is an Apache server that is meant to proxy requests to 
the agile studio app server, so that the app server is not directly 
accessible from outside the docker host.

## Docker Commands
```bash
#build the image
docker build -t agile-studio-server-web:0.1 -f .\Dockerfile .\

#run the container
docker run --name agile-studio-server-web -e TZ=UTC --network agile-studio-server-network -p 127.0.0.1:44360:443 -p 127.0.0.1:8080:80 -v ./file-share/:/app/file-share -d --rm agile-studio-server-web:0.1

#stop the container
docker stop agile-studio-server-web
```

## App Directory (/app)
The app directory contains important files and folders for the web server.
- /app
	- /file-share (volume mount point)
	  - /ssl
		- agile-studio-server.crt (SSL certificate, required)
		- agile-studio-server.key (SSL private key, required)
	  - /logs
		- access.log (web server access log)
		- error.log (web server error log)
	- /scripts
		- build.sh (script to finish building the docker image)
		- entrypoint.sh (script to initialize the container)