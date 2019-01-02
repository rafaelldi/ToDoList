#!/bin/bash
echo "$HEROKU_AUTH_TOKEN" | docker login -u "_" --password-stdin registry.heroku.com
docker tag todolist registry.heroku.com/$HEROKU_APP_NAME/web
docker push registry.heroku.com/$HEROKU_APP_NAME/web