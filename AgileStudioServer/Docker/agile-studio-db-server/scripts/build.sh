#!/bin/sh

# --------------------------------------
LOGS_DIR="/app/file-share/logs"
echo "setting permissions for the logs directory: $LOGS_DIR"
mkdir -p $LOGS_DIR
chown -R root:www-data $LOGS_DIR
chmod -R 770 $LOGS_DIR

# --------------------------------------
DATA_DIR="/app/file-share/data"
echo "setting permissions for the data directory: $DATA_DIR"
mkdir -p $DATA_DIR
chown -R mysql:mysql $DATA_DIR
chmod 750 $DATA_DIR