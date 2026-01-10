#!/bin/sh

APP_DIR="/app"

# --------------------------------------
echo "creating agile-studio-app group and user"
groupadd -r agile-studio-app && useradd -r -g agile-studio-app agile-studio-app

# --------------------------------------
PUBLISH_DIR="$APP_DIR/publish"
echo "setting permissions for directory: $PUBLISH_DIR"
chown -R agile-studio-app:agile-studio-app $PUBLISH_DIR
chmod 550 $PUBLISH_DIR
chmod -R 440 $PUBLISH_DIR/*
chmod 550 $PUBLISH_DIR/AgileStudioServer.dll

# --------------------------------------
LOGS_DIR="$APP_DIR/storage/logs"
echo "setting permissions for the logs directory: $LOGS_DIR"
mkdir -p $LOGS_DIR
chown -R root:agile-studio-app $LOGS_DIR
chmod g+s $LOGS_DIR
chmod 660 $LOGS_DIR