﻿using DevFreela.Domain.Services.MessageBus;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.Services.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;        

        public MessageBusService()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",                
            };
        }

        public void Publish(string queue, byte[] message)
        {
            using(var connection = _factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    //Criando a fila
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    //Publicando a mensagem
                    channel.BasicPublish(
                        exchange: "", //Exchange padrão
                        routingKey: queue,
                        basicProperties: null,
                        body: message); 
                }
            }
        }
    }
}
