using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.AdminCommands
{
    public class RejectUserCommand : IRequest
    {
        public int UserId { get; set; }
    }
}
