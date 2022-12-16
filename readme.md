Have docker swarm running 

```
docker swarm init
```


Set permissions and run

```
sudo chmod +x ./run.sh
./run.sh
````


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



