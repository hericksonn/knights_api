docker build -t knightsapi .
docker run -d -p 8080:80 --name knightsapi knightsapi
