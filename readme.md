# Rabbit MQ: Single active consumer super streams. DotNet 7 docker swarm example 

## Pre Reqs

Have docker swarm running 

```
docker swarm init
```

## Run
Set permissions and run

```
sudo chmod +x ./run.sh
./run.sh
````

## Configuring 
Scale with

```
docker service scale superstack_consumer=10
docker service scale superstack_producer=10
```

View logs

```
docker service logs superstack_consumer -f
docker service logs superstack_producer -f
```

## Issues

2022-12-15: Consumers not appearing to balance load 
https://groups.google.com/u/1/g/rabbitmq-users/c/sJZ9VRogPHM


