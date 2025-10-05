
FROM ubuntu/apache2:latest AS base

WORKDIR /app
COPY scripts/*.sh scripts/
RUN chown root:root scripts/ && chmod -R 770 scripts/

WORKDIR /etc/apache2/
COPY apache2.conf .
COPY agile-studio-server.conf sites-available/agile-studio-server.conf

RUN /app/scripts/build.sh && rm /app/scripts/build.sh

ENTRYPOINT ["/app/scripts/entrypoint.sh"]