using Application.Events;
using Application.Interfaces.EventPublisherInterface;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Messaging
{
    public class MassTransitEventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitEventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task PublishUserApprovedAsync(UserApprovedEvent @event)
        {
            return _publishEndpoint.Publish(@event);
        }

        public Task PublishUserRejectedAsync(UserRejectedEvent @event)
        {
            return _publishEndpoint.Publish(@event);
        }
    }
}
