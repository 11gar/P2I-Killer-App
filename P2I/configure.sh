#!/bin/sh

if [[ -z $URL_BACKEND ]]; then
    echo "YOU HAVE TO SET URL_BACKEND"
    echo "sudo URL_BACKEND=url docker compose -f docker-compose.yml up"
    exit
else
    echo "You have set URL_BACKEND=$URL_BACKEND"
fi

sed -i "s|\"route\":.*|\"route\": \"$URL_BACKEND\"|" src/app/Services/route.json

npx serve dist/p2-i -p 80