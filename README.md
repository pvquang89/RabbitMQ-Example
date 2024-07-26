
## What is RabbitMQ?

RabbitMQ is an open source message broker software. It accepts messages from producers, and delivers them to consumers. It acts like a middleman which can be used to reduce loads and delivery times taken by web application servers.

## The project

This project consists of three sub-projects, which are:
- **RabbitMQNetCore**: a class project containing a class to type our message and also classes with the rules for sending and consuming the message.
- **RabbitMQNetCore.Publisher**: a console project responsible for running the publisher.
- **RabbitMQNetCore.Consumer**: a console project responsible for running the consumer.

Could I have created a single project, yes I could, but I wanted to separate it for a better understanding of what each one does.


## How to run?

First we need to run the RabbitMQ service, I recommend that you make use of a Docker image, so you need to have Docker installed and run the following command:

```bash
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management
```

This command will create and run an instance of RabbitMQ.

Run the Publisher project:

```bash
dotnet run --project .\RabbitMQNetCore.Publisher\RabbitMQNetCore.Publisher.csproj
```

Run the Consumer project:

```bash
dotnet run --project .\RabbitMQNetCore.Consumer\RabbitMQNetCore.Consumer.csproj
```

Now, in the Publisher console, you can perform tests by sending messages and monitoring the consumption of the same in the Consumer console.

This is a simple project to demonstrate how the messaging service works.


  

