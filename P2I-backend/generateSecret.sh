#!/bin/bash
SECRET=$(openssl rand -hex 20) 

sed -i "s|\"JWT_Secret\": \".*\"|\"JWT_Secret\": \"$SECRET\"|" appsettings.json