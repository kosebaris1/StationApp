using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.UserCommands
{
    public class CreateUserCommand : IRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        //public int RoleId { get; set; } // foreign key
        //public bool IsApproved { get; set; }
    }
}
