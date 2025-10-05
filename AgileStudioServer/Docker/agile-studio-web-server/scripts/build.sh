#!/bin/sh

# --------------------------------------
LOGS_DIR="/app/file-share/logs"
echo "setting permissions for the logs directory: $LOGS_DIR"
mkdir -p $LOGS_DIR
chown -R root:www-data $LOGS_DIR
chmod -R 770 $LOGS_DIR

# --------------------------------------
SSL_DIR="/app/file-share/ssl"
echo "setting permissions for the ssl directory: $SSL_DIR"
mkdir -p $SSL_DIR
chown -R root:root $SSL_DIR
chmod -R 750 $SSL_DIR

chown -R root:root $SSL_DIR/*
chmod 644 $SSL_DIR/*.crt
chmod 600 $SSL_DIR/*.key

# --------------------------------------
#setup apache
echo "enabling apache modules"
a2enmod rewrite
a2enmod ssl
a2enmod proxy
a2enmod proxy_http
a2enmod headers

echo "disabling the default apache site"
a2dissite 000-default