# devices-logger

## Used technologies
1. ASP.NET Core 7
2. MediatR
3. FluentValidation

## How to run
1. Navigate to root folder of project
2. To start project run command ```docker-compose up -d```
3. Webserver will start and it listens on two ports
    - 4430 (https)
    - 8080 (http)

## Open API specification
Swagger is accessible on:

1. https://localhost:4430/swagger
2. https://localhost:8080/swagger

## Structure
Program is written as REST API. It exposes several endpoints:

- Devices /api/devices
- Messages / api/messages
- RegisteredDevices / registeredDevices

## How to use
API support two types of devices 
1. Thermometer
2. Scale

In order for device to receive message, it must be registered

Once device is registered. It can receive message base on it´s type

### Scale


## Example of usage
1. Register scale device:
```
curl --location 'https://localhost:8080/api/devices/scale' \
--header 'Content-Type: application/json' \
--data '{
  "serialNumber": "0123456789",
  "firmwareVersion": "1.0",
  "vendor": "Abcd",
  "status": 0,
  "baseUnit": "g",
  "maxWeight": 10000,
  "minWeight": 10
}'
```

2. Device is ready to receive message(s):
 ```
 curl --location 'https://localhost:8080/api/messages/scale' \
--header 'Content-Type: application/json' \
--data '{
  "serialNumber": "0123456789",
  "measurementDate": "2023-05-18T06:12:30.414Z",
  "longitude": 10,
  "latitude": 10,
  "measurementUnit": "g",
  "weight": 1000
}'
```

3. To display count of received message by all devices:
 ```
 curl --location 'https://localhost:8080/api/registeredDevicescount' \
--header 'Content-Type: application/json'
 ```

4. To display all registered devices:
 ```
 curl --location 'https://localhost:8080/api/registeredDevices' \
--header 'Content-Type: application/json'
 ```

 4. To display registered device deviceSerialNumber:
 ```
 curl --location 'https://localhost:8080/api/registeredDevices/deviceSerialNumber' \
--header 'Content-Type: application/json'
 ```