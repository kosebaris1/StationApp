using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.AuthQueries
{
    public class LoginQuery : IRequest<string>
    {
        public LoginDto LoginDto { get; set; }

        public LoginQuery(LoginDto dto)
        {
            LoginDto = dto;
        }
    }
}
