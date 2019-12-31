﻿using RabbitMQ.Client;
using Shared.RabbitMq.QueueInterfaces;
using Shared.RabbitMq.QueueModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.RabbitMq.QueueService
{
    public class MessageConsumerScope : IMessageConsumerScope
    {
        private readonly MessageScopeSettings _messageScopeSettings;
        private readonly Lazy<IMessageQueue> _messageQueueLazy;
        private readonly Lazy<IMessageConsumer> _messageConsumerLazy;

        private readonly IConnectionFactory _connectionFactory;

        public MessageConsumerScope(IConnectionFactory connectionFactory, MessageScopeSettings messageScopeSettings = null)
        {
            _connectionFactory = connectionFactory;
            _messageScopeSettings = messageScopeSettings;

            _messageQueueLazy = new Lazy<IMessageQueue>(CreateMessageQueue);
            _messageConsumerLazy = new Lazy<IMessageConsumer>(CreateMessageConsumer);
        }
        public IMessageConsumer MessageConsumer => _messageConsumerLazy.Value;

        private IMessageQueue MessageQueue => _messageQueueLazy.Value;

        private IMessageQueue CreateMessageQueue()
        {
            return new MessageQueue(_connectionFactory, _messageScopeSettings);
        }
        private IMessageConsumer CreateMessageConsumer()
        {
            return new MessageConsumer(new MessageConsumerSettings
            {
                Channel = MessageQueue.Channel,
                QueueName = _messageScopeSettings.QueueName
            });
        }

        public void Dispose()
        {
            MessageQueue?.Dispose();
        }

    }
}
