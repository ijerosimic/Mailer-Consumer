The app consists of 3 projects (UI/Api/Console)

1) Caller/UI - Basic UI. The user can enter an email address, some text and upload a file
2) Receiver/Web API - Receives data from the caller and sends it to the console app through a RabbitMQ channel
3) Consumer/Console app - consumes the message and sends an email with the data received
