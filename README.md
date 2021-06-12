# SensataApp
Sensata Vehicle Data App
Postman software: https://www.postman.com/downloads/

# Functionality and testing
While the application is running, you can:

### Add a vehicle
1. Click the 'Add vehicle' button.
2. Input a name for the new vehicle.
3. Click 'Save'.

### See a list of all vehicles
Go to localhost:(PORT)/api/vehicles.
Alternatively, you can use Postman to view more structured data.

### Add vehicle data
You need to send a HTTP POST request to localhost:(PORT)/api/vehicles/(ID). The ID needs to be of a vehicle that is registered in the system.
To get vehicle IDs use the previously mentioned function. The body of the request needs to be: `{ "latitude": 55.141210, "longitude": 24.016113, "speed": 55 }`
