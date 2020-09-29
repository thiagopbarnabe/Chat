# Chat
Chat with SignalR Entity framework and RabbitMQ
Dependencies:

RabbitMQ
You need to have RabbitMQ installed. Change rabbitmq connection settings in appsettings of both applications or create a environment variable with TPBChat_ as a prefix

This is the default configuration:
  -"RabbitMQHostname": "localhost"
  -"RabbitMQPort": 5672
  -"QueueAnswersName": "TPBChatAnswers"
  -"QueueCommandsName": "TPBChatCommands"
  -"QueueErrorsName": "TPBChatErrors"

Keep in mind that you need to use a queuename not previously utilized otherwise if queue has different settings than Chat it won't work.


SQLServerLocalDB

You must have it installed. 

The first time you execute the software this error will occur
![alt text](https://raw.githubusercontent.com/thiagopbarnabe/Chat/master/image1.png)

Click apply migrations refresh the page and you should see the login page. 
Create a user for you and login on the application

For the solution to work properly you need to run both apps. WEB project and console app too. 
