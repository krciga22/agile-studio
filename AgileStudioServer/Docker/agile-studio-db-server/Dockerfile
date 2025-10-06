
FROM mysql:8.0.42 AS base

WORKDIR /app
COPY scripts/*.sh scripts/
RUN chown root:root scripts/ && chmod -R 770 scripts/

WORKDIR /etc/mysql/conf.d
COPY my.cnf .

RUN /app/scripts/build.sh && rm /app/scripts/build.sh

ENTRYPOINT ["/app/scripts/entrypoint.sh"]