#!/bin/sh

if [[ -z $URL_BACKEND ]]; then
    echo "YOU HAVE TO SET URL_BACKEND"
    echo "sudo URL_BACKEND=url docker compose -f docker-compose.yml up"
    export URL_BACKEND="http://localhost:5149/api"
    echo "DEFAULT URL_BACKEND=$URL_BACKEND"
else
    echo "You have set URL_BACKEND=$URL_BACKEND"
fi

sed -i "s|\"route\":.*|\"route\": \"$URL_BACKEND\"|" src/app/Services/route.json

npm run build