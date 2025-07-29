using Application.Features.Mediator.Queries.AuthQueries;
using Application.Interfaces;
using Application.Interfaces.JwtService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AuthHandlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginQueryHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var dto = request.LoginDto;

            var user = await _userRepository.GetUserWithRoleAsync(dto.Email, dto.Password);

            if (user == null || !user.IsApproved)
                throw new UnauthorizedAccessException("Geçersiz kullanıcı veya onaylı değil.");

            return _jwtService.GenerateToken(user);
        }
    }

}
