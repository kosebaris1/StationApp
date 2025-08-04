using Application.Events;
using Application.Features.Mediator.Commands.AdminCommands;
using Application.Interfaces;
using Application.Interfaces.EventPublisherInterface;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AdminHandlers.Write
{
    public class ApproveUserCommandHandler : IRequestHandler<ApproveUserCommand, bool>
    {
        private readonly IRepository<User> _repository;
        private readonly IEventPublisher _eventPublisher;
        public ApproveUserCommandHandler(IRepository<User> repository, IEventPublisher eventPublisher)
        {
            _repository = repository;
            _eventPublisher = eventPublisher;
        }

        public async Task<bool> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.UserId);
            if (user == null)
                return false;

            user.IsApproved = true;
            user.UpdatedDate= DateTime.Now;
            await _repository.UpdateAsync(user);

            await _eventPublisher.PublishUserApprovedAsync(new UserApprovedEvent
            {
                Email = user.Email,
                FullName = user.FullName
            });
            return true;
        }
    }
}
