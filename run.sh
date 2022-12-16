#!/bin/sh
docker stack rm superstack
docker build ./producer -t producer
docker build ./consumer -t consumer
docker build ./rabbitmq -t myrabbit
docker stack deploy -c docker-compose.yml superstack
docker service logs superstack_consumer -f