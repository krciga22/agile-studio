#!/usr/bin/env bash

if [ -z "$1" ]; then
  ENV_FILE="./env.js"
else
  ENV_FILE="./env.$1.js"
fi

PUBLIC_ENV_FILE="./public/env.js"

if [ ! -f $ENV_FILE ]; then
    echo "Error: environment file not found: $ENV_FILE"
    exit 1
fi

echo "Copying $ENV_FILE to $PUBLIC_ENV_FILE";
cp $ENV_FILE $PUBLIC_ENV_FILE
