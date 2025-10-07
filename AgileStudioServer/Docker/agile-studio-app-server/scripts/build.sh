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
LOGS_DIR="$APP_DIR/file-share/logs"
echo "setting permissions for the logs directory: $LOGS_DIR"
mkdir -p $LOGS_DIR
chown -R root:agile-studio-app $LOGS_DIR
chmod g+s $LOGS_DIR
chmod 770 $LOGS_DIR
chmod -R 660 $LOGS_DIR/*
#todo these permissions aren't being reflected in the 
#container (possibly due to bind-mount), investigate later

# --------------------------------------
APP_SCRIPTS_DIR="$APP_DIR/scripts"
echo "setting permissions for directory: $APP_SCRIPTS_DIR"
chown -R root:agile-studio-app $APP_SCRIPTS_DIR
chmod 750 $APP_SCRIPTS_DIR
chmod 700 $APP_SCRIPTS_DIR/*.sh
chmod 750 $APP_SCRIPTS_DIR/entrypoint.sh