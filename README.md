# SensataApp

Postman software: https://www.postman.com/

# Functionality and testing
While the application is running, you can:

### Add a vehicle
1. Click the 'Add vehicle' button.
2. Input a name for the new vehicle.
3. Click 'Save'.

The new vehicle does not have any inputs yet so it will not show up on the map until it receives an input. 


### See a list of all vehicles
Go to localhost:(PORT)/api/vehicles.
Alternatively, you can use something like Postman to view more structured data.

### Add vehicle input
You need to send a HTTP POST request to localhost:(PORT)/api/vehicles/(ID), I personaly used Postman for this. The ID needs to be of a vehicle that is registered in the system.
To get vehicle IDs use the previously mentioned function. The body of the request needs to be: `{ "latitude": 55.141210, "longitude": 24.016113, "speed": 55 }`
