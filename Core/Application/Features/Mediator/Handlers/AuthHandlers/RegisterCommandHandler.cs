using Application.Features.Mediator.Commands.UserCommands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AuthHandlers
{
    public class RegisterCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _mapper.Map<User>(request);
                result.CreatedDate = DateTime.UtcNow;
                result.IsApproved = false;
                result.RoleId = 2;
                await _repository.CreateAsync(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateUserCommand Handler: {ex.Message}");
                throw;
            }
        }
    }
}
