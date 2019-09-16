The app consists of 3 projects

1) Caller/UI - Basic UI. The user can submit a form to the mailer api with an email address, some text and an uploaded file. </br>
2) Mailer/Web API - Receives data from the caller and sends it to the console app through a RabbitMQ channel </br>
3) Consumer/Console app - consumes the message and sends an email with the data received
