## Dockerfile
Dockerfile to build the agile studio server db image.

```bash
docker build -t agile-studio-server-db:0.1 -f .\Dockerfile .\
```

To run the container use the following command:
```bash
docker run --name agile-studio-server-db -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=agile-studio-server-db -e MYSQL_USER=agile-studio-server-db-user -e MYSQL_PASSWORD=password -p 127.0.0.1:3306:3306 -d agile-studio-server-db:0.1
```