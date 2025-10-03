
FROM mysql:8.0.42 AS base
WORKDIR /app
COPY init.sh /app/init.sh
ENTRYPOINT ["/app/init.sh"]