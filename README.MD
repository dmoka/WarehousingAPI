# Warehousing microservice

## Spin up locally

1. Open powershell
2. Go to "src" folder
3. Build docker image for Warehousing microservice:
```
docker build -t warehousing .
```
3. Spin up microservice:
```
docker run -p 85:80 warehousing
```
4. The microservice is exposed and can be reached at http://localhost:85 . In order to verify its functionality, use the provided Postman collection located at the "src" folder
