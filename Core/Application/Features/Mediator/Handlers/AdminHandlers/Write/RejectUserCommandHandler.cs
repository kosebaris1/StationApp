using Application.Features.Mediator.Commands.AdminCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AdminHandlers.Write
{
    public class RejectUserCommandHandler : IRequestHandler<RejectUserCommand>
    {
        private readonly IRepository<User> _repository;

        public RejectUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RejectUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _repository.GetByIdAsync(request.UserId);
                user.DeletedDate = DateTime.Now;
                await _repository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RejectUsrerCommand Handler: {ex.Message}");
                throw;
            }


        }
    }
}
